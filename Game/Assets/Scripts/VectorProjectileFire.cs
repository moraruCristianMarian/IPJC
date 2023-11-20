using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorProjectile : MonoBehaviour
{
    public GameObject VectorPrefab;

    [SerializeField]
    private float _power = 2.0f;
    [SerializeField]
    private Vector2 _powerMin = new Vector2(-3.0f,-3.0f);
    [SerializeField]
    private Vector2 _powerMax = new Vector2(3.0f, 3.0f);

    private Vector2 _force;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Camera CamObj;

    private VectorProjectileTrajectory trajectory;
    
    void Start()
    {
        CamObj = GameObject.Find("Main Camera").GetComponent<Camera>();
        trajectory = GetComponent<VectorProjectileTrajectory>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPoint = CamObj.ScreenToWorldPoint(Input.mousePosition);
            _startPoint.z = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 targetPoint = CamObj.ScreenToWorldPoint(Input.mousePosition);
            targetPoint.z = 0f;

            trajectory.RenderLine(_startPoint, targetPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            trajectory.EndLine();

            _endPoint = CamObj.ScreenToWorldPoint(Input.mousePosition);
            _endPoint.z = 0f;

            Vector2 direction = _endPoint - _startPoint;
            float projAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Object proj = Instantiate(VectorPrefab, _startPoint, Quaternion.Euler(0f, 0f, projAngle));

            _force = new Vector2(Mathf.Clamp(direction.x, _powerMin.x, _powerMax.x),
                                 Mathf.Clamp(direction.y, _powerMin.y, _powerMax.y));

            (((GameObject)proj).GetComponent<VectorProjectileScript>()).Power = _force.magnitude * _power;
            Debug.Log((((GameObject)proj).GetComponent<VectorProjectileScript>()).Power);
        }
    }
}
