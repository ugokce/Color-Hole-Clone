using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private static MaterialManager _instance;

    public Material collectableMaterial;
    public Material groundMaterial;
    public Material obstacleMaterial;
    public Material trapGateMaterial;

    public static MaterialManager getInstance()
    {
        if (_instance == null)
        {
            _instance = new MaterialManager();
            return _instance;
        }
        else
        {
            return _instance;
        }
    }
}
