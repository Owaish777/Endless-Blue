using UnityEngine;

public class SimpleBuoyancy : MonoBehaviour
{
    public GameObject buoyancyPoints;
    public GameObject water;
    public Transform centerOfMassTransform;
    public float floatForce = 10f;
    public float damping = 0.5f;
    public float angularDamping = 1f;
    public float maxSubmersionDepth = 2f;
    public float stabilityStrength = 1f;
    public float wavePushMultiplier = 1f;

    private Rigidbody rb;
    private SimpleWave simpleWave;
    private Transform[] floatPoints;

    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.InverseTransformPoint(centerOfMassTransform.position);

        simpleWave = water.GetComponent<SimpleWave>();

        floatPoints = new Transform[buoyancyPoints.transform.childCount];
        for (int i = 0; i < buoyancyPoints.transform.childCount; i++)
        {
            floatPoints[i] = buoyancyPoints.transform.GetChild(i);
        }
    }

    void FixedUpdate()
    {
        foreach (Transform point in floatPoints)
        {
            float waterHeight = simpleWave.GetWaveHeight(point.position);
            float diff = waterHeight - point.position.y;

            if (diff > 0)
            {
                float submersion = Mathf.Clamp01(diff / maxSubmersionDepth);

                // Upward buoyancy force
                Vector3 upForce = Vector3.up * floatForce * submersion;

                // Horizontal push coming from the moving waves
                Vector3 wavePush = simpleWave.GetWavePush(point.position) * wavePushMultiplier * submersion;

                // Apply combined force
                rb.AddForceAtPosition(upForce + wavePush, point.position);
            }   
        }

        //Upright Stabilization Torque
        Vector3 rightingTorque = Vector3.Cross(transform.up, Vector3.up);
        rb.AddTorque(rightingTorque * stabilityStrength);

        rb.AddForce(-rb.linearVelocity * damping);
        rb.AddTorque(-rb.angularVelocity * angularDamping);
    }
}