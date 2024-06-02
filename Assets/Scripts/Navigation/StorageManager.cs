using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StorageManager : Navigation
{
    
    public Text obvirtuName;
    public Text descriptionText;
    public Image iconFocus;
    [SerializeField] Image[] displayImage;
    [SerializeField] Item[] listOfObvirtu;
    [HideInInspector] List<Item> unlockedItemList = new List<Item>{};
    [HideInInspector] int index = 0;
    [HideInInspector] public Item focusedObvirtu;


    public void Start()
    {
        foreach(string obvirtuName in GameManager.unlockedItems)
        {
            unlockedItemList.Add(listOfObvirtu.Where(obj => obj.name == obvirtuName).SingleOrDefault());
        }

        if(unlockedItemList.Count >= 0)
        {
            focusedObvirtu = unlockedItemList[0];
            refreshObvirtu();
        } else
        {
            foreach(Image img in displayImage)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
            }

            iconFocus.color = new Color(iconFocus.color.a, iconFocus.color.g, iconFocus.color.b,0);
        }
        
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
        WindowsController.HoveredButton.GetComponent<Navigation>().Right.GetComponent<Navigation>().Left = gameObject;
        WindowsController.HoveredButton = Right;
        WindowsController.HoveredButton.GetComponent<Navigation>().IsHovered(true);
        WindowsController.isScrolling = false;
    }
    public override void ExclusiveKey()
    {
        WindowsController.isScrolling = true;

        if (Input.GetKeyDown(KeyCode.UpArrow) && unlockedItemList.Count >= 0)
        {
            index -= 1;
            index = CalculateIndex(index);
            focusedObvirtu = unlockedItemList[index];
            refreshObvirtu();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && unlockedItemList.Count >= 0)
        {
            index += 1;
            index = CalculateIndex(index);
            focusedObvirtu = unlockedItemList[index];
            refreshObvirtu();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Clicked();
        }
        
    }

    void refreshObvirtu()
    {
        obvirtuName.text = focusedObvirtu.name;
        iconFocus.GetComponent<Image>().sprite = focusedObvirtu.Icon;
        descriptionText.text = focusedObvirtu.Description;
        ChangeDisplaySprite();
    }

    private int CalculateIndex(int num)
    {
        if(num < 0)
        {
            num += unlockedItemList.Count;
            return CalculateIndex(num);
        } else if(num >= unlockedItemList.Count)
        {
            num -= unlockedItemList.Count;
            return CalculateIndex(num);
        }
        return num;
    }

    private void ChangeDisplaySprite()
    {
        displayImage[0].sprite = unlockedItemList[CalculateIndex(index - 2)].Icon;
        displayImage[1].sprite = unlockedItemList[CalculateIndex(index - 1)].Icon;
        displayImage[2].sprite = unlockedItemList[CalculateIndex(index)].Icon;
        displayImage[3].sprite = unlockedItemList[CalculateIndex(index + 1)].Icon;
        displayImage[4].sprite = unlockedItemList[CalculateIndex(index + 2)].Icon;
    }

}

//  "Red Shoes",
//  "First Chapter Script",
//  "End of May Scroll",
//  "Tea Leaves"