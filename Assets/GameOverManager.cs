using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Quit() {
        Application.Quit();
    }

    public void Restart(){
        // Application.LoadLevel (Application.loadedLevel);
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }
}
