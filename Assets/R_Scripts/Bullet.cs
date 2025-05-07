using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject Enemy;


    public AudioClip enemyHurtSFX;


    
    public GameObject hitEffectPrefab; // Reference to the hit effect prefab
    public float hitEffectDuration = 1.0f; // Duration of the hit effect before it's destroyed

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a collider
        if (collision.collider != null && collision.transform.tag == "Enemy")
        {
            Debug.Log("Bullet collided with: " + collision.collider.name);

            if(collision.transform.tag=="Enemy" && collision.collider.name!="Dreyar By M.Aure")
            {
                collision.transform.GetComponent<EnemyHealth>().GetHealthSystem().Damage(10f);
                SoundManager.instance.playSoundEffect(enemyHurtSFX);


            }
            else if(collision.collider.name=="Dreyar By M.Aure")
            {
                collision.transform.GetComponent<EnemyHealth>().GetHealthSystem().Damage(5f);
                SoundManager.instance.playSoundEffect(enemyHurtSFX);

            }

            // Instantiate the hit effect at the collision point
            GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            
            // Destroy the hit effect after a short delay
            Destroy(hitEffect, hitEffectDuration);

            // Destroy the bullet GameObject
            Destroy(gameObject);
        }


        // if(collision.transform.tag=="Enemy")
        // {
        //     //Enemy.GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(10f);
        // }
    }


   
}
