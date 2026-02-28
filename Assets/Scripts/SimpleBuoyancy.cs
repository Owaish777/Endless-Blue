using UnityEngine;

public class SimpleBuoyancy : MonoBehaviour
{
    public Transform[] floatPoints;
    public GameObject water;
    public float floatForce = 10f;
    public float damping = 0.5f;
    public float angularDamping = 1f;

    private Rigidbody rb;
    private SimpleWave simpleWave;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        simpleWave = water.GetComponent<SimpleWave>();
    }

    void FixedUpdate()
    {
        foreach (Transform point in floatPoints)
        {
            float waterHeight = simpleWave.GetWaveHeight(point.position);
            float diff = waterHeight - point.position.y;

            if (diff > 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatForce * diff, point.position);
            }
        }

        rb.AddForce(-rb.linearVelocity * damping);
        rb.AddTorque(-rb.angularVelocity * angularDamping);
    }
}