using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Point from where the bullet will be fired
    public float bulletSpeed = 20f; // Speed of the bullet
    public GameObject hitEffectPrefab; // Reference to the hit effect prefab

    public void Shoot()
    {
        // Instantiate a new bullet at the fire point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Check if the bullet has a Rigidbody component
        if (rb != null)
        {
            // Add force to the bullet in the forward direction
            rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
        }

        Destroy(bullet, 3f);
    }
}
