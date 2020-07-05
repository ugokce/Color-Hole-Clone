using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
public class LevelObject : MonoBehaviour
{
    [SerializeField]
    public Color objectColor = Color.white;
    [SerializeField]
    public bool isCollectable = true;

    public void Start()
    {
        var rendererComponent = GetComponent<Renderer>();

        if(rendererComponent == null)
        {
            return;
        }

        if (isCollectable)
        {
            rendererComponent.material = MaterialManager.getInstance().collectableMaterial;
        }
        else
        {
            rendererComponent.material = MaterialManager.getInstance().trapGateMaterial;
        }
    }
}
