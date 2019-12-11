using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    #region movement_variables
    
    #endregion

    #region physics_components
    
    public GameObject SpitBall;
    #endregion

    #region Unity_functions
    private new void Awake()
    {
        base.Awake();
        currHealth = maxHealth;
        hitRadius = 1;
        timeBetweenAttacks = 0.0f;
        damageDealt = 1;
        canAttack = false;
        attacking = false;
    }
    private void Update()
    {
        timeBetweenAttacks -= Time.deltaTime;
        if ((!attacking) && canAttack && timeBetweenAttacks <= 0.0f && Vector2.SqrMagnitude(new Vector2(enemyRB.transform.position.x - player.position.x, enemyRB.transform.position.y - player.position.y)) < 1.5)
        {
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

    

    #region attack_functions
    IEnumerator Attack()
    {
        Vector2 direction = player.position - transform.position;
        currDirection = direction;
        //Slime winds up.
        float elapsed_time = 0.0f;
        while (elapsed_time <= .5f)
        {
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        //Play SFX
        //FindObjectOfType<AudioManager>().Play("HitSFX");

        int attack_determiner = Random.Range(0, 9);
        if (attack_determiner <= .3)
        {
            Instantiate(SpitBall, transform.position, Quaternion.identity);
        } else
        {
            Vector2 hitbox = new Vector2(1.5f, 1.5f);
            RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)transform.position, hitbox, 0f, currDirection, 1f);
            ExtDebug.DrawBoxCast2D((Vector2)transform.position, hitbox, 0f, currDirection, 1f, Color.red, 1f);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    hit.transform.GetComponent<player>().takeDamage(damageDealt);
                }
            }
        }
        timeBetweenAttacks = 1.0f;
        attacking = false;
    }

    public void increaseDamage()
    {
        damageDealt += 1;
    }
    #endregion

}
