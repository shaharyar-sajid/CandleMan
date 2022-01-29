using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class progressBar : MonoBehaviour
{
    public GameObject progressbar;
    public GameObject character;
    public Image restartButton;
    int totalseconds = 40;
    int secondsRemaining;
    int stopObsctacleTime = 9;
    int stopiterateTime = 6;
    static public RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        secondsRemaining = totalseconds;
        rt = progressbar.transform as RectTransform;
        rt.sizeDelta = new Vector2(0f, 100f);
        //rt.sizeDelta = new Vector2(100f / totalseconds * (totalseconds - secondsRemaining), 100f);
        StartCoroutine(increaseBar());
    }

    IEnumerator increaseBar()
    {
        if(!GM2nd.isDead && !GM2nd.isLevelComplete)
        {
            yield return new WaitForSeconds(1);
            if(rt.sizeDelta.x < (100f-(100f / totalseconds)))
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x + (100f / totalseconds), rt.sizeDelta.y);
            }
            secondsRemaining--;
            if (secondsRemaining == stopObsctacleTime)
            {
                tileCreatev2.obstacles = false;
            }
            else if (secondsRemaining == stopiterateTime)
            {
                tileCreatev2.iterate = false;
            }
            //Iteration
            if (secondsRemaining != 0)
            {
                StartCoroutine(increaseBar());
            }
        }
    }
}
