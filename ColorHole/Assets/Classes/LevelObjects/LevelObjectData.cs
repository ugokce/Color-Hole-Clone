using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using SerializableTypes;

[System.Serializable]
public class LevelObjectData
{
    [SerializeField]
    public bool isCollectable = true;
    [SerializeField]
    public SerialzeTransform objTransform;

    public LevelObjectData(bool isCollectable, Transform objTransform)
    {
        this.isCollectable = isCollectable;
        this.objTransform = new SerialzeTransform(objTransform);
    }

    public void CopyValues(LevelObjectData newObjectValues)
    {
        objTransform = newObjectValues.objTransform;
        isCollectable = newObjectValues.isCollectable;
    }
}
