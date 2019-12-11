using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossChamberTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && GameObject.FindWithTag("Enemy") == null)
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<SceneController>().winScene();
            GameObject player_obj = GameObject.FindWithTag("Player");
            Destroy(player_obj);
        }
    }
}
