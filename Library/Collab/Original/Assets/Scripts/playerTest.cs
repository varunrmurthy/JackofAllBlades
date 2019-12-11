using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTest : MonoBehaviour
{
    #region UI_variables
    public GameObject fullHeart;
    public GameObject halfHeart;
    public GameObject emptyHeart;
    private GameObject UI;
    private GameObject[] hearts;
    #endregion
    #region sceneVariables
    private SceneController cont;
    private StoreController storeCont;
    #endregion

    #region currencyVariables
    public int currBalance = 0;
    public Text currencyText;
    #endregion

    #region weapons
    public weapons equippedWeapon;
    #endregion

    #region variables
    private float moveSpeed = 5f;
    public int maxHP = 5;
    public float HP;

    private Animator anim;

    private Camera cam;

    private Rigidbody2D rb;

    public Vector2 dir;
    private Vector2 mousePos;
    public Vector2 pov;
    #endregion

    void Awake()
    {
        
        UI = GameObject.Find("UI(Clone)");
        
        gameObject.GetComponent<halberd>().cooldownIconObject = GameObject.Find("HalberdCD");
        gameObject.GetComponent<daggers>().cooldownIconObject = GameObject.Find("DaggerCD");
        currencyText = GameObject.Find("Currency").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cont = GetComponent<SceneController>();
        equippedWeapon = gameObject.GetComponent<daggers>();
        cam = Camera.main;
        storeCont = gameObject.GetComponent<StoreController>();
        
        HP = maxHP;
        renderhealth();
    }

    void Update()
    {
        if (cam == null)
        {
            if (cont.currScene == "Shop")
            {
                storeCont.storeDisplay();
            }
            cam = Camera.main;
        }

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        face();

        #region weaponSwitch
        if (Input.GetKeyDown(KeyCode.P))
        {
            die();
            Debug.Log("Jack committed non-honorable seppuku.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedWeapon = gameObject.GetComponent<daggers>();
            Debug.Log("Equipped Daggers.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equippedWeapon = gameObject.GetComponent<halberd>();
            Debug.Log("Equipped Halberd.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

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
            
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            cont.battleScene();
            HP = maxHP;
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
    public void takeDamage(float damage)
    {
        HP = Mathf.Max(HP - damage, 0);
        renderhealth();
        Debug.Log("Player HP: " + HP.ToString() + ".");
        if (HP == 0)
        {
            die();
        }
    }

    void die()
    {
        Debug.Log("Player has died.");
        cont.ShopScene();
        HP = maxHP;
        renderUI();

    }
    #endregion
    #region ui_functions

    public void renderUI()
    {

        currencyText.text = currBalance.ToString();
        renderhealth();

    }
    public void renderhealth()
    {
        hearts = new GameObject[maxHP];
        int depth = -20;
        int i = 0;
        int horizontalPosition = 0;
        GameObject currHeart = fullHeart;
        while (i < maxHP)
        {
            if (i % 5 == 0)
            {
                depth -= 20;
                horizontalPosition = 0;
            }
            if (i == HP - .5f)
            {
                Debug.Log("changed");
                currHeart = halfHeart;
            }
            else if (i >= HP)
            {
                currHeart = emptyHeart;
            }
            hearts[i] = Instantiate(currHeart, UI.transform) as GameObject;
            hearts[i].transform.localPosition = new Vector3(-150 + horizontalPosition * 30, depth, 0);
            hearts[i].GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            hearts[i].GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            i++;
            horizontalPosition++;

        }
        //Instantiate()
        
    }
    #endregion
}