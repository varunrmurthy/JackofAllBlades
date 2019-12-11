using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopToCastleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameObject[] texts = coll.GetComponent<StoreController>().texts;
            for (int i = 0; i < 3; i++)
            {
                Destroy(texts[i]);
            }
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<SceneController>().castleScene();
        }
    }
}
