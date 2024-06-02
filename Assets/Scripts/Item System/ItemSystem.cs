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
            foreach (Item item in GameManager.selectedItems)
            {
                print(item.Name);
                Item itemActivated = item.Clone();
                itemsActivated.Add(itemActivated);

                if (itemActivated.Adaptable)
                {
                    itemActivated.Adapting(map);
                }

                if (itemActivated.GetType() != typeof(RewardMultiplier))
                {
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
        player.Downgrade(Stat.HP, player.GetMaxHP() - 1);
    }

    public bool CheckItem(string name)
    {
        return GameManager.selectedItems.FindIndex(item => item.Name == name) != -1;
    }
}

