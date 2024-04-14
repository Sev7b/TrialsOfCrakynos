using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
    public float targetInterval = 2.0f; 
    public float forcePerStroke = 10.0f; 
    public float maxDistance = 50.0f; 
    public bool gameStarted = false; 
    public Image timingIndicator; 

    private Rigidbody[] boats; 
    private float lastRowTime = -1f; 
    private float rowCooldown = 1f; 

    void Update()
    {
        if (gameStarted)
        {
            UpdateTimingIndicator();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    private void UpdateTimingIndicator()
    {
        if (gameStarted)
        {
            float elapsedTime = Time.time % targetInterval;

            float cycleProgress = Mathf.Cos((elapsedTime / targetInterval) * 2 * Mathf.PI - Mathf.PI) * -0.5f + 0.5f;

            float size = Mathf.Lerp(50, 0, cycleProgress);
            timingIndicator.rectTransform.sizeDelta = new Vector2(size, size);
        }
    }
}
