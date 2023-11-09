using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Accessing the gravity script for its gravity angle and vector
    private Gravity _gravityScript;
    // Accessing the rigid body component to apply forces to it (jump)
    private Rigidbody2D _myRigidbody;

    // Preia vectorul de miscare din Input System
    private Vector2 _input;
    private float _inputGravityAdjust;

    [SerializeField]
    public float JumpStrength = 300;
    [SerializeField]
    public float MovementSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        _gravityScript = GameObject.Find("Manager").GetComponent<Gravity>();
        _myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    public void GravityRotate(InputAction.CallbackContext context)
    {
        _inputGravityAdjust = -(context.ReadValue<Vector2>())[0];
    }
        
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 relativeUpDirection = -_gravityScript.GravityVector.normalized * JumpStrength;
            _myRigidbody.AddForce(relativeUpDirection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, _gravityScript.GravityAngle - 270);


        // Define the vector on the X axis of the player, in the direction given above, with length equal to MovementSpeed
        Vector3 horizontalMovementVector = new Vector3(_input[0], _input[1], 0) * MovementSpeed;
        // Adjust the length of the vector to the speed the game is running at
        horizontalMovementVector *= Time.deltaTime;        
        // Apply this vector to the player's translation
        transform.Translate(horizontalMovementVector); 

        // Gravity angle adjust
        _gravityScript.AdjustGravityAngle(_inputGravityAdjust);


        // Debug - reset position
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(-3.25f, -1.25f, -10f);
            GetComponent<TimeTravel>().enabled = true;
        }
    }
}
