using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storageBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject windowsController;
    [HideInInspector] public bool playerDekat;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerDekat && windowsController.GetComponent<windowsController>().ActiveWindowsID == -1)
        {
            ZoneManager.instance.ChangeCurrentState(ZoneState.OnWindow);
            player.GetComponent<PlayerController>().movementEnable(false);
            StartCoroutine(windowsController.GetComponent<windowsController>().ToogleWindow(8, true));
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
        }
    }
}
