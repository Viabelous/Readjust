using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    public List<Item> itemPrefs = new List<Item>();

    private void Start()
    {
        foreach (String itemName in GameManager.selectedItems)
        {

        }
    }
}

