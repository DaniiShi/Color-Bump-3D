using UnityEngine;

public class PhysicsOnByTouch : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (rb.isKinematic)
        {
            rb.isKinematic = false;
            rb.WakeUp();
        }
    }
}
