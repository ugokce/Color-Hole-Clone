using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class LevelSerializer : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
        {
            Type typeToDeserialize = null;

            assemblyName = Assembly.GetExecutingAssembly().FullName;
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

            return typeToDeserialize;
        }

        return null;
    }

    public static void Save(Level levelToSave, string filePath)
    {
        try
        {
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new LevelSerializer();
            bformatter.Serialize(stream, levelToSave);
            stream.Close();
         }
        catch(IOException err)
        {
            Debug.LogError(err.Message);
        }
    }

    public static Level OpenLevel(string filePath)
    {
        Level newLevel;
        try
        {
            Stream stream = File.Open(filePath, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new LevelSerializer();
            newLevel = (Level)bformatter.Deserialize(stream);
            stream.Close();
            
            return newLevel;
        }
        catch (IOException err)
        {
            Debug.LogError(err.Message);

            return null;
        }
    }

    public static void SerializeLevel(Level newLevel)
    {
        Save(newLevel, Application.streamingAssetsPath + "/Level-" + newLevel.levelNumber.ToString());
    }

    public static Level DeSerializeLevel(int levelIndex)
    {
        string levelFilePath = Application.persistentDataPath + "/Level-" + levelIndex.ToString();

        if(!File.Exists(levelFilePath))
        {
            StreamFromFile("Level-" + levelIndex.ToString());
        }

        return OpenLevel(levelFilePath);
    }

    public static void StreamFromFile(string fileName)
    {
        string filePathFromStreamingAssets = Application.streamingAssetsPath + "/" + fileName;

        if (Application.platform != RuntimePlatform.Android)
        {
           filePathFromStreamingAssets = "file://" + filePathFromStreamingAssets;
        }
        
        var loadingRequest = UnityWebRequest.Get(filePathFromStreamingAssets);
        loadingRequest.SendWebRequest();
        int i = 0;

        while (!loadingRequest.isDone)
        {
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError || i >= 1000)
            {
                break;
            }

            i++;
        }

        if (!loadingRequest.isNetworkError && !loadingRequest.isHttpError)
        {
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, fileName), loadingRequest.downloadHandler.data);
        }
    }
}
