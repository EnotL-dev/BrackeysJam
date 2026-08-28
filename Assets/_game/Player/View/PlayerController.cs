using Assets._game.UI;
using Assets._game.UI.View;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference runAction;


    [Header("Movement")]
    private bool freezeMovemet = false;
    public void FreezeMovement() => freezeMovemet = true;
    public void UnFreezeMovement() => freezeMovemet = false;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Look")]
    [SerializeField] private Camera camera;
    Transform cameraTransform;
    [SerializeField] private float lookSensitivity = 0.1f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    [Header("Jump Checking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;



    private ISettingConfigService settingConfigService;

    [SerializeField] private float speedboost = 1.5f;

    private CharacterController controller;
    bool isRunning;
    private bool isGrounded;


    private Vector3 velocity;
    private float cameraPitch;

    [Inject]
    void Construct(ISettingConfigService settingConfigService) {
        this.settingConfigService = settingConfigService;
    }

    private void Awake() {
        controller = GetComponent<CharacterController>();

        cameraTransform = camera.GetComponent<Transform>();

        if ( cameraTransform == null && Camera.main != null )
            cameraTransform = Camera.main.transform;
    }

    private void OnEnable() {
        moveAction.action.Enable();
        lookAction.action.Enable();
        jumpAction.action.Enable();
        runAction.action.Enable();

        settingConfigService.OnSensitivityChanged += ChangeSensivity;
        settingConfigService.OnFOVChanged += ChangeFov;

        SetMouseFocus(true);
    }

    private void OnDisable() {
        moveAction.action.Disable();
        lookAction.action.Disable();
        jumpAction.action.Disable();
        runAction.action.Disable();

        SetMouseFocus(false);
    }

    private void Update() {
        HandleGravity();

        //this should lock the look input too
        if ( freezeMovemet ) return;
        HandleRun();
        HandleMovement();
        HandleLook();

    }

    void HandleRun() {
        if ( isRunning ) {
            if ( runAction.action.WasReleasedThisFrame() ) {
                isRunning = false;
            }
        }
        else if ( runAction.action.WasPerformedThisFrame() ) {
            isRunning = true;
        }


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

        if ( isRunning ) {
            controller.Move(move * moveSpeed * speedboost * Time.deltaTime);
        }

        controller.Move(
            move * moveSpeed * Time.deltaTime
        );
    }

    //BUG: stay still can't jump
    //might not use jump
    private void HandleGravity() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask, QueryTriggerInteraction.Ignore);

        if ( isGrounded && velocity.y < 0f ) {
            velocity.y = -2f;
        }

        if ( jumpAction.action.WasPressedThisFrame() && isGrounded ) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime
            );
    }

    public void SetInputEnabled( bool enable ) {
        freezeMovemet = !enable;
    }


    public void SetMouseFocus( bool focused ) {
        Debug.Log($"Cursor should be {focused}");

        if ( focused ) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Debug.Log($"AFTER SET -> lockState: {Cursor.lockState}, visible: {Cursor.visible}");
    }



    void ChangeSensivity(float sensivity) => lookSensitivity = sensivity;
        
    void ChangeFov(float value) => camera.fieldOfView = value;


}




