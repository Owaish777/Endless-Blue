using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    public float maxSubmersionDepth = 2f;

    public InputAction moveAction;
    public float thrustForce = 10f;
    public GameObject thrustPointsLeft;
    public GameObject thrustPointsRight;
    public GameObject water;
    public float offset = 1f;


    private Animator animator;
    private SimpleWave simpleWave;
    private Rigidbody rb;
    private Transform[] leftOarPoints;
    private Transform[] rightOarPoints;

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        simpleWave = water.GetComponent<SimpleWave>();

        leftOarPoints = new Transform[thrustPointsLeft.transform.childCount];
        rightOarPoints = new Transform[thrustPointsRight.transform.childCount];

        for (int i = 0; i < thrustPointsLeft.transform.childCount; i++)
        {
            leftOarPoints[i] = thrustPointsLeft.transform.GetChild(i);
        }

        for (int i = 0; i < thrustPointsRight.transform.childCount; i++)
        {
            rightOarPoints[i] = thrustPointsRight.transform.GetChild(i);
        }
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        float rowInput = input.y;
        float turnInput = input.x;

        HandleRowing(rowInput);
        HandleTurning(turnInput);
    }

    private void FixedUpdate()
    {
        foreach (Transform point in leftOarPoints)
        {
            float waterHeight = simpleWave.GetWaveHeight(point.position);
            float diff = waterHeight - point.position.y;

            if (diff > 0)
            {
                float submersion = Mathf.Clamp01(diff / maxSubmersionDepth);

                rb.AddForceAtPosition(transform.forward * thrustForce * submersion, transform.position - new Vector3(offset , 0 , 0));
            }
        }

        foreach (Transform point in rightOarPoints)
        {
            float waterHeight = simpleWave.GetWaveHeight(point.position);
            float diff = waterHeight - point.position.y;

            if (diff > 0)
            {
                float submersion = Mathf.Clamp01(diff / maxSubmersionDepth);

                rb.AddForceAtPosition(transform.forward * thrustForce * submersion, transform.position + new Vector3(offset, 0, 0));
            }
        }
    }

    void HandleRowing(float rowInput)
    {
        bool isRowing = rowInput > 0f; 
        animator.SetBool("IsRowing", isRowing);
    }

    void HandleTurning(float turn)
    {
        animator.SetFloat("Turn", turn);
    }
}