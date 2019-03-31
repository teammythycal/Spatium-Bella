using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public GameObject _enemiesContainer;
    private int _activeEnemies;
    private GameStateManager _gameStateManager;
    private bool _startCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_startCheck)
        {
            //Find out how many enemies are in the enemies container.
            _activeEnemies = _enemiesContainer.transform.childCount;

            //If all enemies are defeated, then end the game.
            if(_activeEnemies <= 0)
            {
                _gameStateManager.SetEndLevel();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        {
            //Find out how many enemies there are. This should be greater than one since the boss should already be active.
            _activeEnemies = _enemiesContainer.transform.childCount;
            _startCheck = true;
        }
    }

    void OnDrawGizmos()
    {
        if(_startCheck == false)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        // Edge collider position
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x + 5, transform.position.y));
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x - 5, transform.position.y));
    }
}
