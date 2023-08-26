using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Data", menuName = "SteamScriptableObject/ScriptableObject", order = 1)] //Assetlerin oldugu kisimda bir scriptable object acmamizi saglayan kisim
public class GameData : ScriptableObject
{
    [field: SerializeField] private float totalMoney { get; set; }

    [field: SerializeField] private int levelIndex { get; set; }
    [field: SerializeField] private int fakeLevelIndex { get; set; }

    public float TotalMoney
    {
        get { return totalMoney; }
        set { totalMoney = value; }
    }

    public int LevelIndex
    {
        get { return levelIndex; }
        set
        {
            if (levelIndex == 3)
                levelIndex = 1;
            else
                levelIndex = value;
        }
    }

    public int FakeLevelIndex
    {
        get { return fakeLevelIndex; }
        set { fakeLevelIndex = value; }
    }

    [Button]
    public void Reset()
    {
        totalMoney = 0f;
        levelIndex = 1;
        fakeLevelIndex = 1;
    }
}
