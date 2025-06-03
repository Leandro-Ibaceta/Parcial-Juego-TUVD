using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _speed = 10f;
    private Rigidbody _playerRb;
    private PlayerInput _playerInput;
    private Vector2 _input;
    private Vector3 _movementRelativeToCamera;

    //Animation variables
    [SerializeField] private Animator _animator;
    private int _isWalkingHash, _isRunningHash;
   

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isRunningHash = Animator.StringToHash("IsRunning");
    }


    private void Update()
    {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _movementRelativeToCamera = MoveRelativeToCamera(_input);
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer(_movementRelativeToCamera);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void MovePlayer(Vector3 input)
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);
        bool movePressed = _input.x != 0 || _input.y != 0;

        if (movePressed && !isWalking)
            _animator.SetBool(_isWalkingHash, true);

        if ((!movePressed && isWalking))
            _animator.SetBool(_isWalkingHash, false);

        
        

        _playerRb.MovePosition(transform.position + input * _speed * Time.deltaTime);
       
    }

    private void RotatePlayer()
    {
        float rotationSpeed = 5;
        Vector3 posToLookAt;
        posToLookAt.x = _movementRelativeToCamera.x;
        posToLookAt.y = 0;
        posToLookAt.z = _movementRelativeToCamera.z;
        posToLookAt = posToLookAt.normalized;

        Quaternion rotation = transform.rotation;
        Quaternion targetRotation = posToLookAt == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(posToLookAt);
        
        transform.rotation = Quaternion.Slerp(rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private Vector3 MoveRelativeToCamera(Vector2 input)
    {
        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;
        camF.y = camR.y = 0;
        camF.Normalize(); camR.Normalize();
        return input.y * camF + input.x * camR;
    }

    public void Crouch(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            _animator.SetBool("IsCrouching", true);
        }
        if (callbackContext.canceled)
        {
            _animator.SetBool("IsCrouching", false);

        }
        
    }
}
