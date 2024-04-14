using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public int playerID = 0;

    public void RowBoat()
    {
        if (gameStarted && Time.time - lastRowTime >= rowCooldown)
        {
            lastRowTime = Time.time;

            float currentTimeModInterval = Time.time % targetInterval;

            float closenessToMiddle = 1f - Mathf.Abs(currentTimeModInterval - targetInterval / 2) / (targetInterval / 2);

            float effectiveness = Mathf.Max(0.1f, closenessToMiddle);

            float forceToApply = forcePerStroke * effectiveness;

            boatRigidbody.AddForce(Vector3.forward * forceToApply, ForceMode.Impulse);
        }
    }
}
