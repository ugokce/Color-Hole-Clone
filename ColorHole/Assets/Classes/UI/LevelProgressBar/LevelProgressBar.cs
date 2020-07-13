using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelProgressBar : MonoBehaviour
{
    public ProgressBar firstProgressBar;
    public ProgressBar secondProgressBar;

    public TextMeshProUGUI previousLevelLabel;
    public TextMeshProUGUI nextLevelLabel;

    private void Start()
    {
        GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");

        if(controllerObj)
        {
            var levelController = controllerObj.GetComponent<LevelController>();
            levelController.onObjectCollected.AddListener(UpdateProgressBars);
        }

        EventManager.getInstance().playerEvents.onLevelCompleted.AddListener(OnLevelCompleted);
    }

    void OnLevelCompleted(int levelNumber)
    {
        previousLevelLabel.SetText((levelNumber - 1).ToString());
        nextLevelLabel.SetText(levelNumber.ToString());

        UpdateProgressBars(new List<float>{0, 0});
    }

    void UpdateProgressBars(List<float> progressValues)
    {
        firstProgressBar.SetValue(progressValues[0]);
        secondProgressBar.SetValue(progressValues[1]);
    }
}
