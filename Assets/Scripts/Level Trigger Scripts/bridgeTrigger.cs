using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Jack is trying to exit the shop.");
        if (coll.CompareTag("Player"))
        {
            
            coll.GetComponent<SceneController>().BridgeScene();
            coll.transform.position = new Vector3(6.2f, .19f, coll.transform.position.z);
            GameObject[] texts = coll.GetComponent<StoreController>().texts;
            for (int i = 0; i < 3; i++) {
                Destroy(texts[i]);
            }
        }
    }
}
