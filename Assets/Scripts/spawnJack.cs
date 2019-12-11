using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnJack : MonoBehaviour
{
    public GameObject jack;
    static GameObject spawned;
    // Start is called before the first frame update
    void Start()
    {
        if (!spawned)
        {
            Vector3 pos = new Vector3(6.2f, .19f, -9.7f);
            spawned = Instantiate(jack, pos, Quaternion.identity);
            DontDestroyOnLoad(spawned);
            
        }
    }
}
