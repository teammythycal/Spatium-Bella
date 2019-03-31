using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
   public int _score;
   private int _deathPenalty = 1000;
   public Text _scoreTextUI;
   
    public void IncreaseScore(int scoreValue)
    {
        _score +=scoreValue;
        _scoreTextUI.text = _score.ToString();
    }
    public void DecreaseScore()
    {
        _score -= _deathPenalty;

        if (_score<=0)
        {
            _score = 0;
        }
        _scoreTextUI.text = _score.ToString();
    }
}
