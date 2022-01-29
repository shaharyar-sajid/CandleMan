using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideGuide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("score", 0) == 0)
        {
            StartCoroutine(StopGuide());
        }
        else
        {
            disableButton();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void disableButton()
    {
        gameObject.SetActive(false);
    }

    IEnumerator StopGuide()
    {
        yield return new WaitForSeconds(3);
        disableButton();
    }
}
