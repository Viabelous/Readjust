using UnityEngine;

[CreateAssetMenu]
public class NPC : ScriptableObject
{
    [SerializeField] protected new string name;
    [SerializeField] protected Sprite pict;

    [Header("List of Dialogue")]
    [TextArea(15, 15)]
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
