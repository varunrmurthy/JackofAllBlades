using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrow : MonoBehaviour
{
    #region variables
    private int damage = 5;
    private float launchSpeed = 8.0f;

    private Rigidbody2D rb;

    private Vector2 target;
    private Vector2 moveDirection;

    private Camera cam;
    private Transform player;
    #endregion

    void Awake()
    {
        cam = Camera.main;
        target = cam.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        moveDirection = (target - rb.position).normalized * launchSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Arrow hitbox registered.");
            col.transform.GetComponent<Enemy>().takeDamage(damage, player);
            Destroy(gameObject);
        }
    }
}
