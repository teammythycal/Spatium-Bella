using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementNodeEraser : MonoBehaviour
{
    private bool _startDestroyTimer = false;
    private float _destroyTimer = 5.0f;
    void Update()
    {
        if(_startDestroyTimer)
        {
            _destroyTimer -= Time.deltaTime;
            if(_destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MovementNode"))
        {
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("MainCamera") && other.gameObject.name == "CameraTriggerDown")
        {
            _startDestroyTimer = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position,GetComponent<BoxCollider2D>().size);
    }
}
