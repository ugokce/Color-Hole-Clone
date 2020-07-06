using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Mesh DENEME;
    public GameObject pref;
    void Start()
    {
        GameObject nem = Instantiate(pref);
        LevelObject deneme = nem.AddComponent<LevelObject>();
        deneme.isCollectable = true;
        deneme.transform.localScale = Vector3.one;
        deneme.transform.position = Vector3.zero;
        deneme.transform.rotation = Quaternion.identity;
        deneme.meshObject = DENEME;
    }

    void SerializeLevel()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
