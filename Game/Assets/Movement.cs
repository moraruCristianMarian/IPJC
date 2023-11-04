using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Accessing the gravity script for its gravity angle and vector
    private Gravity _gravityScript;
    // Accessing the rigid body component to apply forces to it (jump)
    private Rigidbody2D _myRigidbody;

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

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, _gravityScript.GravityAngle - 270);

        // The direction of horizontal movement = right (+X) minus left (-X) 
        float horizontalMovementDirection = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);

        // Define the vector on the X axis of the player, in the direction given above, with length equal to MovementSpeed
        Vector3 horizontalMovementVector = new Vector3(horizontalMovementDirection, 0, 0) * MovementSpeed;
        // Adjust the length of the vector to the speed the game is running at
        horizontalMovementVector *= Time.deltaTime;

        // Apply this vector to the player's translation
        transform.Translate(horizontalMovementVector); 


        // Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Vector2 relativeUpDirection = -_gravityScript.GravityVector.normalized * JumpStrength;

            _myRigidbody.AddForce(relativeUpDirection);
        }


        // Debug - reset position
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(-3.25f, -1.25f, -10f);
            GetComponent<TimeTravel>().enabled = true;
        }
    }
}
