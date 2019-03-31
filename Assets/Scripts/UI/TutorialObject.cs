using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public Transform _player;
    private Animator _animator;
    public float _xOffset = 3f;
    public float _yOffset = 1.3f;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Disable()
    {
        this.enabled = false;
        this.gameObject.SetActive(false);
    }

    public void Enable()
    {
        this.enabled = true;
        this.gameObject.SetActive(true);
        _animator.SetTrigger("startTutorial");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isActiveAndEnabled)
        {
            this.transform.position = new Vector2(_player.transform.position.x + _xOffset, _player.transform.position.y + _yOffset);
        }
    }
}
