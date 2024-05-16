using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class NPC : ScriptableObject
{
    [SerializeField] protected new string name;
    [SerializeField] protected Sprite pict;

    [Header("List of Dialogue")]
    [TextArea(15,15)]
    [SerializeField] protected string[] dialogue;
    public string Name
    {
        get { return name; }
    }

    public Sprite Pict
    {
        get { return pict; }
    }

    public string[] Dialogue
    {
        get { return dialogue; }
    }

}
