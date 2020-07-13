using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SerializableTypes;

public class LevelEditorWindow : EditorWindow
{
    int levelCompleteNumber = 0;
    int sublevelCompleteNumber = 0;
    int levelIndex = 0;
    Color firstColor = Color.white;
    Color secondColor = Color.white;
    Color thirdColor = Color.white;
    string errorMessages = "";


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

        if (GUILayout.Button("Save Level To File"))
        {
            Level newLevel = new Level(levelIndex, SerializeColor.fromColor(firstColor), SerializeColor.fromColor(secondColor),
                    SerializeColor.fromColor(thirdColor), sublevelCompleteNumber);
            newLevel.levelObjectsData.InsertRange(0, findLevelObjects());
            newLevel.levelPassCount = levelCompleteNumber;

            if (newLevel.levelObjectsData.Count <= 0)
            {
                errorMessages += "Number of Created Objects Must Be Greater Than Zero";
            }

            try
            {
                LevelSerializer.SerializeLevel(newLevel);
                AssetDatabase.Refresh();
            }
            catch(Exception err)
            {
                Debug.LogError("ERROR SAVING FILE");
                errorMessages += err.Message;
            }
        }

        if(errorMessages != "")
        {
            EditorGUILayout.HelpBox(errorMessages, MessageType.Error);

            if (GUILayout.Button("Clear"))
            {
                errorMessages = "";
            }
        }
    }

    private List<LevelObjectData> findLevelObjects()
    {
        List<LevelObjectData> levelObjects = new List<LevelObjectData>();
        levelCompleteNumber = 0;

        foreach (GameObject foundObject in GameObject.FindGameObjectsWithTag("LevelObject"))
        {
            if(foundObject.GetComponent<LevelObject>())
            {
                LevelObjectData tempObject = foundObject.GetComponent<LevelObject>().objectData;
                tempObject.objTransform.position = new SerializeVector3(foundObject.transform.position);
                tempObject.objTransform.rotation = new SerializeQuaternion(foundObject.transform.rotation);
                tempObject.objTransform.scale = new SerializeVector3(foundObject.transform.localScale);

                levelObjects.Add(tempObject);

                if(tempObject.isCollectable)
                {
                    levelCompleteNumber++;
                }
            }
        }

        return levelObjects;
    }
}
