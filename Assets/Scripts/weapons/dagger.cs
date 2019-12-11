using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dagger : weapons, skills
{
    #region variables
    private int damage = 7;
    public bool equipped = true;

    private Rigidbody2D wielder;
    private player player;

    public GameObject kunai;
    public GameObject smokescreen;

    private Vector2 mousePos;
    private Vector2 attackDir;
    #endregion

    #region cooldownVariables
    

    #endregion


    void Awake()
    {
        wielder = GetComponent<Rigidbody2D>();
        player = GetComponent<player>();

        cooldownIconObject = GameObject.Find("DaggerCD");
        cooldownImage = cooldownIconObject.GetComponent<Image>();
        cooldownIconObjectAbility = GameObject.Find("DaggerAbilityCD");
        cooldownImageAbility = cooldownIconObjectAbility.GetComponent<Image>();
        cooldownIconObjectUltimate = GameObject.Find("DaggerUltimateCD");
        cooldownImageUltimate = cooldownIconObjectUltimate.GetComponent<Image>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attackDir = (mousePos - wielder.position).normalized;

        if (equipped)
        {
            cooldownImage.color = Color.yellow;
        }
        else
        {
            cooldownImage.color = Color.white;
        }
    }

    public void basicAttack()
    {
        if (!cooldowns[0])
        {
            StartCoroutine(jab());
            int sound_determiner = Random.Range(0, 9);
            if (sound_determiner <= 4)
            {
                SoundManagerScript.PlaySound("ATK1");
            }
            else
            {
                SoundManagerScript.PlaySound("ATK2");
            }
            RaycastHit2D[] hits = Physics2D.BoxCastAll(wielder.position, Vector2.one, 0f, attackDir, 1f);
            ExtDebug.DrawBoxCast2D(wielder.position, Vector2.one, 0f, attackDir, 1f, Color.blue, 1f);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Enemy"))
                {               
                    hit.transform.GetComponent<Enemy>().takeDamage(damage);
                    damage = 7;
                }
            }
            StartCoroutine(FadeTo(1f, cooldown, 0, cooldownImage));
        }
    }

    public void ability()
    {
        if (!cooldowns[1])
        {
            SoundManagerScript.PlaySound("Skill");
            GameObject shadow = Instantiate(kunai, wielder.position, Quaternion.identity); // fix rotation
            StartCoroutine(FadeTo(1f, 5f, 1, cooldownImageAbility));
        }
    }

    public void ultimate()
    {
        if (!cooldowns[2])
        {
            SoundManagerScript.PlaySound("Skill");
            GameObject shroud = Instantiate(smokescreen, wielder.position, Quaternion.identity);
            damage = 20;
            Destroy(shroud, 3f);
            StartCoroutine(FadeTo(1f, 10f, 2, cooldownImageUltimate));
        }
    }

    #region helperFunctions
    public void teleport(Vector2 v)
    {
        wielder.transform.position = v;
    }

    IEnumerator jab()
    {
        player.anim.SetFloat("daggerJab", 1.5f);
        yield return new WaitForSeconds(0.5f);
        player.anim.SetFloat("daggerJab", 0.5f);
    }
    #endregion
}
