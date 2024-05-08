using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;
    }
}
