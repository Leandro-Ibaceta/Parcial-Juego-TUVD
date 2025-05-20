using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        if(context.performed)
        {
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void MovePlayer(Vector3 input)
    {
        _playerRb.MovePosition(transform.position + input * _speed * Time.deltaTime);
    }


    //Convierte las coordenadas globales del input a las coordenadas de la camara
    private Vector3 MoveRelativeToCamera (Vector2 input)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardProduct = input.y * cameraForward;
        Vector3 cameraRighProduct = input.x * cameraRight;

        Vector3 vectorRotatedToCamera = cameraForwardProduct + cameraRighProduct;

        return vectorRotatedToCamera;
    }
}
