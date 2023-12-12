using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    // Instantiate a copy of this object, and set _pastClone to it
    public GameObject PastClonePrefab;

    // The in-game clone
    private GameObject _pastClone;
    // Delay in seconds between the clone and player
    [SerializeField]
    private float _delay = 3.0f;
    // Accessing the gravity script to update the direction of gravity when teleporting to the clone
    private Gravity _gravityScript;
    // Accessing the rigid body component to reset player's velocity when teleporting to the clone
    private Rigidbody2D _myRigidbody;


    // Initialize components
    void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _gravityScript = GetComponent<Gravity>();
    }

    // When the script is first started or is re-enabled, create a past clone and set its transforms to the player's.
    void OnEnable()
    {
        _pastClone = Instantiate(PastClonePrefab);

        _pastClone.transform.position = transform.position;
        _pastClone.transform.rotation = transform.rotation;

        _pastClone.GetComponent<SpriteRenderer>().color = new Color(0.25f, 1f, 1f, 0.33f);
    }

    void Update()
    {
        // Teleport to the past clone
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = _pastClone.transform.position;
            transform.rotation = _pastClone.transform.rotation;
            
            // Stop the velocity caused by player's previous gravity
            _myRigidbody.velocity = Vector3.zero;

            // Update the direction of gravity to match the past clone's
            _gravityScript.AdjustGravityAngle(_pastClone.transform.rotation.eulerAngles.z - 90, true);

            // Destroy the past clone and disable this script
            Destroy(_pastClone);
            enabled = false;
        }
    }

    // Every fixed update (default: 50/sec.), start a coroutine. 
    // The coroutine sets the clone's position & rotation to the player's, delayed by a few seconds.
    void FixedUpdate()
    {
        Vector3 playerPosition = transform.position;
        Quaternion playerRotation = transform.rotation;

        StartCoroutine(UpdateCloneAfterDelay(playerPosition, playerRotation, _delay));
    }
    IEnumerator UpdateCloneAfterDelay(Vector3 position, Quaternion rotation, float delay)
    {
        yield return new WaitForSeconds(delay);
        _pastClone.transform.position = position;
        _pastClone.transform.rotation = rotation;
    }

    
    // When this script is disabled (e.g. after pressing "T"), stop all still-ongoing coroutines it has started.
    // Reason: the coroutine references _pastClone, which is destroyed when pressing "T".
    void OnDisable()
    {
        StopAllCoroutines();
    }
}