using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitBall : MonoBehaviour
{
    float launchSpeed = 9f;
    Rigidbody2D rb;
    int damageDealt = 1;

    player target;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<player>();
        Debug.Log(target.transform.position);
        moveDirection = (target.transform.position - transform.position).normalized * launchSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3.5f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Jack with spit ball!");
            col.transform.GetComponent<player>().takeDamage(damageDealt);
            Destroy(gameObject);
        }
        
    }
}
