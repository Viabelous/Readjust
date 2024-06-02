using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public enum Map
{
    None,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5
}

[CreateAssetMenu]
public class MapProperty : ScriptableObject
{
    public Map map;
    public new string name;
    [TextArea(5, 10)]

    public string description;
    public Sprite preview;
    public int unlockedProgress;

    public bool HasUnlocked()
    {
        return GameManager.player.GetProgress(Player.Progress.Story) >= unlockedProgress;
    }
}