using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outskirtsTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Jack is trying to enter the castle outskirts.");
        if (coll.CompareTag("Player") && GameObject.FindWithTag("Enemy") == null)
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<SceneController>().OutskirtsScene();
        }
    }
}
