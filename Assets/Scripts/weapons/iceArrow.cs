using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iceArrow : MonoBehaviour
{
    #region variables
    private int damage = 1;
    private float launchSpeed = 8.0f;

    private Rigidbody2D rb;

    private Vector2 target;
    private Vector2 moveDirection;

    private Camera cam;

    public GameObject glacier;
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
            col.transform.GetComponent<Enemy>().takeDamage(5, player);
            Destroy(gameObject);
            GameObject ice = Instantiate(glacier, rb.position, Quaternion.identity);
            Destroy(ice, 2f);
        }
    }
}
