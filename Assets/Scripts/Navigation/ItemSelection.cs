using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
    [SerializeField] private windowsController WindowsController;
    [SerializeField] private ItemWindowsController iwc;
    [SerializeField] private GameObject right;
    [SerializeField] private NavigationState currState;
    [SerializeField] private int frameNum;
    [SerializeField] private Image itemImg;
    private Item item;

    void Start()
    {
        UpdateScrollItems();
    }

    void Update()
    {
        if (currState == NavigationState.Hover)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currState = NavigationState.Focused;
                WindowsController.HoveredButton = right;
            }
        }

        UpdateScrollItems();
    }

    // public override void Clicked()
    // {
    //     throw new System.NotImplementedException();
    // }

    // public override void ExclusiveKey()
    // {
    //     throw new System.NotImplementedException();
    // }

    // public override void IsHovered(bool state)
    // {
    //     if (state)
    //     {
    //         currState = NavigationState.Hover;
    //     }
    //     else
    //     {
    //         currState = HasSelected() ? NavigationState.Selected : NavigationState.Active;
    //     }
    // }

    public bool HasSelected()
    {
        return GameManager.selectedItems.Contains(item);
    }

    public void UpdateScrollItems()
    {
        switch (frameNum)
        {
            case 1:
                item = iwc.GetItems().Count > 3 ?
                iwc.GetItems()[iwc.GetItems().Count - 2] : null;
                break;

            case 2:
                item = iwc.GetItems().Count > 4 ?
                iwc.GetItems()[iwc.GetItems().Count - 2] : null;
                break;

            case 3:
                item = iwc.GetItems()[iwc.GetFocusedIndex()];
                break;

            case 4:
                item = iwc.GetItems().Count > 1 ? iwc.GetItems()[iwc.GetFocusedIndex() + 1] : null;
                break;

            case 5:
                item = iwc.GetItems().Count > 2 ? iwc.GetItems()[iwc.GetFocusedIndex() + 2] : null;
                break;
        }

        itemImg.sprite = item.Icon;
    }

}