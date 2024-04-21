using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            AdjustVelocity(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider)
        {
            AdjustVelocity(other.gameObject);
        }
    }

    void AdjustVelocity(GameObject collidingObject)
    {
        float coverage = CalculateCoveragePercentage(collidingObject);

        // Check if the coverage is at or exceeds 70%
        if (coverage >= 70f)
        {
            // Completely stop the object by setting its velocity and angular velocity to zero
            Rigidbody rb = collidingObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            // Optionally, you can still apply gradual slowing here if less than 70% covered
            // For example, reducing velocity based on how close it is to 70% coverage
            Rigidbody rb = collidingObject.GetComponent<Rigidbody>();
            rb.velocity *= 1 - (coverage / 70f); // Linearly interpolate velocity reduction
        }
    }


    float CalculateCoveragePercentage(GameObject collidingObject)
    {
        CapsuleCollider capsuleCollider = collidingObject.GetComponent<CapsuleCollider>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        // Calculate the world space position of the capsule's bottom
        Vector3 capsuleBottom = collidingObject.transform.position + (collidingObject.transform.up * capsuleCollider.height / 2);

        // Calculate the world space position of the box's top
        Vector3 boxTop = transform.position + (transform.up * boxCollider.size.y / 2);

        // Distance from the capsule's bottom to the box's top
        float distance = Vector3.Distance(capsuleBottom, boxTop);

        // Assuming the capsule enters the box from the top, calculate the coverage
        float totalCoverageDistance = capsuleCollider.height * 0.7f; // 70% coverage
        float currentCoverage = Mathf.Clamp01(1 - (distance / totalCoverageDistance));

        return currentCoverage * 100; // Return as percentage
    }



    float CalculateRequiredForce(GameObject collidingObject)
    {
        // Simplified force calculation. This should be adjusted based on the gameplay needs.
        return 5f - (CalculateCoveragePercentage(collidingObject) - 70f);
    }
}
