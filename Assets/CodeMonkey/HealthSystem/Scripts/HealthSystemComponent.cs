using System;
using System.Collections;
using UnityEngine;





namespace CodeMonkey.HealthSystemCM {

    /// <summary>
    /// Adds a HealthSystem to a Game Object
    /// </summary>
    public class HealthSystemComponent : MonoBehaviour, IGetHealthSystem {

        [Tooltip("Maximum Health amount")]
        [SerializeField] private float healthAmountMax = 100f;

        [Tooltip("Starting Health amount, leave at 0 to start at full health.")]
        [SerializeField] private float startingHealthAmount;

        private HealthSystem healthSystem;


        private void Start()
        {
            healthSystem.OnDead+= HealthSystem_OnDead;
        }

       

        private void Awake() {
            // Create Health System
            healthSystem = new HealthSystem(healthAmountMax);

            if (startingHealthAmount != 0) {
                healthSystem.SetHealth(startingHealthAmount);
            }
        }

        /// <summary>
        /// Get the Health System created by this Component
        /// </summary>
        public HealthSystem GetHealthSystem() {
            return healthSystem;
        }


        // public void Damage(float damage)
        // {
        //     healthSystem.Damage(damage);
        // }


         private void HealthSystem_OnDead(object sender, EventArgs e)
        {


            Debug.Log("DIED");

            StartCoroutine("EnemyDied");

              

        
        }

        IEnumerator EnemyDied()
        {  
            
            yield return new WaitForSeconds(2f);
            Destroy(gameObject.transform.parent.gameObject);
        }
        


    }

}