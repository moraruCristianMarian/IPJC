using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorProjectileScript : MonoBehaviour
{
    public float Power;

    [SerializeField]
    private float _lifetimeMax = 5.0f;
    private float _lifetime = 0.0f;

    void Update()
    {
        transform.Translate(Vector3.right * Power * Time.deltaTime);

        _lifetime += Time.deltaTime;
        if (_lifetime > _lifetimeMax)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        float rotationZ = transform.rotation.eulerAngles.z;
        Vector2 vectorPush = new Vector2(Mathf.Cos(rotationZ * Mathf.Deg2Rad), Mathf.Sin(rotationZ * Mathf.Deg2Rad));
        vectorPush *= Power;

        other.GetComponent<Rigidbody2D>().AddForce(vectorPush, ForceMode2D.Impulse);
        Destroy(gameObject);
    }
}
