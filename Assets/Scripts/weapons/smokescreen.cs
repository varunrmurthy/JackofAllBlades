using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokescreen : MonoBehaviour
{
    #region variables
    private GameObject player;

    private float timer = 2;
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer -= Time.deltaTime;
    }

}
