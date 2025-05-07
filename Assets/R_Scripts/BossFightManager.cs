using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{

    public GameObject enemyDreyar;

    public GameObject portalMessage;
    public GameObject portal;


    void Update()
    {
        if(enemyDreyar!=null && enemyDreyar.GetComponent<EnemyHealth>().bossDied)
        {
            portalMessage.SetActive(true);
            portal.SetActive(true);
            Invoke("DestroyPortalMessage",3f);
            

        }
    }


    void DestroyPortalMessage()
    {
        portalMessage.SetActive(false);
    }
}
