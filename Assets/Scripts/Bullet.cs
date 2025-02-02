using UnityEngine;
using Unity.Mathematics;

public class Bullet : MonoBehaviour
{
    public GameObject hitVFXPrefab; // Assign in the Inspector
    [SerializeField] private float destroyDelay = 0.1f; // Optional delay before destroying

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        if (hitVFXPrefab != null)
        {
            GameObject vfx = Instantiate(hitVFXPrefab, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            Destroy(vfx, 1.7f); // Destroy the effect after 2 seconds
        }
        else
        {
            Debug.LogError("hitVFXPrefab is NULL. Assign it in the Inspector.");
        }

        Destroy(gameObject, destroyDelay);
    }


}
