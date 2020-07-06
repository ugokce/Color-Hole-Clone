using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    Level currentLevel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearLevel()
    {
        foreach (GameObject foundObject in GameObject.FindGameObjectsWithTag("LevelObject"))
        {
            Destroy(foundObject);
        }
    }
}
