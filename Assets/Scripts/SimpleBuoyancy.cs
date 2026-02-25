using UnityEngine;

public class SimpleBuoyancy : MonoBehaviour
{
    public Transform[] floatPoints;
    public float waterLevel = 0f;
    public float floatForce = 10f;
    public float damping = 0.5f;
    public float angularDamping = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (Transform point in floatPoints)
        {
            float diff = waterLevel - point.position.y;

            if (diff > 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatForce * diff, point.position);
            }
        }

        rb.AddForce(-rb.linearVelocity * damping);
        rb.AddTorque(-rb.angularVelocity * angularDamping);
    }
}