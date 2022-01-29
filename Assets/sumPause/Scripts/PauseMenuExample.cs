using UnityEngine;
using System.Collections;

public class PauseMenuExample : MonoBehaviour {

    GameObject panel;

	void Start () {
        // Get panel object
        panel = GameObject.Find("PauseMenuPanel");
        if (panel == null) {
            Debug.LogError("PauseMenuPanel object not found.");
            return;
        }

        panel.SetActive(false); // Hide menu on start
	}

    // Call from inspector button
    public void ResumeGame () {
        SumPause.Status = false; // Set pause status to false
    }

    // Add/Remove the event listeners
    void OnEnable() {
        SumPause.pauseEvent += OnPause;
    }

    void OnDisable() {
        SumPause.pauseEvent -= OnPause;
    }

    /// <summary>What to do when the pause button is pressed.</summary>
    /// <param name="paused">New pause state</param>
    void OnPause(bool paused) {
        if (paused) {
            // This is what we want do when the game is paused
            panel.SetActive(true); // Show menu
            if(!GM2nd.isDead && !GM2nd.isLevelComplete)
            {
                GM2nd.audioRunning.Pause();
                //GM2nd.audioListener.enabled = false;
                GameObject[] gameobjs = GameObject.FindGameObjectsWithTag("cutPiece");
                foreach(GameObject gameobj in gameobjs)
                {
                    gameobj.GetComponent<AudioSource>().enabled = false;
                }
            }
            
        }
        else
        {
            // This is what we want to do when the game is resumed
            panel.SetActive(false); // Hide menu
            if (!GM2nd.isDead && !GM2nd.isLevelComplete)
            {
                GM2nd.audioRunning.Play();
                //GM2nd.audioListener.enabled = true;
                GameObject[] gameobjs = GameObject.FindGameObjectsWithTag("cutPiece");
                foreach (GameObject gameobj in gameobjs)
                {
                    gameobj.GetComponent<AudioSource>().enabled = true;
                }
            }
        }
    }

}
