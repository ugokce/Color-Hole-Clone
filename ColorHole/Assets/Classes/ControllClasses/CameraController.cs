using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 initialPos;

    void Start()
    {
        EventManager.getInstance().playerEvents.onSubLevelCleared.AddListener(MoveNextPosition);
        EventManager.getInstance().playerEvents.onLevelCompleted.AddListener(OnLevelCompleted);
        EventManager.getInstance().playerEvents.onRestartGame.AddListener(OnGameRestarted);
    }

    void OnLevelCompleted(int level)
    {
        ResetPosition();
    }

    void OnGameRestarted()
    {
        ResetPosition();
    }

    void ResetPosition()
    {
        transform.position = initialPos;
    }

    private void MoveNextPosition()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(0.5f);
        mySequence.Append(transform.DOMoveX(-24.1f, 3f));
    }
}
