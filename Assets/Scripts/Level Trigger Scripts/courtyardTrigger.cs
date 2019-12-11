using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courtyardTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            coll.transform.position = new Vector3(3.9f, 0.01f, coll.transform.position.z);
            coll.GetComponent<player>().HP = coll.GetComponent<player>().maxHP;
            coll.GetComponent<player>().renderhealth();
            gm.GetComponent<SceneController>().bossScene();
        }
    }
}
