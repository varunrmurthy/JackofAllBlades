using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region dropVariables
    Hashtable dropChance = new Hashtable();
    public GameObject drop1;
    public float drop1Chance;
    float duration = .3f;
    Vector2 pointToMove;
    #endregion
    #region battleVariables
    public float movespeed;
    public Rigidbody2D enemyRB;
    public float currHealth;
    public float maxHealth;
    public float moveSpeed;
    public Vector2 currDirection;
    public float hitRadius;
    public int damageDealt;
    public float timeBetweenAttacks;
    public bool canAttack;
    public bool attacking;
    public bool stun = false;
    public Transform player;
    protected bool beingPushed = false;
    #endregion
    #region unity_functions
    public void Awake()
    {
        dropChance.Add(drop1, drop1Chance);
        enemyRB = GetComponent<Rigidbody2D>();
        pointToMove = new Vector2(Random.Range(enemyRB.position.x - .5f, enemyRB.position.x + .5f), Random.Range(enemyRB.position.y - .5f, enemyRB.position.y + .5f));
        moveToPoint(pointToMove);
    }
    #endregion
    #region drop_functions
    protected void drop() 
    {
        Debug.Log("dropping");
        float curr = 0f;
        float roll = Random.Range(0f, 1f);
        foreach(GameObject o in dropChance.Keys) {
            Debug.Log("My roll is" + roll);
            if (roll > curr && roll < curr + (float)dropChance[o])
            {
                Debug.Log(transform.position);


                Instantiate(o, transform.position, transform.rotation);
            }
            else curr += (float)dropChance[o];
        }
    }
    #endregion
    #region battle_functions
    public void takeDamage(float value, Transform p = null)
    {
        currHealth -= value;
        
        if (currHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log(p);
            if (!p) StartCoroutine(knockback(player));
            else StartCoroutine(knockback(p));
        }

    }
    IEnumerator knockback(Transform player)
    {
        beingPushed = true;
        this.GetComponent<Rigidbody2D>().velocity = (new Vector2((player.position.x - enemyRB.position.x), player.position.y - enemyRB.position.y).normalized * -7);
        yield return new WaitForSeconds(.1f);
        Debug.Log("I have been pushed back" + this.GetComponent<Rigidbody2D>().velocity + "transform position: " + transform.position);

        this.GetComponent<Rigidbody2D>().velocity *= 0f;
        beingPushed = false;
    }


    protected void Die()
    {
        drop();
        Destroy(this.gameObject);
    }
    #endregion
    #region movement_functions
    public void Move()
    {
        if (!beingPushed)
        {
            //Calculate the movement vector. 
            if (player == null)
            {
                
                if (duration >= 0)
                {
                    
                    duration -= Time.deltaTime;
                }
                else
                {
            
                    duration = .3f;
                    pointToMove = new Vector2(Random.Range(enemyRB.position.x - .5f, enemyRB.position.x + .5f), Random.Range(enemyRB.position.y - .5f, enemyRB.position.y + .5f));
                    moveToPoint(pointToMove);

                }
            }
            else
            {
                if (duration >= 0)
                {
                    pointToMove = new Vector2(Random.Range(player.position.x - .5f, player.position.x + .5f), Random.Range(player.position.y - .5f, player.position.y + .5f));
                    
                    duration -= Time.deltaTime;

                }
                else
                {
                    pointToMove = new Vector2(Random.Range(player.position.x - .5f, player.position.x + .5f), Random.Range(player.position.y - .5f, player.position.y + .5f));
                    duration = .3f;
                    moveToPoint(pointToMove);
                }
                
            }
        }
    }
    public void moveToPoint(Vector2 point)
    {

        Vector2 direction = point - new Vector2(enemyRB.position.x, enemyRB.position.y);
        enemyRB.velocity = direction.normalized * moveSpeed;
    }
    #endregion
}
