using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Start is called before the first frame update
    public int value;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1");
        if (other.transform.CompareTag("Player"))
        {
            player p = other.GetComponent<player>();
            p.currBalance += 1;
            p.renderUI();
            Destroy(this.gameObject);

        }
    }
}
