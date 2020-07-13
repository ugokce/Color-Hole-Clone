using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    [SerializeField]
    public LevelObjectData objectData;

    public void init()
    {
        var rendererComponent = GetComponent<Renderer>();

        if (rendererComponent == null)
        {
            return;
        }

        if (objectData.isCollectable)
        {
            rendererComponent.material = MaterialManager.GetInstance().GetMaterial(MaterialType.Collectable);
        }
        else
        {
            rendererComponent.material = MaterialManager.GetInstance().GetMaterial(MaterialType.TrapOrGate);
        }

        transform.localScale = objectData.objTransform.scale.ToVector3();
        transform.position = objectData.objTransform.position.ToVector3();
        transform.rotation = objectData.objTransform.rotation.ToQuaternion();

        StartCoroutine(EnablePhysics());
    }

    IEnumerator EnablePhysics()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSecondsRealtime(1f);

        GetComponent<Rigidbody>().isKinematic = false;
    }
}
