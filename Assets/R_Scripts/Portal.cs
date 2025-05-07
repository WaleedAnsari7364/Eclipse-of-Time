using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public AudioClip portalSFX;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag("Player"))
        {

            SoundManager.instance.playSoundEffect(portalSFX);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        }
    }


}
