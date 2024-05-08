using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private new string name;
    [SerializeField] private Sprite sprite;

    public string Id
    {
        get { return id; }
    }
    public string Name
    {
        get { return name; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
    }

}
