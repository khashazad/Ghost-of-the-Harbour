using UnityEngine;

public class Gaze : MonoBehaviour
{
    public float gazeDuration = 2f; // Time to trigger animation
    private float gazeTimer = 0f;
    private GameObject lastHitObject;
    
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("AnimatedObject"))
            {
                if (hit.collider.gameObject != lastHitObject)
                {
                    gazeTimer = 0f;
                    lastHitObject = hit.collider.gameObject;
                }

                gazeTimer += Time.deltaTime;
                if (gazeTimer >= gazeDuration)
                {
                    hit.collider.GetComponent<Animator>()?.SetTrigger("Treasure");
                }
            }
        }
        else
        {
            gazeTimer = 0f;
            lastHitObject = null;
        }
    }
}
