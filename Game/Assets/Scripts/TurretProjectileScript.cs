using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileScript : MonoBehaviour
{
    public float Power;

    [SerializeField]
    private float _lifetimeMax = 8.0f;
    private float _lifetime = 0.0f;

    void Update()
    {
        transform.Translate(Vector3.right * Power * Time.deltaTime);

        _lifetime += Time.deltaTime;
        if (_lifetime > _lifetimeMax)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.HasCustomTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}
