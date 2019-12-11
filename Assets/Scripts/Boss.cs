using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    #region movement_variables
    public float movespeed;
    public GameObject circleAttack;
    #endregion

    #region physics_components
    Rigidbody2D enemyRB;
    public GameObject bladeBeam;
    public GameObject nonTargetingBeam;
    #endregion

    #region spawner_objects
    public GameObject slime;
    public bool just_spawned;
    #endregion

    #region Unity_functions
    private new void Awake()
    {
        
        base.Awake();
        boss = true;
        enemyRB = GetComponent<Rigidbody2D>();
        movespeed = 0.2f;
        currHealth = maxHealth;
        hitRadius = 5.0f;
        timeBetweenAttacks = 0.0f;
        damageDealt = 1;
        just_spawned = false;
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
            Vector2 direction = player.position - transform.position;
            enemyRB.velocity = direction.normalized * 0.0f;
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
        //Calculate the movement vector. 
        Vector2 direction = player.position - transform.position;
        currDirection = direction;
        enemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region attack_functions
    IEnumerator knockback(Transform player)
    {
        yield return true;
    }
    IEnumerator Attack()
    {
        int attack_determiner = Random.Range(0, 9);
        while ((attack_determiner > 7) && (just_spawned == true)){
            attack_determiner = Random.Range(0, 9);
        }
        just_spawned = false;
        if (attack_determiner <= 2)
        {
            //Boss performs a circular attack.
            float elapsed_time = 0.0f;
            while (elapsed_time <= 3.0f)
            {
                elapsed_time += Time.deltaTime;
                yield return null;
            }
            SpawnAndDestroys(circleAttack, 0.25f);
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, hitRadius, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    hit.transform.GetComponent<player>().takeDamage(3);
                }
            }
            timeBetweenAttacks = 2.0f;
            attacking = false;
        } else if (attack_determiner <= 3)
        {
            //Boss performs a triple blade beam attack that targets Jack.
            float elapsed_time = 0.0f;
            while (elapsed_time <= 2.5f)
            {
                elapsed_time += Time.deltaTime;
                yield return null;
            }
            Vector3 center_beam = transform.position;
            Vector3 lower_beam = new Vector3 (transform.position.x, transform.position.y - 5.0f, transform.position.z);
            Vector3 upper_beam = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
            Instantiate(bladeBeam, center_beam, Quaternion.identity);
            Instantiate(bladeBeam, lower_beam, Quaternion.identity);
            Instantiate(bladeBeam, upper_beam, Quaternion.identity);
            timeBetweenAttacks = 3.0f;
            attacking = false;
        } else if (attack_determiner <= 6)
        {
            //Boss performs a double blade beam attack that sweeps the field.
            float elapsed_time = 0.0f;
            while (elapsed_time <= 2.5f)
            {
                elapsed_time += Time.deltaTime;
                yield return null;
            }
            Vector3 lower_beam = new Vector3(transform.position.x, transform.position.y - 3.0f, transform.position.z);
            Vector3 upper_beam = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
            Instantiate(nonTargetingBeam, lower_beam, Quaternion.identity);
            elapsed_time = 0.0f;
            while (elapsed_time <= 1.5f)
            {
                elapsed_time += Time.deltaTime;
                yield return null;
            }
            Instantiate(nonTargetingBeam, upper_beam, Quaternion.identity);
            timeBetweenAttacks = 2.0f;
            attacking = false;
        } else
        {
            Vector3 center_minion = new Vector3(transform.position.x + 3.0f, transform.position.y, transform.position.z);
            Vector3 lower_minion = new Vector3(transform.position.x + 3.0f, transform.position.y - 1.25f, transform.position.z);
            Vector3 upper_minion = new Vector3(transform.position.x + 3.0f, transform.position.y + 1.25f, transform.position.z);
            Instantiate(slime, lower_minion, Quaternion.identity);
            Instantiate(slime, upper_minion, Quaternion.identity);
            Instantiate(slime, center_minion, Quaternion.identity);
            timeBetweenAttacks = 6.0f;
            just_spawned = true;
            attacking = false;
        }
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
