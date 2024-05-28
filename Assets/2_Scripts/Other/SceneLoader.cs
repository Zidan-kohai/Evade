using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;

        //if we will run from boot we need || sceneIndex != 1
        if (sceneIndex != 0)
        {
            DailyExerciseController.Instance.SetProgress(Days.Day1, 1);
        }
    }
}
