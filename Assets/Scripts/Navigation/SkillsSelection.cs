using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public enum NavigationState
{
    Active,
    Hover,
    Focused,
    Selected,
    Locked
}

public class SkillsSelection : Navigation
{
    // public Skill skill;
    [SerializeField] private GameObject prefab;
    private Skill skill;
    [SerializeField] private NavigationState currState;
    private Color currentColor;
    [HideInInspector] public bool hasUnlocked;

    void Start()
    {
        skill = prefab.GetComponent<SkillController>().skill.Clone();

        // !!!! NANTI HAPUS !!!!
        if (skill.Name == "Will of Fire" || skill.Name == "Sacrivert" && GameManager.unlockedSkills.Count < 2)
        {
            GameManager.unlockedSkills.Add(skill);
        }

        hasUnlocked = GameManager.CheckUnlockedSkill(skill.Name);
        if (hasUnlocked)
        {
            skill = GameManager.GetUnlockedSkill(skill.Name);
        }
        currentColor = ImageComponent.color;
    }

    void Update()
    {
        hasUnlocked = GameManager.CheckUnlockedSkill(skill.Name);
        switch (currState)
        {
            case NavigationState.Active:
                currentColor.r = 0.5f;
                currentColor.g = 0.5f;
                currentColor.b = 0.5f;
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

            case NavigationState.Selected:
                currentColor.r = 0.5f;
                currentColor.g = 1f;
                currentColor.b = 0.5f;
                ImageComponent.color = currentColor;
                break;

                // case NavigationState.Locked:
                //     currentColor.a = 0.75f;
                //     ImageComponent.color = currentColor;
                //     break;
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
            currState = HasSelected() ? NavigationState.Selected : NavigationState.Active;
        }
    }
    public override void Clicked()
    {
        currState = NavigationState.Focused;
        WindowsController.HoveredButton = GameObject.Find(hasUnlocked ? "select_btn" : "locked_btn");

        // // jika sebelumnya sudah diselect
        // if (IsSelected())
        // {
        //     GameManager.selectedSkills.Remove(prefab);
        // }
        // // jika belum pernah diselect
        // else
        // {
        //     // kalau slot belum penuh
        //     if (GameManager.selectedSkills.Count < 7)
        //     {
        //         GameManager.selectedSkills.Add(prefab);
        //         currState = NavigationState.Selected;
        //     }
        // }
    }
    public override void ExclusiveKey()
    {

    }

    public bool HasSelected()
    {
        return GameManager.selectedSkills.Contains(prefab);
    }

    public NavigationState CurrentState()
    {
        return currState;
    }

    public void ChangeCurrentState(NavigationState state)
    {
        this.currState = state;
    }

    public Skill GetSkill()
    {
        return skill;
    }

    public void Select()
    {
        // kalau slot belum penuh
        if (GameManager.selectedSkills.Count < 7)
        {
            GameManager.selectedSkills.Add(prefab);
            currState = NavigationState.Selected;
        }
    }
    public void Unselected()
    {
        GameManager.selectedSkills.Remove(prefab);
    }

    public void Upgrade()
    {
        skill.UpgradeLevel();
    }
}
