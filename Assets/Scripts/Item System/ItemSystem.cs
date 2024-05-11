using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    private bool hasActivated = false;
    [SerializeField] private Map map;

    // private void Start()
    // {
    //     print("items: " + CumaBuatDebug.instance.selectedItems.Count);

    //     // !!!!!!!!!!!!!!!!!!!!!!!!
    //     // !!!!NANTI UBAH WOIII!!!!
    //     // !!!!!!!!!!!!!!!!!!!!!!!!


    // }


    private void Update()
    {
        if (!hasActivated)
        {
            // foreach (Item item in GameManager.selectedItems)
            foreach (Item item in CumaBuatDebug.instance.selectedItems)
            {
                Item itemActivated = item.Clone();

                if (itemActivated.Adaptable)
                {
                    itemActivated.Adapting(map);
                }

                itemActivated.Activate(gameObject);
            }

            hasActivated = true;
            return;
        }
    }
}

