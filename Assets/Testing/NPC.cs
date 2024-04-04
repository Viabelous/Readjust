using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Image pic;
    public GameObject dialogPanel;
    public GameObject nextButton;
    public Text dialogTeks;
    public string[] dialog;
    private int index;

    public float wordSpeed;
    public bool playerDekat;

    void Update(){
        if (Input.GetKeyDown(KeyCode.E) && playerDekat){
            Debug.Log("karakter berada di dekat NPC");
            if(dialogPanel.activeInHierarchy){
                resetTeks();

            }else{
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if(dialogTeks.text == dialog[index]){
            nextButton.SetActive(true);
        }
    }

    public void resetTeks(){
        dialogTeks.text= "";
        index = 0;
        dialogPanel.SetActive(false);
    }

    IEnumerator Typing(){
        foreach(char letter in dialog[index].ToCharArray()){
            dialogTeks.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine(){

        nextButton.SetActive(false);
        
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
            playerDekat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerDekat = false;
            resetTeks();
        }
    }
}
