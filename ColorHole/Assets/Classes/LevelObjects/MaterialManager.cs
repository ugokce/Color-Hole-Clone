using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType
{
    Collectable = 0,
    Ground = 1,
    Obstacle = 2,
    TrapOrGate = 3
}

public class MaterialManager : MonoBehaviour
{
    public List<Material> gameMaterials;

    public static MaterialManager GetInstance()
    {
        return GameObject.FindWithTag("GameController").GetComponent<MaterialManager>();
    }

    public void SetMaterialColorColor(Color color, MaterialType type)
    {
        gameMaterials[(int)type].color = color;
    }

    public Material GetMaterial(MaterialType type)
    {
        return gameMaterials[(int)type];
    }
}
