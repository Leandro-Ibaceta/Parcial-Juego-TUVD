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

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _movementRelativeToCamera = MoveRelativeToCamera(_input);
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
        _playerRb.MovePosition(transform.position + input * _speed * Time.deltaTime);
    }

    private Vector3 MoveRelativeToCamera(Vector2 input)
    {
        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;
        camF.y = camR.y = 0;
        camF.Normalize(); camR.Normalize();
        return input.y * camF + input.x * camR;
    }
}
