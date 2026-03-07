using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    [Header("References")]
    public InputAction moveAction;

    private Animator animator;

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
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        float rowInput = input.y;
        float turnInput = input.x;

        HandleRowing(rowInput);
        HandleTurning(turnInput);
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