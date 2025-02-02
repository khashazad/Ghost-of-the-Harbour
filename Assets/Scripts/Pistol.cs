using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Mathematics;

public class Pistol : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hitVFXPrefab;

    [SerializeField] private Transform muzzleTransform; // pitsol barrel

    [SerializeField] private InputActionReference gripTriggerAction; // Assign from InputSystem_Actions

    private bool isShooting = false;
    private const string SHOOT_STRING = "Shoot";

    void OnEnable()
    {
        if (gripTriggerAction != null)
            gripTriggerAction.action.Enable();
    }

    void OnDisable()
    {
        if (gripTriggerAction != null)
            gripTriggerAction.action.Disable();
    }

    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (gripTriggerAction == null || gripTriggerAction.action == null) return;

        float gripValue = gripTriggerAction.action.ReadValue<float>();

        if (gripValue > 0.8f && !isShooting)
        {
            isShooting = true;
            Shoot();
        }
        else if (gripValue < 0.2f)
        {
            isShooting = false;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);

        RaycastHit hit;
        Vector3 rayOrigin = muzzleTransform.position;
        Vector3 rayDirection = muzzleTransform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * 50, Color.red, 2f); // Visualize the ray in Scene view

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 50f)) // Limit range to 50 units
        {
            Debug.Log("Hit: " + hit.collider.name);
            GameObject vfx = Instantiate(hitVFXPrefab, hit.point, quaternion.identity);
            Destroy(vfx, 1.7f);
            // Apply damage if enemy is hit
             EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
             enemyHealth?.TakeDamage(damageAmount);
        }
    }

}
