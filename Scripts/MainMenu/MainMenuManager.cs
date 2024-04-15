using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MainMenuManager : MonoBehaviour
{
    public enum State
    {
        TITLE,
        PLAYERS,
        TRIALS
    }

    [SerializeField] public State currentState;

    [Header("TITLE")]
    [Header("Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float height = 5f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float orbitSpeed = 5f;
    [Header("Game Objects")]
    [SerializeField] private Animation title;
    [SerializeField] private AnimationClip titleRise;
    [SerializeField] private Animation startText;
    [SerializeField] private AnimationClip textFall;

    [Space]
    [Header("PLAYERS")]
    [SerializeField] private Animation playerSlots;
    [SerializeField] private AnimationClip slideIn;
    [SerializeField] private AnimationClip slideOut;

    [Space]
    [Header("TRIALS")]
    [SerializeField] private Transform trialsPosition;
    [SerializeField] private float transitionSpeed = 1f; 
    private bool transitioningToTrials = false;
    private float transitionProgress = 0f; 

    void Update()
    {
        switch (currentState)
        {
            case State.TITLE:
            case State.PLAYERS:
                if (target)
                {
                    OrbitAroundTarget();
                }
                break;
            case State.TRIALS:
                if (!transitioningToTrials)
                {
                    StartCoroutine(TransitionToTrials());
                }
                transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime * (orbitSpeed / 2)));
                break;
        }

        HandleStateTransitions();
    }

    void OrbitAroundTarget()
    {
        float angleDegrees = orbitSpeed * Time.time;
        float angleRadians = angleDegrees * Mathf.Deg2Rad;

        Vector3 position = new Vector3();
        position.x = target.position.x + Mathf.Cos(angleRadians) * distance;
        position.z = target.position.z + Mathf.Sin(angleRadians) * distance;
        position.y = target.position.y + height;

        transform.position = position;
        transform.LookAt(target.position);
    }

    IEnumerator TransitionToTrials()
    {
        transitioningToTrials = true;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        while (transitionProgress < 1f)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;
            transform.position = Vector3.Lerp(startPosition, trialsPosition.position, transitionProgress);
            transform.rotation = Quaternion.Lerp(startRotation, trialsPosition.rotation, transitionProgress);
            yield return null;
        }
        transitioningToTrials = false;
    }

    async void HandleStateTransitions()
    {
        if ((Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current?.allControls.Any(c => c.IsPressed()) == true) && currentState == State.TITLE)
        {
            currentState = State.PLAYERS;
            title.Play(titleRise.name);
            startText.Play(textFall.name);

            await Task.Delay(690);

            playerSlots.Play(slideIn.name); 
        }

        // Additional logic here for transitioning from PLAYERS to TRIALS
        // This could be triggered by a specific input or other condition in your game
        // Ensure that TransitionToTrials() is only called when the transition condition is met
    }
}
