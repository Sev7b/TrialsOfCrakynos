using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbiter : MonoBehaviour
{
    public Transform target; // The target point to orbit around
    public float height = 5f; // Height above the target point
    public float distance = 10f; // Distance from the target point
    public float orbitSpeed = 5f; // How fast the camera orbits

    // Start is called before the first frame update
    void Start()
    {
        if (!target)
        {
            Debug.LogError("CameraOrbiter: No target assigned!");
            enabled = false; // Disable this script if no target is assigned
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            OrbitAroundTarget();
        }
    }

    void OrbitAroundTarget()
    {
        // Calculate the next position in the orbit
        float angleDegrees = orbitSpeed * Time.time; // This increases over time, causing the orbit
        float angleRadians = angleDegrees * Mathf.Deg2Rad; // Convert degrees to radians for the math functions

        // Calculate position
        Vector3 position = new Vector3();
        position.x = target.position.x + Mathf.Cos(angleRadians) * distance;
        position.z = target.position.z + Mathf.Sin(angleRadians) * distance;
        position.y = target.position.y + height;

        transform.position = position;

        // Always look at the target
        transform.LookAt(target.position);
    }
}
