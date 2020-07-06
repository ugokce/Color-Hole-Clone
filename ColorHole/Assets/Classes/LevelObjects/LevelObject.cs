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
    [SerializeField]
    public Transform objTransform;
    [SerializeField]
    public Mesh meshObject;

    public void Start()
    {
        init();

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

    private void init()
    {
        transform.localScale = objTransform.localScale;
        transform.position = objTransform.position;
        transform.rotation = objTransform.rotation;
        this.gameObject.AddComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = meshObject;
        this.gameObject.AddComponent<MeshCollider>().convex = true;
        this.gameObject.AddComponent<Rigidbody>();
    }
}
