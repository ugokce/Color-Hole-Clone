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

    void Start()
    {
        holeRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Collider[] objectsInPullRange = Physics.OverlapSphere(transform.position, pullRadius);

        foreach(Collider objectToPull in objectsInPullRange)
        {
            if(objectToPull.tag == "LevelObject" && objectToPull.gameObject.layer != (int)GamePhysicsLayers.Hole)
            {
                ApplyPullForceToObject(objectToPull.gameObject);
            }
        }
    }

    private void ApplyPullForceToObject(GameObject objectToApplyForce)
    {
        Vector3 forceDir = this.transform.position - objectToApplyForce.transform.position;
        //forceDir.y = -2.5f;
        Rigidbody objectRigidbody = objectToApplyForce.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(objectToApplyForce.transform.position, this.transform.position);
        float distanceSqr = Mathf.Clamp(distance * distance, 0.1f, pullRadius);

        float forceMagnitude = (objectRigidbody.mass + holeRigidbody.mass) / distanceSqr; 

        objectToApplyForce.GetComponent<Rigidbody>().velocity = forceDir.normalized * forceMagnitude * pullForceConstant * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelObject")
        {
            other.transform.DOMove(new Vector3(this.transform.position.x, -2.5f, this.transform.position.z), 0.1f).SetEase(Ease.InOutCubic);
            other.gameObject.layer = (int)GamePhysicsLayers.Hole;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LevelObject")
        {
            other.gameObject.layer = (int)GamePhysicsLayers.Default;
        }
    }
}
