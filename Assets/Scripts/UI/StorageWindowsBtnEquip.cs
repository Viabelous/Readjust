using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StorageWindowsBtnEquip : Navigation
{

    // [SerializeField] Sprite hoverImg, activeImg;
    [SerializeField] Text text;
    [SerializeField] NavigationState currState;
    ShopSelection focusedObvirtu;
    bool canBuy;

    void Update()
    {
        Left = WindowsController.FocusedButton;

        print("left: " + Left.name);
        print("hovered: " + WindowsController.HoveredButton.name);

        focusedObvirtu = Left.GetComponent<ShopSelection>();

        canBuy = !GameManager.CheckUnlockedItems(focusedObvirtu.obvirtu.Name);

        if (canBuy)
        {
            text.text = "KENAKAN";
        }
        else
        {
            text.text = "LEPASKAN";
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

        if (ZoneManager.instance.CurrentState() == ZoneState.OnPopUp)
        {
            switch (WindowsController.popUp.id)
            {
                case "buy_failed":
                    if (WindowsController.popUp.GetClickedBtn() == PopUpBtnType.OK)
                    {
                        Destroy(WindowsController.popUp.gameObject);
                        ZoneManager.instance.ChangeCurrentState(ZoneState.Idle);
                    }
                    break;
            }
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
        // kalau belum pernah beli, bisa klik beli
        if (canBuy)
        {
            if (GameManager.player.aerus < focusedObvirtu.obvirtu.Price)
            {
                WindowsController.CreatePopUp(
                    "buy_failed",
                    PopUpType.OK,
                    "Anda membutuhkan lebih banyak Aerus untuk membuka skill ini."
                );
            }
            else
            {
                GameManager.player.Pay(CostType.Aerus, focusedObvirtu.obvirtu.Price);
                GameManager.unlockedItems.Add(focusedObvirtu.obvirtu.Name);
            }

        }

    }

    public override void ExclusiveKey()
    {
    }

}