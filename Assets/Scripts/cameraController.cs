using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<player>().transform;
    }

    void Update()
    {
        if (player != null) transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -20); // Camera follows the player with specified offset position
    }
}
