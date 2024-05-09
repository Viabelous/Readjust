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
    [SerializeField] private string description;
    [SerializeField] private float price;

    [Header("Item Buff Type")]
    [SerializeField] private BuffType type;

    [Header("Item Icon")]
    [SerializeField] private Sprite icon;

    public Item Clone()
    {
        Item newItem = (Item)this.MemberwiseClone();
        newItem.Id += Random.Range(0, 99999);
        return newItem;
    }

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }


    public float Price
    {
        get { return price; }
    }

    public BuffType Type
    {
        get { return type; }
    }

    public Sprite Icon
    {
        get { return icon; }
    }

}
