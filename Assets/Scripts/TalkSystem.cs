using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkSystem : MonoBehaviour
{
    public GameObject pic;
    public GameObject dialogPanel;
    public Text dialogTeks;
    public Text nameTag;
    public string talkerName;
    public GameObject player;
    public NPC npc;
    public string selectedDialog;
    private List<string> dialog;
    public int index;
    public string windows;
    public GameObject windowsController;
    public float wordSpeed;
    public bool playerDekat;


    void Start()
    {
        if (npc != null)
        {
            pic.GetComponent<Image>().sprite = npc.Pict;
        }
        SetText(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerDekat && windowsController.GetComponent<windowsController>().ActiveWindowsID == -1)
        {
            ZoneManager.instance.ChangeCurrentState(ZoneState.OnDialog);

            nameTag.text = talkerName;
            player.GetComponent<PlayerController>().movementEnable(false);

            // kalau dialog panel sudah aktif
            if (dialogPanel.activeInHierarchy)
            {
                if (dialogTeks.text == dialog.First() && Input.GetKeyDown(KeyCode.Q))
                {
                    dialog.RemoveAt(0);
                    NextLine();
                }

            }

            // kalau belum, aktifkan dialog panel
            else
            {
                SetText(0);
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }


    }

    public void resetTeks()
    {
        dialogTeks.text = "";
        player.GetComponent<PlayerController>().movementEnable(true);
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialog.First())
        {
            dialogTeks.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (dialog.Count > 0)
        {
            dialogTeks.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            resetTeks();
            if (windows != string.Empty)
            {
                switch (windows)
                {
                    case "Skill":
                        windowsController.GetComponent<windowsController>().toogleWindow(1, true);
                        break;
                    case "Stat":
                        windowsController.GetComponent<windowsController>().toogleWindow(7, true);
                        break;

                    // NANTI GANTI !!!!
                    case "Stage":
                        SceneManager.LoadScene("Stage1");
                        break;

                    default:
                        break;
                }
                playerDekat = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().interactableNearby(false);
            playerDekat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().interactableNearby(false);
            playerDekat = false;

            // kalau nda diginiin error pas ganti scene
            if (dialogPanel != null)
            {
                dialogPanel.SetActive(false);
            }
            resetTeks();

        }
    }

    public void SetText(int index)
    {
        this.dialog = new List<string>();
        pic.GetComponent<Image>().sprite = npc.Pict;
        string new_dialog = npc.Dialogue[index];
        List<string> teks = new_dialog.Split("/plus/").ToList();
        switch (teks.Last())
        {
            case "debugOnly_teleportStage1":
                windows = "Stage";
                break;
            case "openSkillWindows":
                windows = "Skill";
                break;
            case "openStatWindows":
                windows = "Item";
                break;
        }
        teks.RemoveAt(teks.Count - 1);

        foreach (string barisTeks in teks)
        {
            this.dialog.Add(barisTeks);
        }
    }
}
