using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private const string CANRUN = "CanRun";
    private const string CANJUMP = "CanJump";

    [SerializeField] FixedJoystick _joystick;
    [SerializeField] Button _jumpButton;

    private Animator _playerAnimator;
    private Rigidbody _rb;

    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpForce = 5; // Adjusted jump force for a more realistic jump

    private bool _isGrounded = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        if (!GameManager.Instance.isGameOver)
            _jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        if(!GameManager.Instance.isGameOver)
            HandleMovement();

    }

    void HandleMovement()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            _playerAnimator.SetBool(CANRUN, true);

            // Move the player
            Vector3 newPosition = transform.position + moveDirection * _moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
        else
        {
            _playerAnimator.SetBool(CANRUN, false);
        }
    }

    void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _playerAnimator.SetBool(CANJUMP, true);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded  
        if (collision.gameObject.CompareTag("Ground"))
        {
            _playerAnimator.SetBool(CANJUMP, false);
            _isGrounded = true;
        }
    }

    public void Destroy()
    {
        GameManager.Instance.isPlayerAlive = false;
        Destroy(gameObject);
    }

}
