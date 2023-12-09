using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public float smoothRate = 3f;
    
    private void LateUpdate()
    {
        Vector3 playerPosition = player.position;
        Vector3 cameraPosition  = transform.position;

        // Camera only follows the player on X axis
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerPosition.x, smoothRate * Time.deltaTime);
        //  ... unless?
        cameraPosition.y = Mathf.Lerp(cameraPosition.y, playerPosition.y, smoothRate * Time.deltaTime);

        transform.position = cameraPosition;

        transform.rotation = player.rotation;
    }
}
