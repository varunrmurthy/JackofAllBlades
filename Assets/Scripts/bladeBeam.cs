using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeBeam : MonoBehaviour
{
    float launchSpeed = 8.0f;
    Rigidbody2D rb;
    int damageDealt = 1;

    player target;
    Vector2 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<player>();
        moveDirection = (target.transform.position - transform.position).normalized * launchSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3.5f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Jack with targeted blade beam!");
            col.transform.GetComponent<player>().takeDamage(damageDealt);
            Destroy(gameObject);
        }

    }
}
