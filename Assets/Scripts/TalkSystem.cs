using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public Image pic;
    public GameObject dialogPanel;
    public Text dialogTeks;
    public Text nameTag;
    public string talkerName;
    public GameObject player;
    public string selectedDialog;

    private string[] dialog;

    private int index;

    public string windows;

    public GameObject windowsController;

    public float wordSpeed;
    public bool playerDekat;


    void Start()
    {
        SetText(selectedDialog);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerDekat && windowsController.GetComponent<windowsController>().ActiveWindowsID == -1)
        {
            nameTag.text = talkerName;
            player.GetComponent<PlayerController>().movementEnable(false);

            // kalau dialog panel sudah aktif
            if (dialogPanel.activeInHierarchy)
            {
                if (dialogTeks.text == dialog[index] && Input.GetKeyDown(KeyCode.Q))
                {
                    NextLine();
                }

            }

            // kalau belum, aktifkan dialog panel
            else
            {
                SetText(selectedDialog);
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }


    }

    public void resetTeks()
    {
        dialogTeks.text = "";
        index = 0;
        player.GetComponent<PlayerController>().movementEnable(true);
        // dialogPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialog[index].ToCharArray())
        {
            dialogTeks.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialog.Length - 1)
        {
            index++;
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
            if (!Object.ReferenceEquals(dialogPanel, null))
            {
                dialogPanel.SetActive(false);
            }
            resetTeks();

        }
    }

    public void SetText(string option)
    {
        string[] teks = null;

        switch (option)
        {
            case "zey_basic":
                teks = new string[1];
                teks[0] = "Mau buka skill windows?";
                windows = "Skill";
                break;

            case "rion_basic":
                // SILAHKAN DIGANTI NANTI !!!!!
                teks = new string[4];

                teks[0] = "Kemana tujuan kali ini";
                teks[1] = "Tapi sebelumnya...";
                teks[2] = "Fitur ini belum tersedia";

                teks[3] = "Boong uyy ~ Menuju Stage 1 !!!";
                windows = "Stage";
                break;

            default:
                teks = new string[1];
                teks[0] = "System Error";
                break;

        }

        dialog = teks;
    }

}
