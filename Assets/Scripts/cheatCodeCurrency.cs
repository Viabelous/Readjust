using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheatCodeCurrency : MonoBehaviour
{
    [HideInInspector] PlayerController thisPlayer;

    private KeyCode[] sequence = new KeyCode[]{
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A};
    private int sequenceIndex;

    void Start()
    {
        thisPlayer = GetComponent<PlayerController>();
    }

    void Update()
    {
    if (Input.GetKeyDown(sequence[sequenceIndex])) {
        if (++sequenceIndex == sequence.Length){
             sequenceIndex = 0;
            
            thisPlayer.player.aerus += 20000;
            thisPlayer.player.exp += 20000;

         }
    } else if (Input.anyKeyDown) sequenceIndex = 0;
    }
}
