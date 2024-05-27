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

                currentColor.r = 1f;
                currentColor.g = 1f;
                currentColor.b = 1f;
                ImageComponent.color = currentColor;
                break;

            case NavigationState.Focused:
                currentColor.r = 0.3f;
                currentColor.g = 0.3f;
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
        print("klik stat");
        // if (GameManager.player.CanBeUpgraded(type))
        // {
        //     print("sekarang hover ada di upgrade btn");
        //     currState = NavigationState.Focused;
        //     WindowsController.HoveredButton = GameObject.Find("upgrade_btn");
        // }
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