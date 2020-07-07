using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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
        Save(newLevel, "Assets\\Levels\\Level-" + newLevel.levelNumber.ToString());
    }

    public static Level DeSerializeLevel(int levelIndex)
    {
        return OpenLevel("Assets\\Levels\\Level-" + levelIndex.ToString());
    }
}
