using UnityEngine.UI;
public class CloseButton : Navigation
{
    public int windows_id;

    public override void IsHovered(bool state)
    {
        if (state)
        {
            GetComponent<Image>().sprite = HoverSprite;
        }
        else
        {
            GetComponent<Image>().sprite = BasicSprite;
        }
    }
    public override void Clicked()
    {

        if (windows_id >= 2 && windows_id <= 5)
        {
            StartCoroutine(WindowsController.TransitionWindows(2, 1));
            WindowsController.CloseSkillTree();
        }

        else
        {
            ZoneManager.instance.ChangeCurrentState(ZoneState.Idle);
            StartCoroutine(WindowsController.ToogleWindow(windows_id, false));
        }

    }
    public override void ExclusiveKey()
    {

    }
}
