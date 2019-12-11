using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    #region UI_variables
    public GameObject fullHeart;
    public GameObject halfHeart;
    public GameObject emptyHeart;
    public GameObject UI;
    private GameObject[] hearts = new GameObject[0];
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
    dagger dagger;
    halberd halberd;
    bow bow;
    #endregion

    #region variables
    bool dying = false;
    public int might = 0;
    private float moveSpeed = 5f;
    public int maxHP = 5;
    public float HP;

    private Animator anim;

    private Camera cam;

    public Rigidbody2D rb;

    public Vector2 dir;
    private Vector2 mousePos;
    public Vector2 pov;
    #endregion

    void Awake()
    {
        UI = GameObject.Find("UI(Clone)");
        gameObject.GetComponent<halberd>().cooldownIconObject = GameObject.Find("HalberdCD");
        gameObject.GetComponent<dagger>().cooldownIconObject = GameObject.Find("DaggerCD");
        gameObject.GetComponent<bow>().cooldownIconObject = GameObject.Find("BowCD");
        dagger = gameObject.GetComponent<dagger>();
        halberd = gameObject.GetComponent<halberd>();
        bow = gameObject.GetComponent<bow>();
        currencyText = GameObject.Find("Currency").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cont = GetComponent<SceneController>();
        equippedWeapon = gameObject.GetComponent<dagger>();
        cam = Camera.main;
        storeCont = gameObject.GetComponent<StoreController>();
        
        HP = maxHP;
        renderhealth();
    }

    void Update()
    {
        if (!dying) {
            rb.bodyType = RigidbodyType2D.Dynamic;
            if (cam == null)
            {
                if (cont.currScene == "Shop")
                {
                    storeCont.storeDisplay();
                }
                cam = Camera.main;
                
            }
            if (UI.GetComponent<Canvas>().worldCamera == null)
            {
                UI.GetComponent<Canvas>().worldCamera = Camera.main;
            }

            dir.x = Input.GetAxisRaw("Horizontal");
            dir.y = Input.GetAxisRaw("Vertical");

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            face();

            #region weaponSwitch
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                equippedWeapon = gameObject.GetComponent<dagger>();
                Debug.Log("Equipped Daggers.");
                dagger.equipped = true;
                halberd.equipped = false;
                bow.equipped = false;
                
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                equippedWeapon = gameObject.GetComponent<halberd>();
                Debug.Log("Equipped Halberd.");
                dagger.equipped = false;
                halberd.equipped = true;
                bow.equipped = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                equippedWeapon = gameObject.GetComponent<bow>();
                Debug.Log("Equipped Bow.");
                dagger.equipped = false;
                halberd.equipped = false;
                bow.equipped = true;
            }
            #endregion

            #region weaponAttacks
            if (Input.GetButtonDown("Fire1"))
            {
                equippedWeapon.basicAttack();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                equippedWeapon.ability();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                equippedWeapon.ultimate();
            }
            #endregion

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
            
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Vector2.Distance(mousePos, transform.position) < 4)
                {
                    transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                die();
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static; 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    private void face()
    {
        pov = mousePos - rb.position;
        pov = pov.normalized;
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
        StartCoroutine(wait());
        

    }
    IEnumerator wait()
    {
        dying = true;
        
        yield return new WaitForSeconds(1);
        dying = false;
        cont.ShopScene();
        yield return new WaitForSeconds(.05f);
        transform.position = new Vector3(0.0621082f, -1.851223f, -9.700012f);
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
        for (int j = 0; j < hearts.Length; j++)
        {
            Destroy(hearts[j].gameObject);
        }
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
