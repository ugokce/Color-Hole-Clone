using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GamePhysicsLayers
{
    Hole = 8,
    Ground = 9,
}

public class HoleObjectEffect : MonoBehaviour
{
    Rigidbody holeRigidbody;
    public float pullRadius = 3;


    void Start()
    {
        holeRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Collider[] objectsInPullRange = Physics.OverlapSphere(transform.position, pullRadius);

        foreach(Collider objectToPull in objectsInPullRange)
        {
            ApplyPullForceToObject(objectToPull.gameObject);
        }
    }

    private void ApplyPullForceToObject(GameObject objectToApplyForce)
    {
        Vector3 forceDir = this.transform.position - objectToApplyForce.transform.position;
        Rigidbody objectRigidbody = objectToApplyForce.GetComponent<Rigidbody>();
        float distanceSqr = Vector3.Distance(objectToApplyForce.transform.position, this.transform.position) *
             Vector3.Distance(objectToApplyForce.transform.position, this.transform.position);

        //in the game, pull force was more powerfull on farther objects
        float forceMagnitude = (objectRigidbody.mass + holeRigidbody.mass) * distanceSqr; 

        objectToApplyForce.GetComponent<Rigidbody>().velocity = forceDir.normalized * forceMagnitude * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LevelObject")
        {
            other.gameObject.layer = (int)GamePhysicsLayers.Hole;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LevelObject")
        {
            other.gameObject.layer = (int)GamePhysicsLayers.Ground;
        }
    }
}
