using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQueueActivator : MonoBehaviour
{
    public EnemyEncounterQueue _enemyEncounterQueue;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MainCamera") && other.gameObject.name == "CameraTriggerUp")
        {
            _enemyEncounterQueue.Enable();
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position,GetComponent<BoxCollider2D>().size);
    }
}