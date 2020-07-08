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
        EventManager.getInstance().playerEvents.onDestinationTargetSelected.AddListener(MoveToPoint);
        EventManager.getInstance().playerEvents.onLevelCompleted.AddListener(OnLevelCompleted);
    }

    private void Move(Vector3 direction)
    {
        if(isMovingToAPoint)
        {
            return;
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void MoveToPoint(Vector3 targetPoint)
    {
        transform.DOMove(targetPoint, 0.5f).SetEase(Ease.InOutQuad).OnStart(OnStartedMovingtoPoint).
            OnComplete(OnCompletedMovingToPoint);
    }

    private void OnStartedMovingtoPoint()
    {
        isMovingToAPoint = true;
    }

    private void OnCompletedMovingToPoint()
    {
        isMovingToAPoint = false;
    }

    private void OnLevelCompleted(int completedLevel)
    {
        transform.position = initialPosition;
    }
}
