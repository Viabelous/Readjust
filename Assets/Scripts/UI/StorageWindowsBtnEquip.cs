using UnityEngine;
using UnityEngine.UI;

public class StorageWindowsBtnEquip : Navigation
{
    [SerializeField] Text text;
    [SerializeField] NavigationState currState;
    [SerializeField] StorageManager storageManager;
    [SerializeField] Image[] displayEquipment;
    bool isEquiped;
    bool isFull;

    void Start()
    {
        refresh();
    }

    void Update()
    {
        if (storageManager.focusedObvirtu != null)
        {
            isEquiped = GameManager.CheckSelectedItems(storageManager.focusedObvirtu);
            isFull = GameManager.selectedItems.Count >= 3;

            if (isEquiped)
            {
                text.text = "LEPASKAN";
            }
            else
            {
                text.text = isFull ? "PENUH" : "KENAKAN";
            }
        }
        else
        {
            text.text = "KOSONG";
        }

        switch (currState)
        {
            case NavigationState.Active:
                ImageComponent.sprite = BasicSprite;

                break;
            case NavigationState.Hover:
                ImageComponent.sprite = HoverSprite;
                break;
        }

    }

    public override void IsHovered(bool state)
    {
        if (state)
        {
            currState = NavigationState.Hover;
        }
        else
        {
            currState = NavigationState.Active;
        }
    }

    public override void Clicked()
    {

        if (storageManager.focusedObvirtu != null)
        {
            if (isEquiped)
            {
                GameManager.selectedItems.Remove(storageManager.focusedObvirtu);
                refresh();

            }
            else
            {
                if (!isFull) GameManager.selectedItems.Add(storageManager.focusedObvirtu);
                refresh();
            }
        }

    }

    public override void ExclusiveKey()
    {
    }

    private void refresh()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i <= GameManager.selectedItems.Count - 1)
            {
                displayEquipment[i].sprite = GameManager.selectedItems[i].Icon;
                displayEquipment[i].color = new Color(displayEquipment[i].color.r,
                                                    displayEquipment[i].color.g,
                                                    displayEquipment[i].color.b,
                                                    255f);
            }
            else
            {
                displayEquipment[i].color = new Color(displayEquipment[i].color.r,
                                                    displayEquipment[i].color.g,
                                                    displayEquipment[i].color.b,
                                                    0f);
            }
        }
    }
}