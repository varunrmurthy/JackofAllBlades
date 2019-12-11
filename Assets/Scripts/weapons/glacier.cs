using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glacier : MonoBehaviour
{
    #region variables
    private int damage = 3;
    private Transform player;
    #endregion

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.transform.GetComponent<Enemy>().takeDamage(10, player);
        }
    }
}
