using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    #region movement_variables
    public float movespeed;
    #endregion

    #region physics_components
    Rigidbody2D enemyRB;
    public GameObject SpitBall;
    #endregion

    #region Unity_functions
    private new void Awake()
    {
        base.Awake();
        enemyRB = GetComponent<Rigidbody2D>();
        movespeed = 0.25f;
        currHealth = maxHealth;
        hitRadius = 1;
        timeBetweenAttacks = 0.0f;
        damageDealt = 1;
        canAttack = false;
        attacking = false;
    }
    private void Update()
    {
        if (player == null)
        {
            enemyRB.velocity = transform.position * 0.0f;
            return;
        }
        timeBetweenAttacks -= Time.deltaTime;
        if ((!attacking) && canAttack && timeBetweenAttacks <= 0.0f)
        {
            Debug.Log("Slime will now attempt to attack.");
            //Slime will stop moving.
            Vector2 direction = player.position - transform.position;
            enemyRB.velocity = direction.normalized * 0.0f;
            //Slime will attack and reset its timer.
            attacking = true;
            StartCoroutine(Attack());
        }
        else if (!attacking)
        {
            Move();
        }
    }
    #endregion

    #region movement_functions
    private void Move()
    {
        if (!beingPushed)
        {
            //Calculate the movement vector. 
            Vector2 direction = player.position - transform.position;
            currDirection = direction;
            enemyRB.velocity = direction.normalized * movespeed;
        }
    }
    #endregion

    #region attack_functions
    IEnumerator Attack()
    {
        Vector2 direction = player.position - transform.position;
        currDirection = direction;
        //Slime winds up.
        Debug.Log("Slime prepares to attack!");
        float elapsed_time = 0.0f;
        while (elapsed_time <= 1.0f)
        {
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        //Play SFX
        //FindObjectOfType<AudioManager>().Play("HitSFX");

        int attack_determiner = Random.Range(0, 9);
        if (attack_determiner <= -1)
        {
            Debug.Log("Slime attacks with spitball!");
            Instantiate(SpitBall, transform.position, Quaternion.identity);
        } else
        {
            Debug.Log("Slime attacks with headbutt!");
            //RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, hitRadius, Vector2.zero);
            Vector2 hitbox = new Vector2(0.5f, 0.5f);
            RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)transform.position, hitbox, 0f, currDirection, 1f);
            ExtDebug.DrawBoxCast2D((Vector2)transform.position, hitbox, 0f, currDirection, 1f, Color.red, 1f);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("Hit player with headbutt in direction " + currDirection);
                    hit.transform.GetComponent<playerTest>().takeDamage(damageDealt);
                }
            }
        }
        timeBetweenAttacks = 5.0f;
        attacking = false;
    }
    #endregion

}
