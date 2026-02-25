using UnityEngine;

public class SimpleBuoyancy : MonoBehaviour
{
    public float waterLevel = 0f;
    public float floatForce = 10f;
    public float damping = 0.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float difference = waterLevel - transform.position.y;

        if (difference > 0)
        {
            rb.AddForce(Vector3.up * floatForce * difference);
            rb.AddForce(-rb.linearVelocity * damping);
        }
    }
}