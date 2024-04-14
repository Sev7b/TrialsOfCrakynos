using UnityEngine;
using System.Linq; 

public class RagdollManager : MonoBehaviour
{
    public bool isRagdoll = false;
    private Animator animator;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;
    private CharacterJoint[] characterJoints;

    void Start()
    {
        animator = GetComponent<Animator>();

        rigidbodies = GetComponentsInChildren<Rigidbody>().Where(rb => rb != GetComponent<Rigidbody>()).ToArray();
        colliders = GetComponentsInChildren<Collider>().Where(rb => rb != GetComponent<CapsuleCollider>()).ToArray();
        characterJoints = GetComponentsInChildren<CharacterJoint>();

        ToggleRagdoll(false);
    }

    void Update()
    {
        if (isRagdoll)
        {
            ToggleRagdoll(true);
        }
        else
        {
            ToggleRagdoll(false);
        }
    }

    void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !state;
        }

        foreach (var col in colliders)
        {
            col.enabled = state;
        }
    }

    public void SwitchRagdoll()
    {
        isRagdoll = !isRagdoll;
    }
}
