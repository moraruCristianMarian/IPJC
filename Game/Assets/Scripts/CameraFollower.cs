using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public float smoothRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 playerPosition = player.position;
        Vector3 cameraPosition  = transform.position;

        // Camera only follows the player on X axis
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerPosition.x, smoothRate);

        transform.position = cameraPosition;
    }
}
