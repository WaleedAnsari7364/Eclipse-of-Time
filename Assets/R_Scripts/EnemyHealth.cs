using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IGetHealthSystem
{
    [Tooltip("Maximum Health amount")]
    [SerializeField] private float healthAmountMax = 100f;

    [Tooltip("Starting Health amount, leave at 0 to start at full health.")]
    [SerializeField] private float startingHealthAmount;

    private HealthSystem enemyHealth;

    private Animator enemyAnim;


    [SerializeField] private float deathAnimationTime=2f;

    private bool dead;

    public AudioClip enemyDeathSFX;


    public bool bossDied=false;



    private void Start()
    {
        dead = false;
        enemyAnim = GetComponent<Animator>();
        enemyHealth.OnDead += HealthSystem_OnDead;
    }



    private void Awake()
    {
        // Create Health System
        enemyHealth = new HealthSystem(healthAmountMax);

        if (startingHealthAmount != 0)
        {
            enemyHealth.SetHealth(startingHealthAmount);
        }
    }

    /// <summary>
    /// Get the Health System created by this Component
    /// </summary>
    public HealthSystem GetHealthSystem()
    {
        return enemyHealth;
    }


    // public void Damage(float damage)
    // {
    //     healthSystem.Damage(damage);
    // }


    private void HealthSystem_OnDead(object sender, EventArgs e)
    {

		if (!dead)
		{
            bossDied=true;
            //GetComponent<EnemyAiTutorial>().SetStop(true);
            this.GetComponent<EnemyAiTutorial>().enabled = false;
            enemyAnim.SetTrigger("Death");
            dead = true;
            SoundManager.instance.playSoundEffect(enemyDeathSFX);
            StartCoroutine("EnemyDied");
        }
    }

    IEnumerator EnemyDied()
    {

        
        yield return new WaitForSeconds(deathAnimationTime);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
