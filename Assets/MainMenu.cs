using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update


    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);

    }

    public void StopGame()
    {
        Application.Quit();
    }

}


