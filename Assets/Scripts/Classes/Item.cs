using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

// [CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] protected string id;
    [SerializeField] protected new string name;

    [TextArea(5, 5)]
    [SerializeField] protected string description;
    [SerializeField] protected float price;

    [Header("Item Icon")]
    [SerializeField] protected Sprite icon;

    [Header("Item Buff Type")]
    [SerializeField] protected List<BuffType> types = new List<BuffType>();

    [Header("Depends On Map")]
    [SerializeField] protected bool adaptable = false;

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

    public List<BuffType> Types
    {
        get { return types; }
    }
    public bool Adaptable
    {
        get { return adaptable; }
    }

    public Sprite Icon
    {
        get { return icon; }
    }

    public Item Clone()
    {
        Item newItem = (Item)this.MemberwiseClone();
        newItem.Id += Random.Range(0, 99999);
        return newItem;
    }

    public virtual void Activate(GameObject player)
    {

    }
    public virtual void OnActivated(GameObject player)
    {

    }

    public virtual void Adapting(Map map)
    {

    }

}
