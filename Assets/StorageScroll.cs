using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StorageScroll : Navigation
{
    [SerializeField] GameObject[] display;
    [SerializeField] Item[] listOfObvirtu;

    public override void IsHovered(bool state)
    {
        if (state)
        {
            GetComponent<Image>().sprite = HoverSprite;
            WindowsController.FocusedButton = gameObject;
            WindowsController.GetComponent<windowsController>().isScrolling = true;
            print(GameManager.unlockedItems[0]);
            print(GameManager.unlockedItems[1]);
        }
        else
        {
            GetComponent<Image>().sprite = BasicSprite;
            print(GameManager.unlockedItems.Count);
        }
    }

    public override void Clicked()
    {
        /*
        WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(false);
        WindowsController.isScrolling = false;
        shopManager.GetComponent<ShopManager>().descriptionBox.GetComponent<Navigation>().Left = WindowsController.FocusedButton;
        WindowsController.HoveredButton = shopManager.GetComponent<ShopManager>().buyButton;
        WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(true);
        */
    }
    public override void ExclusiveKey()
    {
        WindowsController.isScrolling = true;

        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            shopManager.GetComponent<ShopManager>().index -= 1;
            if (display == Display.display1)
            {
                shopManager.GetComponent<ShopManager>().scrollShop(false);
            }
            else
            {
                WindowsController.GetComponent<windowsController>().HoveredButton.GetComponent<Navigation>().IsHovered(false);
                WindowsController.GetComponent<windowsController>().HoveredButton = Up;
                WindowsController.GetComponent<windowsController>().HoveredButton.GetComponent<Navigation>().IsHovered(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            shopManager.GetComponent<ShopManager>().index += 1;
            if (display == Display.display3)
            {
                shopManager.GetComponent<ShopManager>().scrollShop(true);
            }
            else
            {
                WindowsController.GetComponent<windowsController>().HoveredButton.GetComponent<Navigation>().IsHovered(false);
                WindowsController.GetComponent<windowsController>().HoveredButton = Down;
                WindowsController.GetComponent<windowsController>().HoveredButton.GetComponent<Navigation>().IsHovered(true);
            }
        }*/

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(false);
            WindowsController.HoveredButton.GetComponent<Navigation>().Right.GetComponent<Navigation>().Left = gameObject;
            WindowsController.HoveredButton = Right;
            WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(true);
            WindowsController.isScrolling = false;

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Clicked();
        }
        
    }

}
