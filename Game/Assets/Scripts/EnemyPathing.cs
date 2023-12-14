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
    private float _baseSpeed = 3.0f;
    private float _speed;

    private float _spriteRotation;
    private float _spriteRotationPrev;


    void Start()
    {
        _speed = _baseSpeed;
        
        _spriteRotationPrev = _spriteRotation;
        GetComponent<SpriteRenderer>().flipX = true;

        _rb = GetComponent<Rigidbody2D>();

        _currentPoint = _pathPoints[1].transform;
        _currentPointIndex = 1;
    }

    void Update()
    {
        //  Update rotation to be aligned with gravity
        transform.rotation = Quaternion.FromToRotation(Vector3.down, Physics2D.gravity.normalized);

        //  Move horizontally in a direction depending on the current path point
        _speed = _baseSpeed * Mathf.Sign(transform.right.x);
        transform.Translate(new Vector2(_speed * Time.deltaTime, 0));

        _spriteRotation = Mathf.Sign(_speed * _pathPoints[0].transform.position.x);
        if (_spriteRotation != _spriteRotationPrev)
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX ;
        _spriteRotationPrev = _spriteRotation;

        //  Reverse direction and path point when the current one is reached
        if (((transform.position.x < _currentPoint.position.x) && (_currentPointIndex == 0))     ||
            ((transform.position.x > _currentPoint.position.x) && (_currentPointIndex == 1)))
        {
            _baseSpeed *= -1;

            _currentPointIndex = 1 - _currentPointIndex;
            _currentPoint = _pathPoints[_currentPointIndex].transform;
            //GetComponent<SpriteRenderer>().flipX = (_currentPointIndex == 0);
        }
    }
    
    //  Draw debug lines showing the enemy's path
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(_pathPoints[0].transform.position, transform.position);
        Gizmos.DrawLine(_pathPoints[1].transform.position, transform.position);
    }
}
