using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounterQueue : MonoBehaviour
{
    public GameObject _enemyContainer;
    public float _timer = 0;
    public float _waitTime = 1;
    public bool _enabled = true;
    private float _yOffset = 1; // A small offset so that the enemy spawner trigger isn't directly over the camera trigger.
    private float _cameraOffset = 5; // The distance between the middle of the screen (0,0) and the top of the screen.
    private Transform[] _enemySpawners;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial timer to a longer value to allow the first enemy spawner the opportunity to activate.
        _timer = 6;

        // Initialize the enemy spawner array.
        UpdateSpawnerArray();

        //Remove first result since it's the parent object's transform.
        _enemySpawners[0] = null;
    }

    public void Enable()
    {
        _enabled = true;
        _timer = _waitTime;
    }

    public void Disable()
    {
        _enabled = false;
        _timer = _waitTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_enabled)
        {
            // Activate the timer when the enemies container does not contain enemies.
            if(_enemyContainer.transform.childCount == 0)
            {
                _timer -=  Time.deltaTime;
            }
            else // Reset the timer if enemies appeared.
            {
                _timer = _waitTime;
            }

            // If it's been x amount of seconds and there aren't any enemies to shoot at...
            // then go to the next encounter so the player isn't waiting for the screen to scroll down.
            if(_timer <= 0)
            {
                _timer = _waitTime;
                GoToNextEncounter();
            }
        }
    }

    void GoToNextEncounter()
    {
        Transform _nextSpawnerTransform = null;

        /*
        This for loop will find the location (Transform component) of the next Enemy Spawner object.
        The loop goes through the _enemyspawners array and finds the first entry that isn't null, then breaks out of the for loop.
        This object will (always) be the next enemy spawner due to two factors:
        1. Older enemy spawners will delete themeslves on activation, making their entry and any of their attached objects in the 
        array a null.
        2. Enemy spawner objects will always appear in front of its attached objects. Making the enemy spawner object the first thing 
        that is seen by the loop.
        The object won't always be at index 0 since enemy spawners will delete themselves as the game goes on and will become null
        in the array.
        */
        for(int i = 0; i < _enemySpawners.Length; i++)
        {
            if(_enemySpawners[i] != null)
            {
                _nextSpawnerTransform = _enemySpawners[i].transform;
                Debug.Log("Moving to next encounter: " + _nextSpawnerTransform.name);
                break;
            }
        }

        // Set the enemy spawner group object's location to just above the next enemy spawner.
        if(_nextSpawnerTransform != null)
        {
            this.gameObject.transform.position -= 
            new Vector3(0, _nextSpawnerTransform.position.y - (_cameraOffset + _yOffset), 0);
        }
    }

    void UpdateSpawnerArray()
    {
        _enemySpawners = this.gameObject.GetComponentsInChildren<Transform>(false);
    }
}
