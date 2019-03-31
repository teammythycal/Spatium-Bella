using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    /*
    First variable below will define the velocity the player can move at.
    Second variable will define the players body.
    Third Variable reprsents the time since the last projectile was fired.
     */
   public float _velocity = 1.0f;
   public int _weaponNumber = 1;
   private Rigidbody2D _playerBody;
   private WeaponsHandler shootProjectile;
   private ShieldHandler _shield;
   private Animator _playerAnimator;
   private GameStateManager _gameStateManager;
   public bool _laserCannonEnabled = false;
   public bool _spreadShotEnabled = false;
   private bool _controlsEnabled = true;
   private bool _firingEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        _weaponNumber = 1;
        _playerBody = GetComponent<Rigidbody2D>();
        shootProjectile = GetComponent<WeaponsHandler>();
        _playerAnimator = GetComponent<Animator>();
        _shield = GameObject.Find("PlayerShield").GetComponent<ShieldHandler>();
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        ShowHideWeaponUI();
        ShowSelectedWeapon();
    }
    
    void ResetSelectedWeaponsUI()
    {
        GameObject.Find("Burst").GetComponent<RectTransform>().localPosition = new Vector3 (-130,10,0);
        GameObject.Find("Spread").GetComponent<RectTransform>().localPosition = new Vector3 (0,10,0);
        GameObject.Find("Laser").GetComponent<RectTransform>().localPosition = new Vector3 (130,10,0);
    }

    void ShowSelectedWeapon()
    {
        ResetSelectedWeaponsUI();
        switch(_weaponNumber)
        {
            case 1:
                GameObject.Find("Burst").GetComponent<RectTransform>().localPosition = new Vector3 (-130,20,0);
            break;
            case 2:
                GameObject.Find("Spread").GetComponent<RectTransform>().localPosition = new Vector3 (0,20,0);
            break;   
            case 3:
                GameObject.Find("Laser").GetComponent<RectTransform>().localPosition = new Vector3 (130,20,0);
            break;
        }
    }

    void ShowHideWeaponUI()
    {
        if(!_laserCannonEnabled)
        {
            GameObject.Find("Laser Shot Text").GetComponent<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("Laser Shot Text").GetComponent<Text>().enabled = true;
        }
        if(!_spreadShotEnabled)
        {
            GameObject.Find("Spread Shot Text").GetComponent<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("Spread Shot Text").GetComponent<Text>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameStateManager.StateIsRunning())
        {
            if(_controlsEnabled)
            {
                if(_firingEnabled)
                {
                    //Fire Weapon
                    if (Input.GetButtonDown("Fire1"))
                    {
                        shootProjectile.FireWeapon(_weaponNumber);
                    }
                    else if (Input.GetButton("Fire1") && _weaponNumber == 3) // Special firing case for the laser shot.
                    {
                        shootProjectile.LaserCannonEnable();
                    }
                    else if (Input.GetButtonUp("Fire1") && _weaponNumber == 3)
                    {
                        shootProjectile.LaserCannonDisable();
                    }
                }

                //Weapon Switching
                if (Input.GetButtonDown("Fire2"))
                {
                    //Disable laser cannon
                    shootProjectile.LaserCannonDisable();

                    //Switching logic
                    if (_weaponNumber == 1 && _spreadShotEnabled)
                    {
                        _weaponNumber = 2;
                    }
                    else if (_weaponNumber == 1 && _laserCannonEnabled)
                    {
                        _weaponNumber = 3;
                    }
                    else if (_weaponNumber == 2 && _laserCannonEnabled)
                    {
                        _weaponNumber = 3;
                    }
                    else if (_weaponNumber == 2 && _laserCannonEnabled==false)
                    {
                        _weaponNumber = 1;
                    }
                    else if (_weaponNumber == 3)
                    {
                        _weaponNumber = 1;
                    }
                    else
                    {
                        _weaponNumber = 1;
                    }
                    ShowSelectedWeapon();
                }

                //Shield Enable/Disable
                if (Input.GetButtonUp("Fire3"))
                {
                    _shield.DisableShield();
                }
                if (Input.GetButton("Fire3"))
                {
                    _shield.EnableShield();
                }

            }
        }
    }
    void FixedUpdate()
    {
            /*
            float varibles below get the horizontal and vertical axis.
            */
            int hAxis = 0;
            int vAxis = 0;

        if(_gameStateManager.StateIsRunning())
        {
            if(_controlsEnabled)
            {
                hAxis = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
                vAxis = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            }
            else
            {
                hAxis = 0;
                vAxis = 0;
            }

            /*
            sets the velocity the player can move at on the horizontal and vertical axis.
            */
            _playerBody.velocity = new Vector2(hAxis * _velocity, vAxis * _velocity);

            //Update animator
            _playerAnimator.SetInteger("horizMovement", hAxis);
        }
        else
        {
            _playerBody.velocity = new Vector2(0,0);
        }
    }

    public void ForceStopAllMovement()
    {
        _playerBody.velocity = new Vector2(0,0);
    }

    public int ReturnPlayerControllerXAxis()
    {
        return Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
    }

    public void EnableFiring()
    {
        _firingEnabled = true;
    }
    public void DisableFiring()
    {
        _firingEnabled = false;
    }

    public bool ControlsEnabled()
    {
        return _controlsEnabled;
    }

    public void DisableControls()
    {
        _controlsEnabled = false;
    }

    public void EnableControls()
    {
        _controlsEnabled = true;
    }
    public void EnableLaserCannon()
    {
        _laserCannonEnabled = true;
        ShowHideWeaponUI();
    }
    public void EnableSpreadShot()
    {
        _spreadShotEnabled = true;
        ShowHideWeaponUI();
    }
}