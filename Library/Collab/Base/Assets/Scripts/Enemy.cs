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
        pointToMove = new Vector2(Random.Range(enemyRB.position.x - 1, enemyRB.position.x + 1), Random.Range(enemyRB.position.y - 1, enemyRB.position.y + 1));
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
    public void takeDamage(float value)
    {
        currHealth -= value;
        Debug.Log("Enemy Health is now " + currHealth.ToString());
        
        if (currHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(knockback());
        }

    }
    IEnumerator knockback()
    {
        beingPushed = true;
        this.GetComponent<Rigidbody2D>().velocity = ((player.position - transform.position).normalized * -50f);

        Debug.Log("I have been pushed back" + this.GetComponent<Rigidbody2D>().velocity + "transform position: " + transform.position);
        yield return new WaitForSeconds(.1f);
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
            Debug.Log(enemyRB);
            //Calculate the movement vector. 
            if (player == null)
            {
                
                if (duration >= 0)
                {
                    moveToPoint(pointToMove);
                    duration -= Time.deltaTime;
                }
                else
                {
                    Debug.Log(pointToMove);
                    duration = .3f;
                    pointToMove = new Vector2(Random.Range(enemyRB.position.x - 1, enemyRB.position.x + 1), Random.Range(enemyRB.position.y - 1, enemyRB.position.y + 1));

                }
            }
            else
            {
                if (duration >= 0)
                {
                    pointToMove = new Vector2(Random.Range(player.position.x - 1, player.position.x + 1), Random.Range(player.position.y - 1, player.position.y + 1));
                    moveToPoint(pointToMove);
                    duration -= Time.deltaTime;

                }
                else
                {
                    pointToMove = new Vector2(Random.Range(player.position.x - 1, player.position.x + 1), Random.Range(player.position.y - 1, player.position.y + 1));
                    duration = .3f;
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
