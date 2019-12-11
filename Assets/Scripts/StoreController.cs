using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreController : MonoBehaviour
{
    #region game_variables
    
    GameObject[] inventory = new GameObject[3];
    public GameObject[] texts = new GameObject[3];
    public GameObject heart;
    public GameObject might;
    public GameObject text;
    #endregion
    #region unity_functions
    // Start is called before the first frame update
    public void storeDisplay()
    {
        Transform cam = Camera.main.transform;
        inventory[0] = might;
        inventory[1] = heart;
        inventory[2] = heart;
        Transform UIPos = transform.GetComponent<player>().UI.transform;
        for (int i = 0; i < inventory.Length; i++)
        {

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(.15f * i + .3f, .5f, Camera.main.nearClipPlane));
            Instantiate(inventory[i], pos, Quaternion.identity);
            text.GetComponent<Text>().text = inventory[i].GetComponent<ShopItem>().cost.ToString();
            texts[i] = Instantiate(text, new Vector3(pos.x, pos.y - 1, 90), UIPos.rotation, UIPos) as GameObject;
            

        }
    }
    #endregion
}
