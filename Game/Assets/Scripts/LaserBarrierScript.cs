using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarrierScript : MonoBehaviour
{
    private Renderer _renderer;
    private BoxCollider2D _collider;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void ButtonActivate()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
    }
    void ButtonDeactivate()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
    }
}
