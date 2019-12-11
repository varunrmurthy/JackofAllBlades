using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bow : weapons, skills
{
    #region variables
    private bool ult;
    public bool equipped;
    public GameObject arrow;
    public GameObject trueArrow;
    public GameObject iceArrow;
    private Rigidbody2D wielder;
    #endregion
    #region cooldown
   
    #endregion
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
    private void Awake()
    {
        wielder = GetComponent<Rigidbody2D>();
        
        equipped = false;
        cooldownIconObject = GameObject.Find("BowCD");
        cooldownImage = cooldownIconObject.GetComponent<Image>();
        cooldownIconObjectAbility = GameObject.Find("BowAbilityCD");
        cooldownImageAbility = cooldownIconObjectAbility.GetComponent<Image>();
        cooldownIconObjectUltimate = GameObject.Find("BowUltimateCD");
        cooldownImageUltimate = cooldownIconObjectUltimate.GetComponent<Image>();
    }

    // cd: 1s | prefab: arrow
    public void basicAttack()
    {
        if (!cooldowns[0])
        {
            if (ult)
            {
                SoundManagerScript.PlaySound("Skill");
                Instantiate(trueArrow, wielder.position, Quaternion.identity);
            }
            else
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
                GameObject a = Instantiate(arrow, wielder.position, Quaternion.identity);
                Destroy(a, 2f);
            }
            StartCoroutine(FadeTo(1f, cooldown, 0, cooldownImage));
        }
    }

    // cd: 7s | prefab: iceArrow & glacier
    public void ability()
    {
        if (!cooldowns[1])
        {
            SoundManagerScript.PlaySound("Skill");
            Instantiate(iceArrow, wielder.position, Quaternion.identity);
            StartCoroutine(FadeTo(1f, 7f, 1, cooldownImageAbility));
        }
    }

    // cd: 20s | prefab: trueArrow
    public void ultimate()
    {
        if (!cooldowns[2])
        {
            SoundManagerScript.PlaySound("Skill");
            StartCoroutine(trueArrows());
            StartCoroutine(FadeTo(1f, 20f, 2, cooldownImageUltimate));
        }
    }

    IEnumerator trueArrows()
    {
        ult = true;
        yield return new WaitForSeconds(5);
        ult = false;
    }
}
