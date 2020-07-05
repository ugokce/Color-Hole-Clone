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

    void Start()
    {
        holeRigidbody = GetComponent<Rigidbody>();

        EventManager.getInstance().playerEvents.onPlayerInput.AddListener(Move);
        EventManager.getInstance().playerEvents.onDestinationTargetSelected.AddListener(MoveToPoint);
    }

    private void Move(Vector3 direction)
    {
        if(isMovingToAPoint)
        {
            return;
        }
     
        holeRigidbody.velocity = direction * moveSpeed;
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
}
