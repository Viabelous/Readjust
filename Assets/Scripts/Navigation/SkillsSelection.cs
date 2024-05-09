using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public enum NavigationState
{
    Active,
    Hover,
    Selected
}

public class SkillsSelection : Navigation
{
    // public Skill skill;
    public GameObject prefab;
    private NavigationState currState;
    private Color current;

    void Start()
    {
        currState = NavigationState.Active;
        current = ImageComponent.color;

    }

    void Update()
    {
        // Color current = ImageComponent.color;

        switch (currState)
        {
            case NavigationState.Active:
                current.r = 0.5f;
                current.g = 0.5f;
                current.b = 0.5f;
                ImageComponent.color = current;
                break;

            case NavigationState.Hover:
                // Color current = ImageComponent.color;
                current.r = 1f;
                current.g = 1f;
                current.b = 1f;
                ImageComponent.color = current;
                break;

            case NavigationState.Selected:

                // current.r = 120f;
                // current.g = 255;
                // current.b = 120f;
                ImageComponent.color = Color.green;
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
            currState = IsSelected() ? NavigationState.Selected : NavigationState.Active;
        }
    }

    private bool IsSelected()
    {
        return GameManager.selectedSkills.Contains(prefab);
    }

    public override void Clicked()
    {
        // jika sebelumnya sudah diselect
        if (IsSelected())
        {
            GameManager.selectedSkills.Remove(prefab);
        }
        // jika belum pernah diselect
        else
        {
            // kalau slot belum penuh
            if (GameManager.selectedSkills.Count < 7)
            {
                GameManager.selectedSkills.Add(prefab);
                currState = NavigationState.Selected;
            }
        }
    }
    public override void ExclusiveKey()
    {

    }
}
