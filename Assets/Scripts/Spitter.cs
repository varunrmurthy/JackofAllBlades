using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
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
        if ((!attacking) && canAttack && timeBetweenAttacks <= 0.0f)
        {
            Vector2 direction = player.position - transform.position;
            enemyRB.velocity = direction.normalized * 0.0f;
            attacking = true;
            StartCoroutine(Attack());
        }
    }
    #endregion



    #region attack_functions
    IEnumerator Attack()
    {
        Vector2 direction = player.position - transform.position;
        currDirection = direction;
        float elapsed_time = 0.0f;
        while (elapsed_time <= 0.5f)
        {
            elapsed_time += Time.deltaTime;
            yield return null;
        }
        Instantiate(SpitBall, transform.position, Quaternion.identity);
        timeBetweenAttacks = 3.5f;
        attacking = false;
    }
    #endregion
}
