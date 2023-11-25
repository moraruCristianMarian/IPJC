using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Accessing the gravity direction object (UI arrow in the bottom-right) to change its rotation accordingly
    public GameObject DirectionObj;

    public float GravityAngle;      // (in degrees)
    public Vector2 GravityVector;
    public Vector2 PlayerGravity;

    public float GravityStrength = 9.81f;


    // How quickly the player can rotate the direction of gravity with the arrow keys
    [SerializeField]
    private float _gravityRotationSpeed = 30.0f; 

    public bool GlobalGravityChange = false;


    void Start()
    {
        GravityAngle = 270;
        ModifyGravity();
    }


    public void AdjustGravityAngle(float amount, bool replaceValue = false)
    {
        if (replaceValue)
            GravityAngle = amount;
        else
            GravityAngle += amount * _gravityRotationSpeed * Time.deltaTime;

        ModifyGravity();
    }

    public void ModifyGravity()
    {   
        GravityAngle = GravityAngle % 360;

        // Defining the vector for gravity depending on the angle
        float gravityAngleRad = Mathf.Deg2Rad * GravityAngle;
        GravityVector.x = Mathf.Cos(gravityAngleRad);
        GravityVector.y = Mathf.Sin(gravityAngleRad);


        if (GlobalGravityChange)
            Physics2D.gravity = GravityVector.normalized * GravityStrength;
        else
            PlayerGravity = GravityVector.normalized * GravityStrength;


        // Rotate the UI arrow to show the direction of gravity
        DirectionObj.transform.rotation = Quaternion.Euler(0, 0, GravityAngle - 270);
    }


    public void ToggleGlobalGravity()
    {
        GlobalGravityChange = !GlobalGravityChange;

        PlayerGravity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = GlobalGravityChange ? 1.0f : 0.0f;
    }
}
