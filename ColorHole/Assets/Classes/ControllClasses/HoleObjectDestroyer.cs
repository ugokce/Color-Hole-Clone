using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelObject")
        {
            LevelObject obj = other.GetComponent<LevelObject>();

            if(!obj)
            {
                return;
            }

            if (!obj.objectData.isCollectable)
            {
                EventManager.getInstance().playerEvents.onPlayerFailed.Invoke();
            }
            else
            {
                EventManager.getInstance().playerEvents.onObjectCollected.Invoke();
            }

            other.gameObject.layer = (int)GamePhysicsLayers.Hole;
            other.transform.DOKill();
            Destroy(other.gameObject, 1);

            Debug.Log(other.gameObject.name);
        }
    }
}
