using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alanPlayer : MonoBehaviour
{
    #region sceneVariables
    private SceneController cont;
    private StoreController storeCont;
    #endregion

    /* #region currencyVariables
    public int currBalance = 0;
    public Text currencyText;
    #endregion */

    #region weapons
    private skills equippedWeapon;
    #endregion

    #region variables
    private float moveSpeed = 5f;
    private float maxHP = 5f;
    private float HP;

    // public Slider healthBar;

    private Animator anim;

    private Camera cam;

    private Rigidbody2D rb;

    private Vector2 dir;
    private Vector2 mousePos;
    private Vector2 pov; // private to avoid clashing with playerTest.cs
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cont = GetComponent<SceneController>();
        equippedWeapon = gameObject.GetComponent<dagger>();
        cam = Camera.main;
        HP = maxHP;
        // healthBar.maxValue = maxHP;
        // healthBar.value = maxHP;
    }

    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        face();

        #region weaponSwitch
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedWeapon = gameObject.GetComponent<dagger>();
            Debug.Log("Equipped Daggers.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equippedWeapon = gameObject.GetComponent<halberd>();
            Debug.Log("Equipped Halberd.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equippedWeapon = gameObject.GetComponent<bow>();
            Debug.Log("Equipped Bow.");
        }
        #endregion

        #region weaponAttack
        if (Input.GetButtonDown("Fire1"))
        {
            equippedWeapon.basicAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        #endregion

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.MovePosition(rb.position + dir * moveSpeed * 2);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    private void face()
    {
        pov = mousePos - rb.position;
        anim.SetFloat("xAxis", pov.x);
        anim.SetFloat("yAxis", pov.y);
    }

    #region healthMethods
    public void takeDamage(int damage)
    {
        HP -= damage;
        // healthBar.value = HP;
        Debug.Log("Player HP: " + HP.ToString() + ".");
        if (HP <= 0)
        {
            die();
        }
    }

    void die()
    {
        Debug.Log("Player has died.");
        // cont.ShopScene();
    }
    #endregion
}