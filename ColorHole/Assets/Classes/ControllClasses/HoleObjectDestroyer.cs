using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelObject")
        {
            Destroy(other.gameObject);
        }
    }
}
