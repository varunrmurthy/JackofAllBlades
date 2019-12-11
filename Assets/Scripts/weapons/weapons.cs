using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapons : MonoBehaviour
{

    public float cooldown;
    public float ultCooldown;
    public float abilityCooldown;

    public bool[] cooldowns = { false, false, false };
    public GameObject cooldownIconObject;
    public Image cooldownImage;
    public GameObject cooldownIconObjectAbility;
    public Image cooldownImageAbility;
    public GameObject cooldownIconObjectUltimate;
    public Image cooldownImageUltimate;
    public IEnumerator FadeTo(float aValue, float aTime, int skill, Image cooldownImage)
    {
        cooldowns[skill] = true;
        cooldownImage.fillAmount = 0;
        for (float t = 0; t < aTime; t += Time.deltaTime)
        {
            cooldownImage.fillAmount += 1 / aTime * Time.deltaTime;
            yield return null;
        }
        cooldowns[skill] = false;
    }
}