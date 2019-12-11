using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{
    //Called when an Obj enters the trigger collider.
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //Check if coll is the player or not and set in parent script if so.
        if (coll.CompareTag("Player"))
        {
            GetComponentInParent<Enemy>().player = coll.transform;
            GetComponentInParent<Enemy>().canAttack = true;
        }
    }

}
