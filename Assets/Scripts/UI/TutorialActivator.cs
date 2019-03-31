using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActivator : MonoBehaviour
{
    public TutorialObject _uiTutorialObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MainCamera") && other.gameObject.name == "CameraTriggerUp")
        {
            _uiTutorialObject.Enable();
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position,GetComponent<BoxCollider2D>().size);
    }
}
