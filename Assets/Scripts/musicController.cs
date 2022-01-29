using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicController : MonoBehaviour
{
    bool isPlayed;
    Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        isPlayed = true;
        GetComponent<Image>().color = new Color32(72, 128, 64, 255);
        textComponent = transform.Find("Text").gameObject.GetComponent<Text>();
        textComponent.text = "Music ON";
    }

    public void ToggleMusic()
    {
        if(isPlayed)
        {

            GetComponent<Image>().color = new Color32(128, 64, 64, 255);
            textComponent.text = "Music OFF";
            isPlayed = false;
            GM2nd.audioMusic.Pause();
        }
        else
        {
            GetComponent<Image>().color = new Color32(72, 128, 64, 255);
            textComponent.text = "Music ON";
            isPlayed = true;
            GM2nd.audioMusic.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
