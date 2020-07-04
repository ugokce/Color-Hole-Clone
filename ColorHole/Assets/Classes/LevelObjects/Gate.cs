using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject gateObject;

    void Start()
    {
        
    }

    private void OpenTheDoor()
    {
        gateObject.transform.DOMoveY(-1f, 0.3f).OnComplete(onDoorOpened);
    }

    private void onDoorOpened()
    {
        EventManager.getInstance().playerEvents.onGateOpened.Invoke();
    }
}
