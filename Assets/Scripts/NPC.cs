using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Image pic;
    public GameObject dialogPanel;
    public Text dialogTeks;

    public GameObject Player;

    [HideInInspector]
    public string[] dialog;

    [HideInInspector]
    public int index;

    public float wordSpeed;
    public bool playerDekat;

    void Update(){
        string[] teks = new string[5];
        teks[0] = "wekwekwek";
        teks[1] = "halahmu";
        teks[2] = "coba mencoba";
        teks[3] = "apacoba";
        teks[4] = "bjirrr";
        rescript(teks);

        if (Input.GetKeyDown(KeyCode.Q) && playerDekat){
            Player.GetComponent<PlayerController>().movementEnable(false);
            if(dialogPanel.activeInHierarchy){
                if(dialogTeks.text == dialog[index] && Input.GetKeyDown(KeyCode.Q)){
                    NextLine();
                }

            }else{
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }

    public void resetTeks(){
        dialogTeks.text= "";
        index = 0;
        dialogPanel.SetActive(false);
        Player.GetComponent<PlayerController>().movementEnable(true);
    }

    IEnumerator Typing(){
        foreach(char letter in dialog[index].ToCharArray()){
            dialogTeks.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine(){
        if(index < dialog.Length - 1){
            index++;
            dialogTeks.text = "";
            StartCoroutine(Typing());
        }else{
            resetTeks();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Player.GetComponent<PlayerController>().interactableNearby(false);
            playerDekat = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Player.GetComponent<PlayerController>().interactableNearby(false);
            playerDekat = false;
            resetTeks();
        }
    }

    public void rescript(string[] teks){
        dialog = teks;
    }

}
