using UnityEngine;
using System.Collections;

public class EnemyExplosionTrigger : MonoBehaviour
{
    public GameObject hitVFXPrefab; // Assign in the Inspector
    [SerializeField] private float destroyDelay = 0.1f; // Optional delay before destroying
    public Material GlowMaterial;

    [Header("Explosion Settings")]
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionDamage = 10f; // Damage dealt when in explosion range

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (GlowMaterial == null)
            {
                Debug.LogWarning("Please assign the GlowMaterial.");
                return;
            }

            // Apply the glowing effect
            Renderer[] childRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in childRenderers)
            {
                r.material = GlowMaterial;
            }

            // Start explosion after delay
            StartCoroutine(DelayExplosion());
        }
    }

    IEnumerator DelayExplosion()
    {
        yield return new WaitForSeconds(2f); // Wait for the explosion delay

        // Get the AudioSource from the GameObject with tag "explosionSound"
        GameObject soundObject = GameObject.FindGameObjectWithTag("explosionSound");
        if (soundObject != null)
        {
            AudioSource audio = soundObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play(); // Play the explosion sound
            }
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'explosionSound'");
        }

        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Check if the player is within explosion radius
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= explosionRadius)
            {
                // If player is within explosion radius, apply damage
                PlayerScript playerScript = player.GetComponent<PlayerScript>();
                if (playerScript != null)
                {
                    playerScript.TakeDamage(); // Call TakeDamage()
                }
            }
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }

        // Instantiate the explosion VFX
        GameObject vfx = Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
        Destroy(vfx, 1.7f);

        // Destroy the enemy object after explosion
        Destroy(gameObject, 1f);
    }
}