using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        EventManager.getInstance().playerEvents.onCameraShouldFollowPlayer.AddListener(onCameraShouldFollowPlayer);
    }

    private void onCameraShouldFollowPlayer(Vector3 targetPosition)
    {
        targetPosition.y = this.transform.position.y;

        transform.DOMove(targetPosition, 0.1f);
    }
}
