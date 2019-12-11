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
    public bool boss = false;
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
    public Color og_color;
    public bool hurt = false;
    protected bool beingPushed = false;
    #endregion
    #region unity_functions
    public void Awake()
    {
        dropChance.Add(drop1, drop1Chance);
        enemyRB = GetComponent<Rigidbody2D>();
        pointToMove = new Vector2(Random.Range(enemyRB.position.x - .5f, enemyRB.position.x + .5f), Random.Range(enemyRB.position.y - .5f, enemyRB.position.y + .5f));
        moveToPoint(pointToMove);
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        og_color = spr.color;
    }
    #endregion
    #region drop_functions
    protected void drop() 
    {
        float curr = 0f;
        float roll = Random.Range(0f, 1f);
        foreach(GameObject o in dropChance.Keys) {
            if (roll > curr && roll < curr + (float)dropChance[o])
            {
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
        if (!p) currHealth -= player.GetComponent<player>().might;
        else currHealth -= p.GetComponent<player>().might;
        StartCoroutine(HurtAnimation());
        
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
    IEnumerator HurtAnimation()
    {
        if (!hurt) {
            hurt = true;
            float elapsed_time = 0.0f;
            SpriteRenderer spr = GetComponent<SpriteRenderer>();
            while (spr.color != Color.red)
            {
                elapsed_time += Time.deltaTime;
                spr.color = Color.Lerp(spr.color, Color.red, elapsed_time / 0.25f);
                yield return null;
            }
            elapsed_time = 0;
            while (spr.color != og_color)
            {
                elapsed_time += Time.deltaTime;
                spr.color = Color.Lerp(spr.color, og_color, elapsed_time / 0.25f);
                yield return null;
            }
        }
        hurt = false;
    }
    IEnumerator knockback(Transform player)
    {
        if (!boss) {
            beingPushed = true;
            this.GetComponent<Rigidbody2D>().velocity = (new Vector2((player.position.x - enemyRB.position.x), player.position.y - enemyRB.position.y).normalized * -7);
            yield return new WaitForSeconds(.1f);

            this.GetComponent<Rigidbody2D>().velocity *= 0f;
            beingPushed = false;
        }
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
