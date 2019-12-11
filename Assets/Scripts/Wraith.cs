using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith : Enemy
{

    public GameObject enemyHitboxx;

    #region Unity_functions
    private new void Awake()
    {
        base.Awake();
        currHealth = maxHealth;
        hitRadius = 2.0f;
        timeBetweenAttacks = 0.0f;
        damageDealt = 1;
        canAttack = false;
        attacking = false;
    }
    private void Update()
    {
        
        timeBetweenAttacks -= Time.deltaTime;
        if ((!attacking) && canAttack && timeBetweenAttacks <= 0.0f && Vector2.SqrMagnitude(new Vector2(enemyRB.transform.position.x - player.position.x, enemyRB.transform.position.y - player.position.y)) < 3)
        {
            //Wraith will stop moving.
            Vector2 direction = player.position - transform.position;
            enemyRB.velocity = direction.normalized * 0.0f;
            //Wraith will attack and reset its timer.
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
    #endregion

    #region attack_functions
    IEnumerator Attack()
    {
        //Wraith winds up for circular scythe attack.
        float elapsed_time = 0.0f;
        while (elapsed_time <= .5f)
        {
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        //Play SFX
        //FindObjectOfType<AudioManager>().Play("HitSFX");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, hitRadius, Vector2.zero);
        SpawnAndDestroys(enemyHitboxx, 0.25f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<player>().takeDamage(damageDealt);
            }
        }
        
        timeBetweenAttacks = 1.0f;
        attacking = false;
    }

    void SpawnAndDestroys(GameObject prefab, float delay)
    {
        Vector3 testt;
        testt.x = enemyRB.position.x;
        testt.y = enemyRB.position.y;
        testt.z = 0;
        GameObject newg = Instantiate(prefab, testt, Quaternion.identity, enemyRB.transform);
        Destroy(newg, delay);
    }
    #endregion

}
