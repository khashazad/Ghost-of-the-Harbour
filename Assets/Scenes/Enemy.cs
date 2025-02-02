using UnityEngine;
using UnityEngine.AI;
using Unity.XR.CoreUtils;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;

    void Update()
    {
        agent = GetComponent<NavMeshAgent>();

        // Find the XR Origin (XR Rig) dynamically
        XROrigin xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin != null)
        {
            target = xrOrigin.transform;
        }
        else
        {
            Debug.LogError("XR Origin (XR Rig) not found in the scene! Make sure it's added.");
        }

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
