using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Jack is trying to enter the castle courtyard (Battlefield).");
        if (coll.CompareTag("Player") && GameObject.FindWithTag("Enemy") == null)
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<SceneController>().battleScene();
        }
    }
}
