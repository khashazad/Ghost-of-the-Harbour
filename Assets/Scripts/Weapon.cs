using UnityEngine;
using Unity.Mathematics;

public class Weapon : MonoBehaviour
{
    public GameObject cannonBall;
    public Transform barrel;
    public float force;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] int damageAmount = 10;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(cannonBall, barrel.position, barrel.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = barrel.forward * force * Time.deltaTime; // Fixing velocity issue

            muzzleFlash.Play();
        }
    }


    /*    void HandleShoot()
        {
            //TODO
            //if (Input.GetButtonDown(SHOOT_STRING))
            if (Input.GetMouseButtonDown(0))
            {

                GameObject bullet = Instantiate(cannonBall, barrel.position, barrel.rotation);
                bullet.GetComponent<Rigidbody>().linearVelocity = barrel.forward * force * Time.deltaTime; ;

                muzzleFlash.Play();

                //RaycastHit hit;

                //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
                //{
                //    Instantiate(hitVFXPrefab, hit.point, quaternion.identity);
                //    Debug.Log("hit " + hit.collider.name);
                //    //EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                //    //enemyHealth?.TakeDamage(damageAmount);
                //}
            }

        }*/
}
