using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartItem: ShopItem
{
    
    #region shop_functions
    public int getCost()
    {
        return cost;
    }
    public void onBuy(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player stats = collider.gameObject.GetComponent<player>();
            if (stats.currBalance >= cost)
            {
                stats.maxHP += 1;
                stats.HP = stats.maxHP;
                stats.currBalance -= cost;
                stats.renderUI();
                Destroy(this.gameObject);
                Debug.Log("Successfully Bought");
            }
            else
            {
                Debug.Log("Not Enough Money");
            }
        }
    }
    #endregion
    private void Start()
    {
        cost = 3;
        Debug.Log("initiated: Heart");
    }
    #region unity_functions
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Player Tried to Buy");
        onBuy(collider);
        
    }
    #endregion

}
