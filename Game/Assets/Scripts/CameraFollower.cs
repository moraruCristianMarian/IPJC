using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Player;
    public float SmoothRate = 3f;
    
    private void LateUpdate()
    {
        Vector3 playerPosition = Player.position;
        Vector3 cameraPosition  = transform.position;

        // Camera only follows the player on X axis
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerPosition.x, SmoothRate * Time.deltaTime);
        //  ... unless?
        cameraPosition.y = Mathf.Lerp(cameraPosition.y, playerPosition.y, SmoothRate * Time.deltaTime);

        transform.position = cameraPosition;

        transform.rotation = Player.rotation;
    }
}
