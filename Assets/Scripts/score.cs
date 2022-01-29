using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class score : MonoBehaviour
{

    static public int _score;
    static public bool _scoreUpdate;

    // Start is called before the first frame update
    void Start()
    {
        _scoreUpdate = true;
        _score = PlayerPrefs.GetInt("score", 0);
        UpdateScore(_score);
        StartCoroutine(increaseScore());
    }

    void UpdateScore(int score)
    {
        _score = score;
        GetComponent<Text>().text = "Score: " + _score.ToString();
    }

    void AddScore(int score)
    {
        _score += score;
        if (_score < 0)
            _score = 0;
        GetComponent<Text>().text = "Score: " + _score.ToString();
    }

    IEnumerator increaseScore()
    {
        yield return new WaitForSeconds(0.25f);
        if (_scoreUpdate)
            AddScore(100);
        else
            AddScore(0);
        StartCoroutine(increaseScore());
    }
}
