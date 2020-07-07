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

            if (obj && !obj.isCollectable)
            {
                EventManager.getInstance().playerEvents.onPlayerFailed.Invoke();
            }

            Destroy(other.gameObject);
        }
    }
}
