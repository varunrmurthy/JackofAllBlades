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
        DontDestroyOnLoad(this);
    }
    public void ShopScene()
    {
        SceneManager.LoadScene("Shop");
        currScene = "Shop";
    }
    public void battleScene()
    {

        SceneManager.LoadScene("Battlefield");
        currScene = "Battle";
        if (!spawned)
        {
            spawned = Instantiate(UI) as GameObject;
        } 
    }
}
