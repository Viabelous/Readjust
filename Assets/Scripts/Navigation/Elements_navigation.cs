using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Collections;
using System;

public class Elements : Navigation
{
    public Sprite LockedSprite;
    public bool isUnlocked;
    public bool firstAccess;
    
    public int elementNumber;

    public GameObject SkillsSelectionWindows;

    void Start()
    {
        if(isUnlocked)
        {        
            GetComponent<Image>().sprite = BasicSprite;
        } else
        {
            GetComponent<Image>().sprite = LockedSprite;
            Color current = ImageComponent.color;
            current.a = 0.5f;
            ImageComponent.color = current;
        }
        if(firstAccess)
        {
            GetComponent<Image>().sprite = HoverSprite;
        }
    }

    public override void IsHovered(bool state)
    {
        if(isUnlocked == true)
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
            if(state)
            {
                GetComponent<Image>().sprite = LockedSprite;
                Color current = ImageComponent.color;
                current.a = 1f;
                ImageComponent.color = current;
            } else
            {
                GetComponent<Image>().sprite = LockedSprite;
                Color current = ImageComponent.color;
                current.a = 0.5f;
                ImageComponent.color = current;
            }            
        }

    }

    public override void Clicked()
    {
        if(isUnlocked)
        {
            StartCoroutine(WindowsController.TransitionWindows(1, elementNumber+1));
            SkillsSelectionWindows.SetActive(true);
        }
    }

    public override void ExclusiveKey()
    {

    }

}
