using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHandler : MonoBehaviour
{
    private CircleCollider2D shieldCollider;
    private SpriteRenderer shieldRenderer;
    private PlayerControls _playerControls;
    public int _shieldMaxHP = 5;
    public Text _shieldBarText;
    public Image _shieldSlider;
    public bool _isPlayer;
    public int _shieldHP = 0;

    void Start()
    {
        setShieldUI();
        shieldCollider = GetComponent<CircleCollider2D>();
        shieldRenderer = GetComponent<SpriteRenderer>();
        _playerControls = GetComponentInParent<PlayerControls>();
        DisableShield();
    }

    void Update()
    {
        transform.position = transform.parent.position;
    }

    public void setShieldUI()
    {
        if (_isPlayer)
        {
            _shieldBarText.text = _shieldHP + "/" + _shieldMaxHP;
            _shieldSlider.fillAmount = (float)_shieldHP / (float)_shieldMaxHP;
        }
    }
    public void TakeDamage(int damage)
    {
        if (_shieldHP <= damage)
        {
            _shieldHP = 0;
            DisableShield();
            setShieldUI();
        }
        else
        {
            _shieldHP = _shieldHP - damage;
            setShieldUI();
        }
    }

    public void EnableShield()
    {
        if (_shieldHP != 0)
        {
            shieldCollider.enabled = true;
            shieldRenderer.enabled = true;
            _playerControls.DisableFiring();
        }
    }

    public void DisableShield()
    {
        shieldCollider.enabled = false;
        shieldRenderer.enabled = false;
        _playerControls.EnableFiring();
    }

    public void AddShieldHP(int addshieldHP)
    {
        if (_shieldHP==0)//Shield was disabled but because this method is called the shield health is about to be greater than zero.
        {
            EnableShield();
        }
        if ((addshieldHP+_shieldHP) > _shieldMaxHP)
        {
            _shieldHP = _shieldMaxHP;
            setShieldUI();
            Debug.Log(_shieldHP);
        }
        else
        {
             _shieldHP += addshieldHP;
            setShieldUI();
            Debug.Log(_shieldHP);
        }
    }

    public void AddMaxShieldHp(int addMaxshieldHP)
    {
        _shieldMaxHP += addMaxshieldHP;
        setShieldUI();
    }

}