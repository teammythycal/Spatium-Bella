using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHandler : MonoBehaviour
{
       /*
    First variable represents the burst shot object.
    Second variable represents the position the burst shot should be created at.
    Third variable represents the rate at which the burst shot can be fired.
    */
   public GameObject _burstShot;
   public GameObject _spreadShot;
   public LineRenderer _laserCannon;
   public int _laserCannonDamage = 1;
   public float _laserCannonDamageRate = 0.25f;
   public float _burstFireRate = 0.15f;
   public float _spreadFireRate = 0.25f;
   public float _horzOffset = 0.50f;
   public float _vertOffset = 1.0f;
   private RaycastHit2D hitInfo;
   private Vector2 shotStartPos;
   private float _timer = 0;
   
    void FixedUpdate()
    {
        LaserCannon();

        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else if(_timer < 0)
        {
            _timer = 0;
        }
    }

    public void FireWeapon(int weaponNumber)
    {
        if(_timer == 0 && weaponNumber != 3)
        {
            if (weaponNumber == 2)
            {
                _timer = _spreadFireRate;
                SpreadShot();
            }
            else
            {
                _timer = _burstFireRate;
                BurstShot();
            }
        }
    }
    void BurstShot()
    {
        shotStartPos = transform.position;
        shotStartPos += new Vector2(0.0f, _vertOffset);
        Instantiate(_burstShot, shotStartPos, transform.rotation);
    }
    void SpreadShot()
    {
        shotStartPos = transform.position;
        shotStartPos += new Vector2(0.0f, _vertOffset);
        Instantiate(_spreadShot, shotStartPos, transform.rotation);
    }
    void LaserCannon()
    { 
        if (_laserCannon.enabled == true)
        {
            //Determine laser start position (Position 0)
            shotStartPos = transform.position;
            shotStartPos += new Vector2(0, _vertOffset);

            //Send a raycast upward
            hitInfo = Physics2D.Raycast(shotStartPos, transform.up);
            if (hitInfo)
            {
                Debug.Log(hitInfo.transform.name + " has been hit");
            }

            _laserCannon.SetPosition(0, shotStartPos);

            //Set laser end position (Position 1)
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                _laserCannon.SetPosition(1, hitInfo.point);
                LaserCannonDamage();
            }
            else if(hitInfo.transform.CompareTag("Destructible") || hitInfo.transform.CompareTag("Indestructable"))
            {
                if(hitInfo.transform.CompareTag("Destructible"))
                {
                    EnvironmentCollisionHandler _enviromentCollisionScript = hitInfo.transform.gameObject.GetComponent<EnvironmentCollisionHandler>();

                    Vector3 _collisionVector = new Vector3(hitInfo.point.x,hitInfo.point.y + 0.3f,0);
                    _enviromentCollisionScript.RemoveTile(_collisionVector);

                    _laserCannon.SetPosition(1, hitInfo.point);
                }
                else
                {
                    _laserCannon.SetPosition(1, hitInfo.point);
                }
            }
            else
            {
                _laserCannon.SetPosition(1, shotStartPos + new Vector2(0, 10));
            }
        }
    }
    public void LaserCannonDamage()
    {
        if (_timer == 0)
        {
           _timer = _laserCannonDamageRate;
            hitInfo.transform.gameObject.GetComponent<HealthHandler>().TakeDamage(_laserCannonDamage);
        }
    }

    public void ResetLaserPosition()
    {
        _laserCannon.SetPosition(0, new Vector2(0, 0));
        _laserCannon.SetPosition(1, new Vector2(0, 0));
    }
    
    public void LaserCannonEnable()
    {
        _laserCannon.enabled = true;
    }
    public void LaserCannonDisable()
    {
        ResetLaserPosition();
        _laserCannon.enabled = false;
    }
}
