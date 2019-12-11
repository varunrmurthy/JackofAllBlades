using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject UI;
    public static GameObject spawned;
    public string currScene;
    void Start()
    {
        currScene = "Intro";
    }

    public void ShopScene()
    {
        currScene = "Shop";
        SceneManager.LoadScene("Shop");
        
    }
    public void BridgeScene()
    {
        currScene = "Bridge";
        SceneManager.LoadScene("Bridge");
        
        if (!spawned)
        {
            spawned = Instantiate(UI) as GameObject;
        }
    }
    public void OutskirtsScene()
    {
        currScene = "Outskirts";
        SceneManager.LoadScene("Outskirts");
        
        if (!spawned)
        {
            spawned = Instantiate(UI) as GameObject;
        }
    }
    public void battleScene()
    {
        currScene = "Battle";
        SceneManager.LoadScene("Battlefield");
        
        if (!spawned)
        {
            spawned = Instantiate(UI) as GameObject;
            //
        } 
    }
    public void castleScene()
    {
        currScene = "Master Map";
        if (spawned)
        {
            GameObject.FindObjectOfType<player>().currScene = "Master Map";
        }
        SceneManager.LoadScene("Master Map");
        if (!spawned)
        {
            spawned = Instantiate(UI) as GameObject;
        }
    }
    public void bossScene()
    {
        currScene = "Boss";
        if (spawned)
        {
            GameObject.FindObjectOfType<player>().currScene = "Boss";
        }
        SceneManager.LoadScene("Boss Chamber");

        if (!spawned)
        {
            spawned = Instantiate(UI) as GameObject;
        }
    }
    public void winScene()
    {
        currScene = "Winner";
        UI = null;
        SceneManager.LoadScene("Winner");
    }

}
