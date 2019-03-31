using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum direction {left,right,up,down};
    public GameObject _enemyObject;
    public GameObject _nodeObject;
    public Transform _enemySpawnLocation;
    public Transform _nodeSpawnLocation;
    public Transform _enemyObjectContainer;
    public Transform _movementNodeContainer;
    public int _amount = 1;
    public direction _spawnDirection = direction.left; 
    public float _speedOverride = 0;
    public float _fireRateOverride = 0;
    public float _spawnOffset = 1;

    void MultipleEnemySpawn()
    {
        for(int i = 0; i < _amount; i++)
        {
            switch(_spawnDirection)
            {
                case direction.left:
                    _enemySpawnLocation.position -= new Vector3(_spawnOffset,0,0);
                break;
                case direction.right:
                    _enemySpawnLocation.position += new Vector3(_spawnOffset,0,0);
                break;
                case direction.up:
                    _enemySpawnLocation.position += new Vector3(0,_spawnOffset,0);
                break;
                case direction.down:
                    _enemySpawnLocation.position -= new Vector3(0,_spawnOffset,0);
                break;
            }
            //
            SpawnEnemy();
        }

        Destroy(this.gameObject);
    }

    void SpawnEnemy()
    {
        GameObject _newEnemyObject = Instantiate(_enemyObject,_enemySpawnLocation.transform.position,_enemySpawnLocation.transform.rotation,_enemyObjectContainer);
        EnemyMovementHandler _enemyMovementHandler = _newEnemyObject.GetComponent<EnemyMovementHandler>();
        EnemyShotHandler _enemyShotHandler = _newEnemyObject.GetComponent<EnemyShotHandler>();

        //Set new speed
        if(_speedOverride > 0)
        {
            _enemyMovementHandler.SetSpeed(_speedOverride);
        }

        //Attach node if available
        if(_nodeObject != null)
        {
            // Attach the node to the enemy object.
            _enemyMovementHandler.SetMoveNode(_nodeObject);
        }

        //Override fire rate
        if(_fireRateOverride > 0)
        {
            _enemyShotHandler.ChangeFrequency(_fireRateOverride);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        {
            // Spawn movement node if available.
            if(_nodeObject != null)
            {
                _nodeObject = Instantiate(_nodeObject,_nodeSpawnLocation.transform.position,_nodeSpawnLocation.transform.rotation,_movementNodeContainer);
            }

             // Spawn first enemy
            SpawnEnemy();
            _amount -= 1;

            // Spawn additional enemies.
            if(_amount > 1)
            {
                MultipleEnemySpawn();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Edge collider position
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x + 5, transform.position.y));
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x - 5, transform.position.y));

        Gizmos.color = Color.red;

        // Enemy object spawn location
        Gizmos.DrawWireCube(_enemySpawnLocation.transform.position, new Vector3(0.5f,0.5f,0.5f));

        Gizmos.color = Color.green;

        // Node object spawn location
        Gizmos.DrawWireCube(_nodeSpawnLocation.transform.position, new Vector3(0.5f,0.5f,0.5f));
    }
}
