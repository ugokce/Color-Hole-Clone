using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFailedView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.getInstance().playerEvents.onPlayerFailed.AddListener(ShowUI);
    }

    private void ShowUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ReStartGame()
    {
        HideUI();
        EventManager.getInstance().playerEvents.onRestartGame.Invoke();
    }

    private void HideUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
