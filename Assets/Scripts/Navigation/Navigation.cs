using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public abstract class Navigation : MonoBehaviour
{
    public Image ImageComponent;
    public windowsController WindowsController;
    public Sprite BasicSprite;
    public Sprite HoverSprite;
    public GameObject Left;
    public GameObject Up;
    public GameObject Right;
    public GameObject Down;

    public abstract void IsHovered(bool state);
    public abstract void Clicked();
    public abstract void ExclusiveKey();
}
