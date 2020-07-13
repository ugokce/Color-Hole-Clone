using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject gateObject;
    Vector3 initialPos;

    void Start()
    {
        initialPos = gateObject.transform.position;
        EventManager.getInstance().playerEvents.onSubLevelCleared.AddListener(OpenTheDoor);
        EventManager.getInstance().playerEvents.onLevelCompleted.AddListener(ResetDoor);
    }

    private void ResetDoor(int level)
    {
        gateObject.transform.position = initialPos;
    }

    private void OpenTheDoor()
    {
        gateObject.transform.DOMoveY(-2.5f, 0.3f).OnComplete(onDoorOpened);
    }

    private void onDoorOpened()
    {
        EventManager.getInstance().playerEvents.onGateOpened.Invoke();
    }
}
