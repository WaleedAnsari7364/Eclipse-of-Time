using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;

public class SlashDamage : MonoBehaviour
{

    public AudioClip enemyHurtSFX;

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy" && other.name!="Dreyar By M.Aure" && other.name!="orc")
        {
            other.transform.GetComponent<EnemyHealth>().GetHealthSystem().Damage(15f);
            SoundManager.instance.playSoundEffect(enemyHurtSFX);
        }
        else if(other.name=="Dreyar By M.Aure" || other.name=="orc")
        {
            other.transform.GetComponent<EnemyHealth>().GetHealthSystem().Damage(4f);
            SoundManager.instance.playSoundEffect(enemyHurtSFX);
        }
    }
    


    

}
