using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    private SceneController _sceneController;
    private Animator _animator;
    private bool _introSkipped = false;

    // Start is called before the first frame update
    void Start()
    {
        _sceneController = GetComponent<SceneController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.anyKeyDown && _introSkipped == false)
        {
            SkipIntro();
        }
    }

    public void SkipIntro()
    {
        _animator.SetTrigger("skipIntro");
        _introSkipped = true;
    }

    public void StartGame()
    {
        _animator.SetTrigger("startGame");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
