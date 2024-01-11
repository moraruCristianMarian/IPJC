using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScript : MonoBehaviour
{
    void Start()
    {
        //  Resets gravity when a level is (re)started
        Physics2D.gravity = Vector2.down * 9.81f;
    }
}
