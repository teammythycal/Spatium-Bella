using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotSpawner : MonoBehaviour
{
    public enum type {CrossShot1, CrossShot2, CrossShotAll, EightShot1, EightShot2, EightShotAll};
    public type _shotType = type.CrossShot1;
    public GameObject _projectileBase;
    private GameObject _projectile;
    private Vector2[] _projectileVelocityArray;

    //Velocity Arrays
    private Vector2[] _crossShot1Velocity =
    {
        new Vector2(0,2), new Vector2(0,-2), new Vector2(2,0), new Vector2(-2,0),
    };
    private Vector2[] _crossShot2Velocity =
    {
        new Vector2(2,2), new Vector2(-2,-2), new Vector2(2,-2), new Vector2(-2,2),
    };

    private Vector2[] _crossShotAll = new Vector2[8];
    private Vector2[] _eightShot1Velocity = 
    {
        new Vector2(0,2), new Vector2(2,0), new Vector2(0,-2), new Vector2(-2,0),
        new Vector2(1.4f,1.4f), new Vector2(1.4f,-1.4f), new Vector2(-1.4f,-1.4f), new Vector2(-1.4f,1.4f),
    };

    private Vector2[] _eightShot2Velocity = 
    {
        new Vector2(1.8f,0.8f), new Vector2(1.8f,-0.8f), new Vector2(-1.8f,0.8f), new Vector2(-1.8f,-0.8f),
        new Vector2(0.8f,1.8f), new Vector2(-0.8f,1.8f), new Vector2(0.8f,-1.8f), new Vector2(-0.8f,-1.8f),
    };
    private Vector2[] _eightShotAll = new Vector2[16];

    // Start is called before the first frame update
    void Start()
    {
        // Create the cross shot all array
        _crossShot1Velocity.CopyTo(_crossShotAll,0);
        _crossShot2Velocity.CopyTo(_crossShotAll,4);

        //Create the _eightshotall array.
        _eightShot1Velocity.CopyTo(_eightShotAll,0);
        _eightShot2Velocity.CopyTo(_eightShotAll,8);

        switch(_shotType)
        {
            case type.CrossShot1:
                _projectileVelocityArray = _crossShot1Velocity;
            break;
            case type.CrossShot2:
                _projectileVelocityArray = _crossShot2Velocity;
            break;
            case type.CrossShotAll:
                _projectileVelocityArray = _crossShotAll;
            break;
            case type.EightShot1:
                _projectileVelocityArray = _eightShot1Velocity;
            break;
            case type.EightShot2:
                _projectileVelocityArray = _eightShot2Velocity;
            break;
            case type.EightShotAll:
                _projectileVelocityArray = _eightShotAll;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _projectileVelocityArray.Length; i++)
        {
            //Create projectile
            _projectile = Instantiate(_projectileBase,this.transform.position,this.transform.rotation);

            //Set velocity
            BurstShot _burstScript = _projectile.GetComponent<BurstShot>();
            _burstScript.SetVelocity(_projectileVelocityArray[i]);
        }

        //Destroy this object when complete.
        Destroy(this.gameObject);
    }
}