using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] windowsController WindowsController;
    [SerializeField] Item[] listObvirtu;
    [SerializeField] GameObject[] display;
    public GameObject closeBtn;

    public GameObject obvirtuName;
    public GameObject pic;
    public GameObject descriptionText;
    public GameObject buyButton;
    public Text price;
    [HideInInspector] public int index = 0;
    ShopSelection obvirtuFocused;


    void Update()
    {
        // kalau yg hover adalah itemnya
        if (WindowsController.HoveredButton.GetComponent<ShopSelection>() != null)
        {
            obvirtuFocused = WindowsController.HoveredButton.GetComponent<ShopSelection>();
            obvirtuName.GetComponent<Text>().text = obvirtuFocused.obvirtu.Name;
            pic.GetComponent<Image>().sprite = obvirtuFocused.obvirtu.Icon;
            descriptionText.GetComponent<Text>().text = obvirtuFocused.obvirtu.Description;
            price.text = obvirtuFocused.obvirtu.Price.ToString();
        }
    }

    public void scrollShop(bool scrollDown)
    {
        print("test: " + index);
        if (index >= listObvirtu.Length)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = listObvirtu.Length - 1;
        }

        if (scrollDown)
        {
            display[0].GetComponent<ShopSelection>().obvirtu = listObvirtu[(index - 2 < 0) ? listObvirtu.Length - index - 2 : index - 2];
            display[1].GetComponent<ShopSelection>().obvirtu = listObvirtu[(index - 1 < 0) ? listObvirtu.Length - index - 1 : index - 1];
            display[2].GetComponent<ShopSelection>().obvirtu = listObvirtu[index];
        }
        else
        {
            display[0].GetComponent<ShopSelection>().obvirtu = listObvirtu[index];
            display[1].GetComponent<ShopSelection>().obvirtu = listObvirtu[(index + 1 >= listObvirtu.Length) ? index + 1 - listObvirtu.Length : index + 1];
            display[2].GetComponent<ShopSelection>().obvirtu = listObvirtu[(index + 2 >= listObvirtu.Length) ? index + 2 - listObvirtu.Length : index + 2];
        }

        foreach (GameObject dis in display)
        {
            dis.GetComponent<ShopSelection>().refreshObvirtu();
        }
    }
}