using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level : MonoBehaviour
{
    [SerializeField]
    public int levelNumber = 0;
    [SerializeField]
    public Color firstColor;
    [SerializeField]
    public Color secondColor;
    [SerializeField]
    public Color thirdColor;
    [SerializeField]
    public bool isCompleted = false;
    [SerializeField]
    public int subLevelPassCount = 0;

    [SerializeField]
    public List<LevelObject> levelObjects; 

    public Level(int levelIndex, Color firstColor, Color secondColor,
        Color thirdColor, int subLevelPassCount)
    {
        this.levelNumber = levelIndex;
        this.firstColor = firstColor;
        this.secondColor = secondColor;
        this.thirdColor = thirdColor;
        this.subLevelPassCount = subLevelPassCount;
        levelObjects = new List<LevelObject>();
    }

    public void GenerateLevelObjects()
    {
        foreach(var objects in levelObjects)
        {
            Instantiate(objects);
        }
    }

    public int getObjectCount()
    {
        return levelObjects.Count;
    }

    public void initLevel()
    {

    }

  
}
