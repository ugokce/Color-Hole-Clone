﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableTypes;

[Serializable]
public class Level
{
    [SerializeField]
    public int levelNumber = 1;
    [SerializeField]
    public SerializeColor firstColor;
    [SerializeField]
    public SerializeColor secondColor;
    [SerializeField]
    public SerializeColor thirdColor;
    [SerializeField]
    public bool isCompleted = false;
    [SerializeField]
    public int subLevelPassCount = 0;

    [SerializeField]
    public List<LevelObjectData> levelObjects; 

    public Level(int levelIndex, SerializableTypes.SerializeColor firstColor, SerializableTypes.SerializeColor secondColor,
        SerializableTypes.SerializeColor thirdColor, int subLevelPassCount)
    {
        this.levelNumber = levelIndex;
        this.firstColor = firstColor;
        this.secondColor = secondColor;
        this.thirdColor = thirdColor;
        this.subLevelPassCount = subLevelPassCount;
        levelObjects = new List<LevelObjectData>();
    }

    public int getObjectCount()
    {
        return levelObjects.Count;
    }
}
