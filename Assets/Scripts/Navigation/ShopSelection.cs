using UnityEngine.UI;
using UnityEngine;

public enum Display
{
    display1, display2, display3
}

public class ShopSelection : Navigation
{
    public Display display;
    public Item obvirtu;
    public ShopManager shopManager;

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
        WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(false);
        WindowsController.isScrolling = false;
        shopManager.descriptionBox.GetComponent<Navigation>().Left = WindowsController.FocusedButton;
        WindowsController.HoveredButton = shopManager.GetComponent<ShopManager>().buyButton;
        WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(true);
    }
    public override void ExclusiveKey()
    {
        WindowsController.isScrolling = true;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            WindowsController.PlaySound(WindowsController.scrollButtonSound[0]);
            shopManager.GetComponent<ShopManager>().index -= 1;
            if (display == Display.display1)
            {
                shopManager.GetComponent<ShopManager>().scrollShop(false);
            }
            else
            {
                WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(false);
                WindowsController.HoveredButton = Up;
                WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            WindowsController.PlaySound(WindowsController.scrollButtonSound[0]);
            shopManager.index += 1;
            if (display == Display.display3)
            {
                shopManager.GetComponent<ShopManager>().scrollShop(true);
            }
            else
            {
                WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(false);
                WindowsController.HoveredButton = Down;
                WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            WindowsController.PlaySound(WindowsController.navigateButtonSound[7]);
            shopManager.closeBtn.GetComponent<Navigation>().Left = gameObject;
            WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(false);
            WindowsController.HoveredButton = Right;
            WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(true);
            WindowsController.isScrolling = false;

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            WindowsController.PlaySound(WindowsController.clickButtonSound[7]);
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
