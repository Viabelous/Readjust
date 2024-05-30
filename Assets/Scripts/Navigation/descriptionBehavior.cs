using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Collections;
using System;

public class DescriptionBehavior : Navigation
{
    public GameObject textObject;
    public Sprite FocusSprite;
    [Header("Max Scroll Value")]
    [Tooltip("Hitunganya pakai baris mungkin(?)")] public int yVal;
    private float yBase;
    private float scrollValue = 0f;
    private bool isFocused;

    public override void IsHovered(bool state)
    {
        if(!isFocused)
        {
            if(state)
            {
                GetComponent<Image>().sprite = HoverSprite;
            } else
            {
                GetComponent<Image>().sprite = BasicSprite;
            }
        } else
        {
            GetComponent<Image>().sprite = FocusSprite;
        }
        

    }

    public override void Clicked()
    {
        if(isFocused == true)
        {
            scrollValue = 0f;
            scrolling();
            WindowsController.isScrolling = false;
            isFocused = false;
            IsHovered(false);
        }
        else
        {        
            WindowsController.isScrolling = true;
            isFocused = true;
            IsHovered(true);
        }
    }

    public override void ExclusiveKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(scrollValue > 0) scrollValue -= 1; 
            scrolling();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(scrollValue <= yVal) scrollValue += 1;
            scrolling();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Clicked();
        }
    }

    private void scrolling()
    {
        if(yBase == 0) yBase = textObject.GetComponent<RectTransform>().anchoredPosition.y;
        textObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(
            textObject.GetComponent<RectTransform>().anchoredPosition.x, 
            yBase + scrollValue * 20f,
            0
            );
    }

}
