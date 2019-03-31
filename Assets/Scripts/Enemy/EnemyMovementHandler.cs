using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour
{
    public enum _behaviours {delete, wait};
    public GameObject _nextMoveNode = null;
    public float _speed = 5;
    public float _waitTime = 10;
    public _behaviours _currentBehaviour = _behaviours.delete;
    private Vector2 _movementVector;
    private Rigidbody2D _entityRigidBody2D;
    private GameStateManager _gameStateManager;
    private float _timer = 0;

    void Start()
    {
        _entityRigidBody2D = GetComponent<Rigidbody2D>();
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();

        if(_nextMoveNode != null)
        {
            _movementVector = CreateDestinationVector2();
        }
    }

    void FixedUpdate()
    {
        if(_gameStateManager.StateIsRunning() && _nextMoveNode != null)
        {
            if(_currentBehaviour == _behaviours.wait)
            {
                _timer += Time.deltaTime;

                if(_timer >= _waitTime)
                {
                    _timer = 0;
                    SetBehaviour((int)_behaviours.delete);
                }
            }
            else
            {
                StartMovement();
                _entityRigidBody2D.velocity = _movementVector.normalized * _speed;
            }
        }
        else // Stop movement when the game state is stopped.
        {
            StopMovement();
        }
    }

    public void SetBehaviour(int _behaviourIndex)
    {
        switch(_behaviourIndex)
        {
            case 0:
                _currentBehaviour = _behaviours.delete;
            break;
            case 1:
                _currentBehaviour = _behaviours.wait;
                StopMovement();
            break;
            default:
                _currentBehaviour = _behaviours.delete;
            break;
        }
    }

    public void SetSpeed(float _newSpeed)
    {
        if(_newSpeed > 0)
        {
            _speed = _newSpeed;
        }
    }
    public void StopMovement()
    {
        _entityRigidBody2D.velocity = new Vector2(0,0);
    }

    public void StartMovement()
    {
        _movementVector = CreateDestinationVector2();
    }

    public void SetMoveNode(GameObject node)
    {
        _nextMoveNode = node;
    }

    private Vector2 CreateDestinationVector2()
    {
        /*
        Creates a new Vector2 by subtracting a destination Vector2 (the _nextMoveNode's position) and a origin Vector2 (the enemy's position).
        This Vector2 points towards the _nextMoveNode, so we can set the enemy's RigidBody2D velocity to this in order to move it in the direction
        of the _nextMoveNode.
        The result is unnormalized, so normalize the result Vector2 before using it or else the movement speed will be uneven.
        */
        return _nextMoveNode.transform.position - transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!GameObject.Find("Shield").GetComponent<CircleCollider2D>().enabled)
            {
                /* The above check prevents the player hitbox from being hit while the shield is active.
                 * The player's hitbox is technically still active even though the shield hitbox is surrounding the player
                 * and can still be hit if a projectile/enemy somehow goes past the shield.
                 */
                other.gameObject.GetComponent<HealthHandler>().TakeDamage(1);
            }
        }
        else if(other.gameObject.CompareTag("PlayerShield"))
        {
            other.gameObject.GetComponent<ShieldHandler>().TakeDamage(1);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject == _nextMoveNode)
        {
            EnemyMovementNode _nextNodeScript = other.gameObject.GetComponent<EnemyMovementNode>();
            _nextMoveNode = _nextNodeScript.SendNextMoveNode();
            
            if(_nextMoveNode != null) //Re-calculate destination vector only if we received another node.
            {
                _entityRigidBody2D.velocity = CreateDestinationVector2();
                SetBehaviour(_nextNodeScript.GetBehaviour());
            }
            else // If we hit the last node, then delete this enemy.
            {
                if(_currentBehaviour == _behaviours.delete)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw a line to the attached node.
        if(_nextMoveNode != null)
        {
            Gizmos.DrawLine(transform.position,_nextMoveNode.transform.position);
        }
    }
}
