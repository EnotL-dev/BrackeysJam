using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference jumpAction;

    [Header("Movement")]
    private bool freezeMovemet = false;
    public void FreezeMovement() => freezeMovemet = true;
    public void UnFreezeMovement() => freezeMovemet = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Look")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float lookSensitivity = 0.1f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private CharacterController controller;

    private Vector3 velocity;
    private float cameraPitch;

    private void Awake() {
        controller = GetComponent<CharacterController>();

        if ( cameraTransform == null && Camera.main != null )
            cameraTransform = Camera.main.transform;
    }

    private void OnEnable() {
        moveAction.action.Enable();
        lookAction.action.Enable();
        jumpAction.action.Enable();

        SetMouseFocus(true);
    }

    private void OnDisable() {
        moveAction.action.Disable();
        lookAction.action.Disable();
        jumpAction.action.Disable();

        SetMouseFocus(false);
    }

    private void Update() {

        //this should lock the look input too
        if ( !freezeMovemet ) {
            HandleMovement();
            HandleLook();
        }
        HandleGravity();
    }

    private void HandleLook() {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        // Horizontal look
        transform.Rotate(
            Vector3.up,
            lookInput.x * lookSensitivity
        );

        // Vertical look
        cameraPitch -= lookInput.y * lookSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);

        cameraTransform.localRotation =
            Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void HandleMovement() {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Movement relative to player/camera's horizontal orientation
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move =
            forward * input.y +
            right * input.x;

        move = Vector3.ClampMagnitude(move, 1f);

        controller.Move(
            move * moveSpeed * Time.deltaTime
        );
    }

    private void HandleGravity() {
        if ( controller.isGrounded && velocity.y < 0f ) {
            velocity.y = -2f;
        }

        if ( jumpAction.action.WasPressedThisFrame() &&
            controller.isGrounded ) {
            velocity.y =
                Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime
        );
    }

    private void SetMouseFocus( bool focused ) {
        if ( focused ) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}




