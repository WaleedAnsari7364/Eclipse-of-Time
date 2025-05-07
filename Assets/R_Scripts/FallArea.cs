using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallArea : MonoBehaviour
{

    public GameObject LeaveZoneMessage;

    public PlayerHealth playerHealth;

    public bool canDamage= true;


    void OnCollisionStay(Collision collision)
    {

        if(collision.transform.tag=="Player" && canDamage)
        {
            canDamage=false;

            LeaveZoneMessage.SetActive(true);
            StartCoroutine(nameof(FallZoneDamage));      

        }

    }


    void OnCollisionExit(Collision collision)
    {

        if(collision.transform.tag=="Player")
        {

            LeaveZoneMessage.SetActive(false);
                 

        }

    }


    IEnumerator FallZoneDamage()
    {
        yield return new WaitForSeconds(2f);
        playerHealth.Damage(6f);
        canDamage=true;
    }
    

   
}
