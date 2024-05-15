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

                if (itemActivated.GetType() != typeof(RewardMultiplier))
                {
                    print("Aktivasi Item");
                    itemActivated.Activate(gameObject);
                }
            }

            if (CheckItem("Badge of Honour"))
            {
                BadgeOfHonourEffect();
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

    private void BadgeOfHonourEffect()
    {
        Player player = GameObject.Find("Player").GetComponent<PlayerController>().player;
        player.Downgrade(Stat.HP, player.maxHp - 1);
    }

    public bool CheckItem(string name)
    {
        // !!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!NANTI UBAH WOIII!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!

        // return GameManager.selectedItems.FindIndex(item => item.Name == name) != -1;
        return CumaBuatDebug.instance.selectedItems.FindIndex(item => item.Name == name) != -1;
    }
}

