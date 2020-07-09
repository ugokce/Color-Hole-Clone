using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressBar : MonoBehaviour
{
    public ProgressBar firstProgressBar;
    public ProgressBar secondProgressBar;
    private LevelController levelController;

    private void Start()
    {
        GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");

        if(controllerObj)
        {
            levelController = controllerObj.GetComponent<LevelController>();
            EventManager.getInstance().playerEvents.onObjectCollected.AddListener(UpdateProgressBars);
        }
    }

    void UpdateProgressBars()
    {
        firstProgressBar.SetValue(levelController.GetSubLevelProgress());
        secondProgressBar.SetValue((levelController.GetLevelProgress()));
    }
}
