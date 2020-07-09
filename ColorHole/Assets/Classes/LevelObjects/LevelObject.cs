using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public LevelObjectData objectData;

    public void Start()
    {
        init();

        var rendererComponent = GetComponent<Renderer>();

        if (rendererComponent == null)
        {
            return;
        }

        if (objectData.isCollectable)
        {
            rendererComponent.material = MaterialManager.getInstance().collectableMaterial;
        }
        else
        {
            rendererComponent.material = MaterialManager.getInstance().trapGateMaterial;
        }
    }

    private void init()
    {
        transform.localScale = objectData.objTransform.scale.ToVector3();
        transform.position = objectData.objTransform.position.ToVector3();
        transform.rotation = objectData.objTransform.rotation.ToQuaternion();
    }
}
