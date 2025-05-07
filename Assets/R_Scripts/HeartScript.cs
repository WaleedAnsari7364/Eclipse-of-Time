using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{

    public AudioClip healthSFX;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerHealth>().Heal(20f);
            SoundManager.instance.playSoundEffect(healthSFX);
            Destroy(gameObject);
        }
    }
   

}
