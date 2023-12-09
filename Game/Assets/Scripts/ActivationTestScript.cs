using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTestScript : MonoBehaviour
{
    void ButtonActivate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
        Debug.Log("ACTIVATED!");
    }
    void ButtonDeactivate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
        Debug.Log("DEACTIVATED!");
    }
}
