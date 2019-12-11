using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class halberd : weapons, skills
{
    #region variables
    private int damage = 4;

    public bool equipped;
    private Rigidbody2D wielder;    
    

    public GameObject hitbox;
    #endregion


    void Awake()
    {
        wielder = GetComponent<Rigidbody2D>();

        equipped = false;
        cooldownIconObject = GameObject.Find("HalberdCD");
        cooldownImage = cooldownIconObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (equipped)
        {
            cooldownImage.color = Color.yellow;
        }
        else
        {
            cooldownImage.color = Color.white;
        }
    }

    public void basicAttack() // Stronger knockback and medium cooldown --> slow but CC
    {
        if (!cooldowns[0])
        {
            int sound_determiner = Random.Range(0, 9);
            if (sound_determiner <= 4)
            {
                SoundManagerScript.PlaySound("ATK1");
            }
            else
            {
                SoundManagerScript.PlaySound("ATK2");
            }
            RaycastHit2D[] hits = Physics2D.CircleCastAll(wielder.position, 2.0f, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().takeDamage(damage + wielder.GetComponent<player>().might);
                }
            }
            SpawnAndDestroy(hitbox, 0.25f);
            StartCoroutine(FadeTo(1, cooldown, 0, cooldownImage));
        }
    }
    public void ability() // cd: 10f
    {
        SoundManagerScript.PlaySound("Skill");
        RaycastHit2D[] ultHits = Physics2D.CircleCastAll(wielder.position, 4.0f, Vector2.zero);
        foreach (RaycastHit2D hit in ultHits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().takeDamage(damage + 6 + wielder.GetComponent<player>().might);
            }
        }
        SpawnAndDestroy(hitbox, 0.25f);
    }

    public void ultimate() // cd: 15f
    {
        int count = 0;
        if (count == 3)
        {
            SoundManagerScript.PlaySound("Skill");
            RaycastHit2D[] abilityHits = Physics2D.CircleCastAll(wielder.position, 3.0f, Vector2.zero);
            foreach (RaycastHit2D hit in abilityHits)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().takeDamage(damage + wielder.GetComponent<player>().might);
                }
            }
            count = 0;
            SpawnAndDestroy(hitbox, 0.25f);
            // put CD thingy here
        }
        else
        {
            SoundManagerScript.PlaySound("Skill");
            RaycastHit2D[] abilityHits = Physics2D.CircleCastAll(wielder.position, 3.0f, Vector2.zero);
            foreach (RaycastHit2D hit in abilityHits)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().takeDamage(damage);
                }
            }
            count += 1;
            SpawnAndDestroy(hitbox, 0.25f);
        }
        
    }

    void SpawnAndDestroy(GameObject prefab, float delay)
    {
        GameObject newGO = Instantiate(prefab, wielder.transform);
        Destroy(newGO, delay);
    }
}
