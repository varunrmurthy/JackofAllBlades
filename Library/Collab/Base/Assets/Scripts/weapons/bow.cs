using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bow : MonoBehaviour, weapons
{
    #region variables
    private bool ult;

    public GameObject arrow;
    public GameObject trueArrow;
    public GameObject iceArrow;

    private Rigidbody2D wielder;
    #endregion

    private void Awake()
    {
        wielder = GetComponent<Rigidbody2D>();
    }

    public void basicAttack() // Low damage, weak knockback, and low cd --> weak but that's because it's long range
    {
        if (ult)
        {
            Instantiate(trueArrow, wielder.position, Quaternion.identity);
        } else
        {
            Instantiate(arrow, wielder.position, Quaternion.identity);
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
