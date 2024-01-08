using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Accessing the gravity script for its gravity angle and vector
    private Gravity _gravityScript;
    private Rigidbody2D _rb;

    public float JumpForce = 9;
    public float Speed = 4;
    public float fallingSpeed = 6;

    private bool _isGrounded;
    private bool _doubleJump = true;
    [SerializeField]
    private float _groundCheckSize = 0.35f;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    private Vector2 _input;
    private float _inputGravityAdjust;

    //  Object inspect
    private Camera _mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        _gravityScript = GetComponent<Gravity>();
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }


    public void MoveInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    public void GravityRotateInput(InputAction.CallbackContext context)
    {
        _inputGravityAdjust = -(context.ReadValue<Vector2>())[0];
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Check jump
            _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundCheckSize, GroundLayer);

            if (_isGrounded)
                Jump();
            else
                if (_doubleJump)
            {
                Jump(_doubleJump);
                _doubleJump = false;
            }
        }
    }
    void Jump(bool isDoubleJump = false)
    {
        //The second jump is not as high
        _rb.velocity = -_gravityScript.GravityVector.normalized * JumpForce * (isDoubleJump ? (2.0f / 3.0f) : 1);
    }

    public void ClickInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var rayhit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
            if (rayhit.collider)
            {
                Debug.Log(rayhit.collider.gameObject.name);
                if (rayhit.collider.gameObject.HasCustomTag("Button"))
                {
                    _mainCamera.GetComponent<CameraFollower>().Player = rayhit.collider.gameObject.GetComponent<ButtonScript>().LinkedObject.transform;
                    return;
                }
            }

            _mainCamera.GetComponent<CameraFollower>().Player = transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, _gravityScript.GravityAngle - 270);

        //Check restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _gravityScript.ToggleGlobalGravity();
        }
    }
    private void FixedUpdate()
    {
        //Get Direction 
        Vector3 horizontalMovementVector = new Vector3(_input[0], _input[1], 0);

        //Check Direction of Sprite
        if (horizontalMovementVector[0] != 0)
        {
            GetComponent<SpriteRenderer>().flipX = horizontalMovementVector[0] < 0;
        }
        horizontalMovementVector = horizontalMovementVector * Speed * Time.deltaTime;

        //Move object
        //_rb.velocity = new Vector2(horizontalMovementVector[0], _rb.velocity.y);
        transform.Translate(horizontalMovementVector);
        //_rb.velocity = horizontalMovementVector;


        // Gravity angle adjust
        _gravityScript.AdjustGravityAngle(_inputGravityAdjust);
        // Apply modified gravity specifically to the player (in case Global Gravity Change is off)
        _rb.AddForce(_gravityScript.PlayerGravity * _rb.mass);



        if (_isGrounded)
            _doubleJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, _groundCheckSize);
    }

}