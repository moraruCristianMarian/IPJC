using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private GameObject[] _pathPoints = new GameObject[2];

    private Rigidbody2D _rb;
    private Transform _currentPoint;
    private int _currentPointIndex = 0;

    [SerializeField]
    private float _speed = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();


        GameObject rootParent = transform.parent.gameObject;

        Transform pointTransform;

        pointTransform = rootParent.transform.Find("PathPointA");
        _pathPoints[0] = pointTransform.gameObject;

        pointTransform = rootParent.transform.Find("PathPointB");
        _pathPoints[1] = pointTransform.gameObject;


        _currentPoint = _pathPoints[1].transform;
        _currentPointIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = _currentPoint.position - transform.position;

        if (_currentPoint == _pathPoints[1].transform)
           transform.Translate(new Vector2(_speed * Time.deltaTime, 0));
        else
           transform.Translate(new Vector2(-_speed * Time.deltaTime, 0));

        
        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f)
        {
            _currentPointIndex = 1 - _currentPointIndex;
            _currentPoint = _pathPoints[_currentPointIndex].transform;
            GetComponent<SpriteRenderer>().flipX = (_currentPointIndex == 0);
        }
    }
}
