using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Accessing the gravity direction object (UI arrow in the bottom-right) to change its rotation accordingly
    public GameObject DirectionObj;

    public float GravityAngle;      // (in degrees)
    public Vector2 GravityVector;

    public float GravityStrength = 9.81f;


    // How quickly the player can rotate the direction of gravity with the arrow keys
    [SerializeField]
    private float _gravityRotationSpeed = 40.0f; 



    void Start()
    {
        GravityAngle = 270;
        ModifyGravity(true);
    }

    void Update()
    {
        ModifyGravity(false);
    }

    // If "forceUpdate" is true, the entire function will execute (used for initialization, & from scripts like TimeTravel)
    public void ModifyGravity(bool forceUpdate)
    {   
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GravityAngle += _gravityRotationSpeed * Time.deltaTime;
            forceUpdate = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GravityAngle -= _gravityRotationSpeed * Time.deltaTime;
            forceUpdate = true;
        }

        // If an arrow wasn't pressed, and the function wasn't called with forceUpdate = true, it is not necessary to execute
        // the rest of this function, since the gravity angle was not modified by the player.
        if (!forceUpdate)
            return;


        GravityAngle = GravityAngle % 360;

        // Defining the vector for gravity depending on the angle
        float gravityAngleRad = Mathf.Deg2Rad * GravityAngle;
        GravityVector.x = Mathf.Cos(gravityAngleRad);
        GravityVector.y = Mathf.Sin(gravityAngleRad);

        Physics2D.gravity = GravityVector.normalized * GravityStrength;


        // Rotate the UI arrow to show the direction of gravity
        DirectionObj.transform.rotation = Quaternion.Euler(0, 0, GravityAngle - 270);
    }
}
