using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bow : MonoBehaviour, weapons
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
    private bool onCooldown = false;
    public float cooldown;

    public GameObject cooldownIconObject;
    private Image cooldownImage;
    IEnumerator FadeTo(float aValue, float aTime)
    {
        onCooldown = true;
        cooldownImage.fillAmount = 0;
        for (float t = 0; t < aTime; t += Time.deltaTime)
        {
            cooldownImage.fillAmount += 1 / aTime * Time.deltaTime;
            yield return null;
        }
        onCooldown = false;
    }
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
        cooldownImage = cooldownIconObject.GetComponent<Image>();
        equipped = false;
    }

    public void basicAttack() // Low damage, weak knockback, and low cd --> weak but that's because it's long range
    {
        if (!onCooldown)
        {
            if (ult)
            {
                Instantiate(trueArrow, wielder.position, Quaternion.identity);
            }
            else
            {
                Instantiate(arrow, wielder.position, Quaternion.identity);
            }
            StartCoroutine(FadeTo(1f, cooldown));
        }
    }

    public void ability() // Fire a hail of arrows that barrages an area after a brief delay
    {
        Instantiate(iceArrow, wielder.position, Quaternion.identity);
    }

    public void ultimate() // For 5 seconds, basic attacks fire true arrows that pierce through everything, dealing massive damage
    {
        StartCoroutine(trueArrows());
    }

    IEnumerator trueArrows()
    {
        ult = true;
        yield return new WaitForSeconds(5);
        ult = false;
    }
}
