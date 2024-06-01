using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using System;

public enum Display
{
    display1, display2, display3
}

public class ShopSelection : Navigation
{
    public Display display;
    public Item obvirtu;
    public GameObject shopManager;

    [SerializeField] private Text nama;
    [SerializeField] private Text harga;
    [SerializeField] private GameObject icon;

    void Update()
    {
        refreshObvirtu();
    }

    public override void IsHovered(bool state)
    {
        if (state)
        {
            GetComponent<Image>().sprite = HoverSprite;
            WindowsController.FocusedButton = gameObject;
            WindowsController.GetComponent<windowsController>().isScrolling = true;
        }
        else
        {
            GetComponent<Image>().sprite = BasicSprite;
        }
    }

    public override void Clicked()
    {
        WindowsController.GetComponent<windowsController>().HoveredButton.GetComponent<Navigation>().IsHovered(false);
        //shopManager.GetComponent<ShopManager>().buyButton.left = WindowsController.GetComponent<windowsController>().HoveredButton;
        WindowsController.GetComponent<windowsController>().isScrolling = false;
        WindowsController.GetComponent<windowsController>().HoveredButton = shopManager.GetComponent<ShopManager>().buyButton;
        WindowsController.GetComponent<windowsController>().HoveredButton.GetComponent<Navigation>().IsHovered(true);
    }
    public override void ExclusiveKey()
    {
        WindowsController.isScrolling = true;

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
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Clicked();
        }
    }

    public void refreshObvirtu()
    {
        nama.text = obvirtu.name;
        harga.text = obvirtu.Price.ToString();
        icon.GetComponent<Image>().sprite = obvirtu.Icon;
    }

}
