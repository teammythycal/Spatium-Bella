using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDamage : MonoBehaviour
{
    private bool _cameraFlag = false;
    private bool _environmentFlag = false;
    private HealthHandler _playerHealthHandler;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealthHandler = GetComponent<HealthHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_cameraFlag && _environmentFlag)
        {
            _playerHealthHandler.SetHealth(0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "CameraTriggerDown")
        {
            _cameraFlag = true;
            //Debug.Log("CAM ON");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "CameraTriggerDown")
        {
            _cameraFlag = false;
            //Debug.Log("CAM OFF");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Indestructable") || other.gameObject.CompareTag("Destructible"))
        {
            _environmentFlag = true;
            //Debug.Log("ENV ON");
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
       if(other.gameObject.CompareTag("Indestructable") || other.gameObject.CompareTag("Destructible") )
        {
            _environmentFlag = false;
            //Debug.Log("ENV OFF");
        }
    }
}