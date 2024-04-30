using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Image pic;
    public GameObject dialogPanel;
    public Text dialogTeks;

    public GameObject player;
    public string selectedDialog;

    private string[] dialog;

    private int index;

    public string windows;

    public GameObject windows_controller;

    public float wordSpeed;
    public bool playerDekat;
    

    void Start(){
        SetText(selectedDialog);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Q) && playerDekat && windows_controller.GetComponent<windowsController>().ActiveWindowsID == -1){
            player.GetComponent<PlayerController>().movementEnable(false);
            if(dialogPanel.activeInHierarchy){
                if(dialogTeks.text == dialog[index] && Input.GetKeyDown(KeyCode.Q)){
                    NextLine();
                }

            }else{
                SetText(selectedDialog);
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }

    public void resetTeks(){
        dialogTeks.text= "";
        index = 0;
        player.GetComponent<PlayerController>().movementEnable(true);
        dialogPanel.SetActive(false);
    }

    IEnumerator Typing(){
        foreach(char letter in dialog[index].ToCharArray()){
            dialogTeks.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine(){
        if(index < dialog.Length - 1)
        {
            index++;
            dialogTeks.text = "";
            StartCoroutine(Typing());
        }else
        {
            resetTeks();
            if(windows != string.Empty)
            {
                switch(windows)
                {
                    case "Skill":
                        windows_controller.GetComponent<windowsController>().toogleWindow(1, true);
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
        if(other.CompareTag("Player")){
            player.GetComponent<PlayerController>().interactableNearby(false);
            playerDekat = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            player.GetComponent<PlayerController>().interactableNearby(false);
            playerDekat = false;
            resetTeks();
        }
    }

    public void SetText(string option)
    {
        string[] teks = null;

        switch(option){
            case "zey_basic":
                teks = new string[1];
                teks[0] = "Mau buka skill windows?";
                windows = "Skill";
                break;

            case "rion_basic":
                teks = new string[3];
                teks[0] = "tch";
                teks[1] = "nandayo koitse";
                teks[2] = "canda";
                break;

            default:
                teks = new string[1];
                teks[0] = "System Error";
                break;
            
        }

        dialog = teks;
    }

}
