﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkSystem : MonoBehaviour
{
    public GameObject pic;
    public GameObject dialogPanel;
    public Text dialogTeks;
    public Text nameTag;
    public GameObject player;
    public NPC npc;
    private List<string> dialog;
    private string windows;
    public windowsController WindowsController;
    public float wordSpeed;
    [HideInInspector] public bool playerDekat;
    private bool stopTyping;
    private bool isTypingNow;


    void Start()
    {
        if (npc != null)
        {
            pic.GetComponent<Image>().sprite = npc.Pict;
        }
        SetText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerDekat && WindowsController.ActiveWindowsID == -1)
        {
            ZoneManager.instance.ChangeCurrentState(ZoneState.OnDialog);

            player.GetComponent<PlayerController>().movementEnable(false);

            // kalau dialog panel sudah aktif
            if (dialogPanel.activeInHierarchy)
            {
                if (dialogTeks.text == dialog.First() && Input.GetKeyDown(KeyCode.Q))
                {
                    isTypingNow = false;
                    dialog.RemoveAt(0);
                    NextLine();
                }

            }
            // kalau belum, aktifkan dialog panel
            else
            {
                player.GetComponent<Animator>().SetFloat("Speed", 0);
                player.GetComponent<Animator>().SetFloat("Face", 2);
                isTypingNow = false;
                SetText();
                dialogPanel.SetActive(true);
                NextLine();
            }
        }
        if (isTypingNow == true && Input.GetKeyDown(KeyCode.Q))
        {
            stopTyping = true;
            isTypingNow = false;
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
        stopTyping = false;
        yield return new WaitForSeconds(0.2f);
        isTypingNow = true;
        foreach (char letter in dialog.First())
        {
            if (!stopTyping)
            {
                dialogTeks.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }

            else
            {
                dialogTeks.text = dialog.First();
                yield break;
            }
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
            if (GameManager.firstEncounter[npc.Name]) GameManager.firstEncounter[npc.Name] = false;
            resetTeks();
            if (windows != string.Empty)
            {
                switch (windows)
                {
                    case "Stage":
                        StartCoroutine(WindowsController.ToogleWindow(0, true));
                        break;
                    case "Skill":
                        StartCoroutine(WindowsController.ToogleWindow(1, true));
                        break;
                    case "Stat":
                        StartCoroutine(WindowsController.ToogleWindow(6, true));
                        break;
                    case "Shop":
                        WindowsController.openShop();
                        break;
                    case "Story0":
                        GameManager.selectedMap = Map.Stage1;
                        WindowsController.levelChanger.Transition("Stage1");
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

    public void SetText()
    {
        this.dialog = new List<string>();
        pic.GetComponent<Image>().sprite = npc.Pict;
        int indexChoosen = GameManager.firstEncounter[npc.Name] ? 0
                                                                : UnityEngine.Random.Range(1, npc.Dialogue.Length);
        string new_dialog = npc.Dialogue[indexChoosen];
        List<string> teks = new_dialog.Split("/plus/").ToList();
        switch (teks.Last())
        {
            case "openStageWindows":
                windows = "Stage";
                break;
            case "openSkillWindows":
                windows = "Skill";
                break;
            case "openStatWindows":
                windows = "Stat";
                break;
            case "openShopWindows":
                windows = "Shop";
                break;
            case "doStory0":
                windows = "Story0";
                break;
        }
        teks.RemoveAt(teks.Count - 1);

        foreach (string barisTeks in teks)
        {
            this.dialog.Add(barisTeks);
        }

        nameTag.text = npc.Name;
    }
}
