using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesHandler : MonoBehaviour
{
    public int _lives = 3;
    public bool _isDead = false;
    private bool _isInvincible = false;
    private float _timer = 0;
    private float _respawnTimerMax = 2;
    private Vector2 _playerRespawnPoint;
    private PlayerControls _playerControls;
    private SpriteRenderer _playerSpriteRenderer;
    private HealthHandler _playerHealthHandler;
    private Collider2D _playerCollider;
    private WeaponsHandler _playerWeaponsHandler;
    private GameStateManager _gameStateManager;
    private Animator _playerAnimator;
    private Text _livesUIText;

    // Start is called before the first frame update
    void Start()
    {
        _playerControls = GetComponent<PlayerControls>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerHealthHandler = GetComponent<HealthHandler>();
        _playerCollider = GetComponent<Collider2D>();
        _playerWeaponsHandler = GetComponent<WeaponsHandler>();
        _playerRespawnPoint = new Vector2(0, -3);
        _gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        _livesUIText = GameObject.Find("Lives Counter").GetComponent<Text>();
        _playerAnimator = GetComponent<Animator>();
        RefreshUIText();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInvincible)
        {
            _timer += Time.deltaTime;
            if(_timer >= 3)
            {
                _timer = 0;
                _isInvincible = false;
                _playerAnimator.SetBool("isInvincible", _isInvincible);
            }
        }

        // Check if the player ran out of lives.
        if(_lives <= 0)
        {
            _gameStateManager.SetContinue();
        }

        // Check if we need to respawn the player.
        if(_isDead && _lives > 0)
        {
            _timer += Time.deltaTime;
            if(_timer >= _respawnTimerMax)
            {
                _timer = 0;
                PlayerRespawn();
            }
        }
    }

    public void PlayerDeathStart()
    {
        _playerControls.ForceStopAllMovement();
        _playerControls.DisableControls();
        _playerWeaponsHandler.LaserCannonDisable();
        _playerCollider.enabled = false;
        _isDead = true;
    }

    public void PlayerDeathEnd()
    {
        _playerSpriteRenderer.enabled = false;
        _lives -= 1;
        RefreshUIText();
    }

    void PlayerRespawn()
    {
        _playerHealthHandler.SetHealth(_playerHealthHandler._maxHP);
        _isInvincible = true;
        _playerAnimator.SetBool("isInvincible", _isInvincible);
        _isDead = false;
        transform.position = _playerRespawnPoint;
        _playerControls.EnableControls();
        _playerCollider.enabled = true;
        _playerSpriteRenderer.enabled = true;
    }

    public void RefreshLives()
    {
        _lives = 3;
        RefreshUIText();
    }

    public bool IsPlayerInvincible()
    {
        return _isInvincible;
    }

    void RefreshUIText()
    {
        _livesUIText.text = _lives.ToString();
    }
}
