using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.getInstance().playerEvents.onLevelCompleted.AddListener(OnLevelCompleted);
    }

    void OnLevelCompleted(int levelNumber)
    {
        PlayerPrefs.SetInt("Gold", levelNumber * 50);
    }

    public static int GetCurrentGold()
    {
        return PlayerPrefs.GetInt("Gold", 0);
    }
}
