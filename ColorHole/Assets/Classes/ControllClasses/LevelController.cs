using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public List<GameObject> levelObjectPrefabs;

    Level currentLevel;
    int levelIndex = 0;
    int collectedObjectCount = 0;
    bool onSubLevelCleared = false;
    public GenericEventWithList<float> onObjectCollected = new GenericEventWithList<float>();

    void Start()
    {

#if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
#endif
        levelIndex = PlayerPrefs.GetInt("CurrentLevel", 1);

        EventManager.getInstance().playerEvents.onObjectCollected.AddListener(OnObjectCollected);
        EventManager.getInstance().playerEvents.onRestartGame.AddListener(initLevel);
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
        ResetLevelValues();
        ClearLevel();
        currentLevel = LevelSerializer.DeSerializeLevel(levelIndex);
        GenerateLevelObjects();
        AssignLevelColors();
    }

    private void AssignLevelColors()
    {
        MaterialManager.GetInstance().SetMaterialColor(currentLevel.firstColor, MaterialType.Ground);
        MaterialManager.GetInstance().SetMaterialColor(currentLevel.secondColor, MaterialType.Obstacle);
        MaterialManager.GetInstance().SetMaterialColor(currentLevel.thirdColor, MaterialType.TrapOrGate);
    }

    void OnLevelCompleted()
    {
        levelIndex++;
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);

        initLevel();
    }

    void ResetLevelValues()
    {
        collectedObjectCount = 0;
        onSubLevelCleared = false;
    }

    void OnObjectCollected()
    {
        collectedObjectCount++;

        onObjectCollected.Invoke(new List<float> {GetSubLevelProgress(), GetLevelProgress()});

        if (collectedObjectCount >= currentLevel.levelPassCount)
        {
            EventManager.getInstance().playerEvents.onLevelCompleted.Invoke(currentLevel.levelNumber);
            OnLevelCompleted();
        }
        else if(collectedObjectCount >= currentLevel.subLevelPassCount && !onSubLevelCleared)
        {
            EventManager.getInstance().playerEvents.onSubLevelCleared.Invoke();
            onSubLevelCleared = true;
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
            Debug.Log("Sublevel:" + collectedObjectCount + " / " + currentLevel.subLevelPassCount);
            return 0;
        }

        Debug.Log("Level" + (collectedObjectCount - currentLevel.subLevelPassCount) + " / " + (currentLevel.levelPassCount - currentLevel.subLevelPassCount));

        return (float)(collectedObjectCount - currentLevel.subLevelPassCount) / (currentLevel.levelPassCount - currentLevel.subLevelPassCount);
    }
}
