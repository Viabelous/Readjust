using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatWindowsBtnSelection : Navigation
{
    [SerializeField] private NavigationState currState;
    private Color currentColor;
    private StatSelection focusedStat;

    void Start()
    {
        currentColor = ImageComponent.color;
    }

    void Update()
    {
        switch (currState)
        {
            case NavigationState.Active:
                if (WindowsController.HoveredButton == gameObject)
                {
                    currState = NavigationState.Hover;
                }

                currentColor.r = 0.5f;
                currentColor.g = 0.5f;
                currentColor.b = 0.5f;

                ImageComponent.color = currentColor;
                break;

            case NavigationState.Hover:
                Right = WindowsController.FocusedButton;
                focusedStat = Right.GetComponent<StatSelection>();

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
        if (GameManager.player.exp < GameManager.player.GetExpUpCost(focusedStat.type))
        {
            WindowsController.CreatePopUp(
                "upgrade_failed",
                PopUpType.OK,
                "Anda membutuhkan lebih banyak Exp Orb untuk dapat meningkatkan stat ini."
            );
        }
        else if (GameManager.player.aerus < GameManager.player.GetAerusUpCost(focusedStat.type))
        {
            WindowsController.CreatePopUp(
                "upgrade_failed",
                PopUpType.OK,
                "Anda membutuhkan lebih banyak Aerus untuk dapat meningkatkan stat ini."
            );
        }

        else if (
            GameManager.player.aerus < GameManager.player.GetAerusUpCost(focusedStat.type) &&
            GameManager.player.exp < GameManager.player.GetExpUpCost(focusedStat.type)

        )
        {
            WindowsController.CreatePopUp(
                "upgrade_failed",
                PopUpType.OK,
                "Anda membutuhkan lebih banyak Aerus dan Exp Orb untuk dapat meningkatkan stat ini."
            );
        }

        else
        {
            GameManager.player.IncreaseProgress(focusedStat.type, 1);
            if (GameManager.player.GetProgress(focusedStat.type) == GameManager.player.StatMaxLevel)
            {
                HoverBackToStat();
                gameObject.SetActive(false);
            }

            GameManager.player.Pay(CostType.Aerus, GameManager.player.GetAerusUpCost(focusedStat.type));
            GameManager.player.Pay(CostType.Exp, GameManager.player.GetExpUpCost(focusedStat.type));

        }
        // break;
    }

    public override void ExclusiveKey()
    {
        throw new System.NotImplementedException();
    }

    private void HoverBackToStat()
    {
        focusedStat.ChangeCurrentState(NavigationState.Hover);
        WindowsController.HoveredButton = Left;
        WindowsController.FocusedButton = null;
        currState = NavigationState.Active;
    }
}
