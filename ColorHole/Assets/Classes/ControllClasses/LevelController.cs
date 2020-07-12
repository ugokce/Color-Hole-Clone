using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<GameObject> levelObjectPrefabs;

    Level currentLevel;
    int levelIndex = 0;
    int collectedObjectCount = 0;
    void Start()
    {
        levelIndex = PlayerPrefs.GetInt("CurrentLevel", 1);

        EventManager.getInstance().playerEvents.onObjectCollected.AddListener(OnObjectCollected);

        initLevel();
    }

    public void GenerateLevelObjects()
    {
        foreach (var obj in currentLevel.levelObjectsData)
        {
            GameObject newLevelObject = Instantiate(levelObjectPrefabs[(int)obj.meshType]);
            newLevelObject.AddComponent<LevelObject>();
            newLevelObject.GetComponent<LevelObject>().objectData.CopyValues(obj);
            newLevelObject.GetComponent<LevelObject>().init();
        }
    }

    public void ClearLevel()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("LevelObject"))
        {
            Destroy(obj);
        }
    }

    public void initLevel()
    {
        ClearLevel();
        currentLevel = LevelSerializer.DeSerializeLevel(levelIndex);
        GenerateLevelObjects();
    }

    void OnLevelCompleted()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel.levelNumber);
        levelIndex++;
        initLevel();
    }

    void OnObjectCollected()
    {
        collectedObjectCount++;

        if(collectedObjectCount >= currentLevel.levelObjectsData.Count)
        {
            EventManager.getInstance().playerEvents.onLevelCompleted.Invoke(currentLevel.levelNumber);
        }
        else if(collectedObjectCount >= currentLevel.subLevelPassCount)
        {
            OnLevelCompleted();
            EventManager.getInstance().playerEvents.onSubLevelCleared.Invoke();
        }
    }

    public float GetSubLevelProgress()
    {
        if(currentLevel != null && collectedObjectCount < currentLevel.subLevelPassCount)
        {
            return (float)collectedObjectCount / currentLevel.subLevelPassCount;
        }

        return 1;
    }

    public float GetLevelProgress()
    {
        if(collectedObjectCount < currentLevel.subLevelPassCount)
        {
            return 0;
        }

        return (collectedObjectCount - currentLevel.subLevelPassCount) / (currentLevel.levelObjectsData.Count - currentLevel.subLevelPassCount);
    }
}
