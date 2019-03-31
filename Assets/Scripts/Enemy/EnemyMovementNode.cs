using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementNode : MonoBehaviour
{
    public GameObject _nextMoveNode;
    public enum _behaviours {delete, wait};
    public _behaviours _currentBehaviour = _behaviours.delete;

    public GameObject SendNextMoveNode()
    {
        return _nextMoveNode;
    }

    public int GetBehaviour()
    {
        return (int)_currentBehaviour;
    }

    void OnDrawGizmos()
    {
        if(_currentBehaviour == _behaviours.delete)
        {
            Gizmos.color = Color.red;
        }
        else if(_currentBehaviour == _behaviours.wait)
        {
            Gizmos.color = Color.blue;
        }

        //Node location
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f,0.5f,0));

        //Draw a line that points to the next node (only if _nextmovenode isn't empty).
        if(_nextMoveNode != null)
        {
            Gizmos.DrawLine(transform.position,_nextMoveNode.transform.position);
        }
    }
}
