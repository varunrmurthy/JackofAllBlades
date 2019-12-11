using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    #region movement_variables
    public float movespeed;
    #endregion

    #region physics_components
    Rigidbody2D enemyRB;
    #endregion

    #region targeting_variable
    public Transform player;
    #endregion

    #region health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region attack_variables
    public int damage;
    public float hitRadius;
    public float timeBetweenAttacks;
    public bool canAttack;
    #endregion

    #region Unity_functions
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        movespeed = 0.25f;
        currHealth = maxHealth;
        timeBetweenAttacks = 0.0f;
        canAttack = false;
    }
    private void Update()
    {
        if (player == null)
        {
            enemyRB.velocity = transform.position * 0.0f;
            return;
        }
        timeBetweenAttacks -= Time.deltaTime;
        if (canAttack && timeBetweenAttacks <= 0.0f) {
            Debug.Log("Dummy will now attempt to attack.");
            //Dummy will stop moving.
            Vector2 direction = player.position - transform.position;
            enemyRB.velocity = direction.normalized * 0.0f;
            //Dummy will attack and reset its attack timer.
            Attack();
            timeBetweenAttacks = 5.0f;
        } else
        {
            Move();
        }
    }
    #endregion

    #region movement_functions
    private void Move()
    {
        Debug.Log(player);
        //Calculate the movement vector. 
        Vector2 direction = player.position - transform.position;
        enemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region attack_functions
    private void Attack()
    {
        //Play SFX
        //FindObjectOfType<AudioManager>().Play("HitSFX");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, hitRadius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                //Cause damage.
                Debug.Log("Hit player with attack.");
                //hit.transform.GetComponent<Player>().TakeDamage(damage);
                hit.transform.GetComponent<playerTest>().takeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Dummy can attack now.");
        player = collision.transform;
        canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Dummy can no longer attack now.");
        player = null;
        canAttack = false;
    }
    #endregion

    #region health_functions
    public void TakeDamage(float value)
    {
        //Play SFX and decrement health.
        //FindObjectOfType<AudioManager>().Play("KnightHurt");
        currHealth -= value;
        Debug.Log("Enemy Health is now " + currHealth.ToString());

        //Check for death.
        if (currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
