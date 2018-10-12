using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ReplaySameArgs() // reload the same level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
