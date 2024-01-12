using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneScript : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToGameplayScene()
    {
        SceneManager.LoadScene(1); 
    }
    public void ToReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void ToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void OnQuit()
    {
        Application.Quit();  
    }
}
