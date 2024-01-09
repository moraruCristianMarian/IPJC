using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject ProjectilePrefab;

    [SerializeField]
    private float _fireCooldown = 1.0f;
    private float _firedLast = 0.0f;

    void Update()
    {
        _firedLast += Time.deltaTime;
        if (_firedLast >= _fireCooldown)
        {
            _firedLast -= _fireCooldown;
            Instantiate(ProjectilePrefab, transform.position, transform.rotation);
        }
    }
}
