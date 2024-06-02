using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatSelection : Navigation
{
    [SerializeField] private NavigationState currState;
    [SerializeField] public Player.Progress type;
    private Color currentColor;

    void Start()
    {
        currentColor = ImageComponent.color;
        // if (currState == NavigationState.Hover)
        // {
        //     WindowsController.FocusedButton = gameObject;
        // }
    }

    void Update()
    {
        switch (currState)
        {
            case NavigationState.Active:
                currentColor.r = 0.3f;
                currentColor.g = 0.3f;
                currentColor.b = 0.3f;
                ImageComponent.color = currentColor;
                break;

            case NavigationState.Hover:
                WindowsController.FocusedButton = gameObject;

                // tutup pop up
                if (ZoneManager.instance.CurrentState() == ZoneState.OnPopUp)
                {
                    switch (WindowsController.popUp.id)
                    {
                        case "upgrade_failed":
                            if (WindowsController.popUp.GetClickedBtn() == PopUpBtnType.OK)
                            {
                                Destroy(WindowsController.popUp.gameObject);
                                ZoneManager.instance.ChangeCurrentState(ZoneState.Idle);
                            }
                            break;
                    }
                }

                currentColor.r = 1f;
                currentColor.g = 1f;
                currentColor.b = 1f;
                ImageComponent.color = currentColor;
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
        if (GameManager.player.CanBeUpgraded(type))
        {
            // aerus dan exp tidak cukup
            if (
                GameManager.player.aerus < GameManager.player.GetAerusUpCost(type) &&
                GameManager.player.exp < GameManager.player.GetExpUpCost(type)
            )
            {
                WindowsController.CreatePopUp(
                    "upgrade_failed",
                    PopUpType.OK,
                    "Anda membutuhkan lebih banyak Aereus dan Exp Orb untuk dapat meningkatkan stat ini."
                );

            }

            // aerus tidak cukup
            else if (GameManager.player.aerus < GameManager.player.GetAerusUpCost(type))
            {
                WindowsController.CreatePopUp(
                    "upgrade_failed",
                    PopUpType.OK,
                    "Anda membutuhkan lebih banyak Aereus untuk dapat meningkatkan stat ini."
                );
            }

            // exp tidak cukup
            else if (GameManager.player.exp < GameManager.player.GetExpUpCost(type))
            {
                WindowsController.CreatePopUp(
                    "upgrade_failed",
                    PopUpType.OK,
                    "Anda membutuhkan lebih banyak Exp Orb untuk dapat meningkatkan stat ini."
                );
            }

            // berhasil upgrade
            else
            {
                GameManager.player.Pay(CostType.Aerus, GameManager.player.GetAerusUpCost(type));
                GameManager.player.Pay(CostType.Exp, GameManager.player.GetExpUpCost(type));

                GameManager.player.IncreaseProgress(type, 1);

            }

        }
        else
        {
            // WindowsController.CreatePopUp(
            //     "upgrade_failed",
            //     PopUpType.OK,
            //     "Stat telah mencapai level maksimal."
            // );
        }
    }

    public override void ExclusiveKey()
    {
        throw new System.NotImplementedException();
    }

    public NavigationState CurrentState()
    {
        return currState;
    }

    public void ChangeCurrentState(NavigationState state)
    {
        this.currState = state;

    }


}