using SerializableTypes;
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

    public void SetMaterialColor(SerializeColor color, MaterialType type)
    {
        gameMaterials[(int)type].color = color.getColor();
    }

    public Material GetMaterial(MaterialType type)
    {
        return gameMaterials[(int)type];
    }
}
