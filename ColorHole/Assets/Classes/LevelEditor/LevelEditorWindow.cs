using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    int sublevelCompleteNumber = 0;
    int levelIndex = 0;
    Color firstColor = Color.white;
    Color secondColor = Color.white;
    Color thirdColor = Color.white;
    string errorMessages = "";


    bool isSerializationInProgress = false;

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Sub Level Complete Number", EditorStyles.boldLabel);
        sublevelCompleteNumber = EditorGUILayout.IntField("Number", sublevelCompleteNumber, EditorStyles.textField);
        EditorGUILayout.Space();
        GUILayout.Label("Level Index", EditorStyles.boldLabel);
        levelIndex = EditorGUILayout.IntField("Level", levelIndex, EditorStyles.textField);
        EditorGUILayout.Space();
        GUILayout.Label("Level Colors", EditorStyles.boldLabel);
        firstColor = EditorGUILayout.ColorField("FirstColor(Ground)", firstColor);
        secondColor = EditorGUILayout.ColorField("SecondColor(Obstacles)", secondColor);
        thirdColor = EditorGUILayout.ColorField("ThirdColor(Gates & Traps)", thirdColor);
        EditorGUILayout.Space();

        if(GUILayout.Button("Save Level To File") && !isSerializationInProgress)
        {
            isSerializationInProgress = true;
            Level newLevel = new Level(levelIndex, firstColor, secondColor, thirdColor, sublevelCompleteNumber);
            newLevel.levelObjects.InsertRange(0, findLevelObjects());

            if(newLevel.levelObjects.Count <= 0)
            {
                isSerializationInProgress = false;

                errorMessages += "Number of Created Objects Must Be Greater Than Zero";
            }

            LevelSerializer.SerializeLevel(newLevel);
            isSerializationInProgress = false;
        }

        EditorGUILayout.HelpBox(errorMessages, MessageType.Error);
    }

    private GameObject[] findLevelObjects()
    {
        return GameObject.FindGameObjectsWithTag("LevelObject");
    }
}
