using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource SFX;
    public static SoundManager instance {get;set;}

    void Awake()
    {
        if(instance==null)
        {
            instance=this;
        }
        else
        {
            instance=this;
        }
        SFX= GetComponent<AudioSource>();
    }

    


    public void playSoundEffect(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }


}
