using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public GameObject[] _pathPoints = new GameObject[2];

    private Rigidbody2D _rb;
    private Transform _currentPoint;
    private int _currentPointIndex = 0;

    [SerializeField]
    private float _speed = 3.0f;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _currentPoint = _pathPoints[1].transform;
        _currentPointIndex = 1;
    }

    void Update()
    {
        //  Move horizontally in a direction depending on the current path point
        if (_currentPoint == _pathPoints[1].transform)
           transform.Translate(new Vector2(_speed * Time.deltaTime, 0));
        else
           transform.Translate(new Vector2(-_speed * Time.deltaTime, 0));

        //  Reverse direction and path point when the current one is reached
        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f)
        {
            _currentPointIndex = 1 - _currentPointIndex;
            _currentPoint = _pathPoints[_currentPointIndex].transform;
            GetComponent<SpriteRenderer>().flipX = (_currentPointIndex == 0);
        }
    }
    
    //  Draw debug lines showing the enemy's path
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(_pathPoints[0].transform.position, transform.position);
        Gizmos.DrawLine(_pathPoints[1].transform.position, transform.position);
    }
}
