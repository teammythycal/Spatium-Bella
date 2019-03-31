using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRotation : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    public float _rotation = 1;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rotation += Time.deltaTime;
        _rigidbody2d.rotation =  _rigidbody2d.rotation + _rotation;
    }
}
