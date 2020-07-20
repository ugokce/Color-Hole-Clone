using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody holeRigidbody;
    private bool isMovingToAPoint = false;
    private Vector3 initialPosition = new Vector3(-1.49f, -1.57f, 3.6f);

    void Start()
    {
        holeRigidbody = GetComponent<Rigidbody>();

        EventManager.getInstance().playerEvents.onPlayerInput.AddListener(Move);
        EventManager.getInstance().playerEvents.onLevelCompleted.AddListener(OnLevelCompleted);
        EventManager.getInstance().playerEvents.onRestartGame.AddListener(OnRestartLevel);
        EventManager.getInstance().playerEvents.onSubLevelCleared.AddListener(PlaySubLevelCompletedAnimation);
    }

    private void Move(Vector3 direction)
    {
        if(isMovingToAPoint)
        {
            return;
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void PlaySubLevelCompletedAnimation()
    {
        if(isMovingToAPoint)
        {
            return;
        }

        Vector3 doorFront = new Vector3(-10f, -1.57f, 3.59f);
        Vector3 nextStartPoint = new Vector3(-22.27f, -1.57f, 3.59f);

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMove(doorFront, 0.5f)
            .OnStart(OnStartedMovingtoPoint)
            .OnComplete(OnArrivedToDoor));
        mySequence.Append(transform.DOMove(nextStartPoint, 3f));

        mySequence.OnComplete(OnCompletedMovingToPoint);
    }

    private void OnArrivedToDoor()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnStartedMovingtoPoint()
    {
        GetComponent<Collider>().enabled = false;
        isMovingToAPoint = true;
    }

    private void OnCompletedMovingToPoint()
    {
        isMovingToAPoint = false;
        transform.DOKill();
        EventManager.getInstance().playerEvents.onPlayerCompletedSubLevelAnim.Invoke();
    }

    private void OnLevelCompleted(int completedLevel)
    {
        transform.position = initialPosition;
    }

    private void OnRestartLevel()
    {
        transform.position = initialPosition;
    }
}
