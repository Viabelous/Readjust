using System.Collections.Generic;
using UnityEngine;

public class ItemWindowsController : MonoBehaviour
{
    // NANTI UBAH INI PAS UDAH ADA TOKO!!!
    [SerializeField] List<Item> items = new List<Item>();
    private int focusedIndex;
    private Item focusedItem;

    void Start()
    {
        focusedIndex = 0;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (focusedIndex + 1 < items.Count)
            {
                focusedIndex += 1;
                UpdateFocusedItem();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (items.Count > 3)
            {
                focusedIndex = items.Count - 1;
                UpdateFocusedItem();
            }
        }
    }

    private void UpdateFocusedItem()
    {
        focusedItem = items[focusedIndex];
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public Item GetFocusedItem()
    {
        return focusedItem;
    }

    public int GetFocusedIndex()
    {
        return focusedIndex;
    }

    // public List<Item> GetShowedItem()
    // {
    //     return showedItem;
    // }
}