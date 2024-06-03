using UnityEngine;
using UnityEngine.UI;

public class StatWindowsController : MonoBehaviour
{

    [SerializeField] private windowsController WindowsController;
    [SerializeField] private GameObject upgradeBtn;
    [SerializeField] private Text statName, statDescription, statAerusCost, statExpCost;
    [SerializeField] private Text[] statsText = new Text[6];
    StatSelection focusedStat;

    void Start()
    {
        WindowsController.FocusedButton = null;

        UpdateStatBtnHover();
        UpdateStatValue();
        SetStatDescription();
    }

    void Update()
    {
        if (WindowsController.HoveredButton.GetComponent<StatSelection>() != null)
        {
            UpdateStatBtnHover();
            SetStatDescription();
            UpdateStatValue();
        }
    }

    void SetStatDescription()
    {
        int level = GameManager.player.GetProgress(focusedStat.type);

        if (GameManager.player.CanBeUpgraded(focusedStat.type))
        {
            statAerusCost.text = GameManager.player.GetAerusUpCost(focusedStat.type).ToString();
            statExpCost.text = GameManager.player.GetExpUpCost(focusedStat.type).ToString();

        }
        else
        {
            statAerusCost.text = "-";
            statExpCost.text = "-";
        }


        switch (focusedStat.type)
        {
            case Player.Progress.MaxHP:
                statName.text = "Max Health Point (HP) Lv. " + level;
                statDescription.text = "Stat yang menjadi nilai dasar darah karakter. Saat HP habis, karakter akan Game Over.";
                break;
            case Player.Progress.MaxMana:
                statName.text = "Max Mana Point (MP) Lv. " + level;
                statDescription.text = "Stat yang menjadi batasan dalam penggunaan skill. Dalam penggunaan skill diperlukan Mana kecuali skill tertentu, skill tidak dapat digunakan ketika Mana tidak mencukupi.";
                break;
            case Player.Progress.ATK:
                statName.text = "Attack (ATK) Lv. " + level;
                statDescription.text = "Stat yang menentukan basis kekuatan dari karakter, damage dari basic attack dan sebagian besar skill dipengaruhi oleh stat ini.";
                break;
            case Player.Progress.DEF:
                statName.text = "Defend (DEF) Lv. " + level;
                statDescription.text = "Stat yang menentukan ketahanan karakter. Saat menerima damage dari musuh, damage diterima akan berkurang tergantung dari stat ini.";
                break;
            case Player.Progress.FOC:
                statName.text = "Focus (FOC) Lv. " + level;
                statDescription.text = "Stat yang menentukan damage akhir dari tiap serangan karakter. Semakin besar tingkat FOCUS karakter, semakin mungkin karakter mengakibatkan serangan ampuh yang senilai dengan 250% damage asli.";
                break;
            case Player.Progress.AGI:
                statName.text = "Agility (AGI) Lv. " + level;
                statDescription.text = "Stat yang menentukan kecepatan berjalan karakter. Dibutuhkan kecepatan berjalan yang lebih tinggi untuk menandingi kecepatan musuh yang tinggi.";
                break;
        }
    }

    void UpdateStatBtnHover()
    {
        focusedStat = WindowsController.HoveredButton.GetComponent<StatSelection>();
    }

    public void UpdateStatValue()
    {
        statsText[0].text = GameManager.player.GetMaxHP().ToString();
        statsText[1].text = GameManager.player.GetMaxMana().ToString();
        statsText[2].text = GameManager.player.GetATK().ToString();
        statsText[3].text = GameManager.player.GetDEF().ToString();
        statsText[4].text = GameManager.player.GetFOC().ToString();
        statsText[5].text = GameManager.player.GetAGI().ToString();
    }

}