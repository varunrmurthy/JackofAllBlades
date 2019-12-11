using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonTargetingBeam : MonoBehaviour
{
    float launchSpeed = 8.0f;
    Rigidbody2D rb;
    int damageDealt = 2;

    player target;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<player>();
        moveDirection = new Vector2(1.0f, 0.0f).normalized * launchSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3.5f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Jack with non-targeting blade beam!");
            col.transform.GetComponent<player>().takeDamage(damageDealt);
            Destroy(gameObject);
        }

    }
}
