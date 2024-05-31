using System.Collections;
using UnityEngine;

public class MainMenuWindow : Window
{

    [SerializeField] private GameObject iconHandler;
    [SerializeField] private DailyExerciseView exerciseView;

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        if (exerciseView.HasRewardThatNoneClaim())
        {
            iconHandler.SetActive(true);
        }
        else
        {
            iconHandler.SetActive(false);
        }
    }
}
