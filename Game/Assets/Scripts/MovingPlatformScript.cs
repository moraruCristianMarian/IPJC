using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public GameObject[] _pathPoints = new GameObject[2];
    private Transform _currentPoint;
    private int _currentPointIndex = 0;
    

    [SerializeField]
    private float _speed = 2.0f;


    void Start()
    {
        _currentPoint = _pathPoints[1].transform;
        _currentPointIndex = 1;
    }

    void Update()
    {
        //  Move towards the current path point
        Vector2 point = _currentPoint.position - transform.position;
        Vector2 moveVector = point.normalized * _speed * Time.deltaTime;

        transform.Translate(moveVector);
        

        //  Swap to the other path point when the current one is reached
        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f)
        {
            _currentPointIndex = 1 - _currentPointIndex;
            _currentPoint = _pathPoints[_currentPointIndex].transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //  When the player steps on the platform, they become a child of the platform.
        //  This makes the player move alongside the platform instead of staying still as it moves away.
        if (other.CompareTag("Player"))
            other.transform.SetParent(this.transform);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //  When the player jumps / walks off the platform, they are no longer a child of it.
        if (other.CompareTag("Player"))
            if (transform.gameObject.activeInHierarchy) // Stops the error "Cannot set the parent of the GameObject 'Player' while activating or deactivating the parent GameObject 'MovingPlatform'."
                other.transform.SetParent(null);
    }

    //  Draw debug lines showing the platform's path
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(_pathPoints[0].transform.position, transform.position);
        Gizmos.DrawLine(_pathPoints[1].transform.position, transform.position);
    }
}
