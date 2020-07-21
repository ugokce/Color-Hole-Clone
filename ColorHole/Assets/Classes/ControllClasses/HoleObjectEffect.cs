using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GamePhysicsLayers
{
    Default = 0,
    Hole = 8,
}

public class HoleObjectEffect : MonoBehaviour
{
    Rigidbody holeRigidbody;
    public float pullRadius = 3;
    public float pullForceConstant = 1f;
    bool isPlayerFailed = false;

    void Start()
    {
        holeRigidbody = GetComponent<Rigidbody>();
        EventManager.getInstance().playerEvents.onPlayerFailed.AddListener(OnPlayerFailed);
        EventManager.getInstance().playerEvents.onRestartGame.AddListener(OnGameRestarted);
    }

    private void FixedUpdate()
    {
        if(isPlayerFailed)
        {
            return;
        }

        Collider[] objectsInPullRange = Physics.OverlapSphere(transform.position, pullRadius);

        foreach(Collider objectToPull in objectsInPullRange)
        {
            if (GetComponent<Collider>().enabled == false)
            {
                break;
            }

            if (objectToPull.gameObject.layer == (int)GamePhysicsLayers.Hole)
            {
                continue;
            }

            if(objectToPull.tag == "LevelObject")
            {
                ApplyPullForceToObject(objectToPull.gameObject);
            }
        }
    }

    private void OnGameRestarted()
    {
        isPlayerFailed = false;
    }

    private void OnPlayerFailed()
    {
        isPlayerFailed = true;
    }

    private void ApplyPullForceToObject(GameObject objectToApplyForce)
    {
        Vector3 forceDir = this.transform.position - objectToApplyForce.transform.position;
        Rigidbody objectRigidbody = objectToApplyForce.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(objectToApplyForce.transform.position, this.transform.position);
        float distanceSqr = Mathf.Clamp(distance * distance, 0.1f, pullRadius);

        float forceMagnitude = (objectRigidbody.mass + holeRigidbody.mass) / distanceSqr; 

        objectToApplyForce.GetComponent<Rigidbody>().velocity = forceDir.normalized * forceMagnitude * pullForceConstant * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerFailed)
        {
            return;
        }

        if (other.tag == "LevelObject")
        {
            other.DOKill();
            other.transform.DOMove(new Vector3(this.transform.position.x, -2.5f, this.transform.position.z), 0.1f);
 
            other.transform.DOScale(0.2f, 0.2f);
            other.gameObject.layer = (int)GamePhysicsLayers.Hole;
        }
    }
}
