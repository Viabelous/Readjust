using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    private bool hasActivated = false;
    [SerializeField] private Map map;
    List<Item> itemsActivated = new List<Item>();

    private void Update()
    {
        // aktifkan semua item untuk pertama kalinya
        if (!hasActivated)
        {
            foreach (Item item in CumaBuatDebug.instance.selectedItems)
            {
                Item itemActivated = item.Clone();
                itemsActivated.Add(itemActivated);

                if (itemActivated.Adaptable)
                {
                    itemActivated.Adapting(map);
                }

                itemActivated.Activate(gameObject);
            }

            hasActivated = true;
        }

        // aktifkan efek dari item, biasanya tipe custom
        else
        {
            foreach (Item item in itemsActivated)
            {
                item.OnActivated(gameObject);
            }
        }

    }
}

