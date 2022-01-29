using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartGame()
    {
        //SceneManager.UnloadSceneAsync("Optimized");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
