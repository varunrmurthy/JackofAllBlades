using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kunai : MonoBehaviour
{
    #region variables
    private int damage = 7;
    private float launchSpeed = 8.0f;

    private Rigidbody2D rb;

    private Vector2 mousePos;

    private Camera cam;

    private GameObject player;
    private Transform pTransform;
    #endregion

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.velocity = (mousePos - rb.position).normalized * launchSpeed;
        pTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.transform.GetComponent<Enemy>().takeDamage(damage, pTransform);
            player.GetComponent<dagger>().teleport(rb.position);
            Destroy(gameObject);
        }
    }
}
