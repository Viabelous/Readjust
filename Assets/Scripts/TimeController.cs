using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{

    public Text timeText;
    private int min, sec;
    private float timeInSec;
    // Start is called before the first frame update
    void Start()
    {
        timeInSec = StageManager.Instance.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeInSec = Time.time;

        min = Mathf.FloorToInt(timeInSec / 60f);
        sec = Mathf.FloorToInt(timeInSec % 60f);

        timeText.text = string.Format("{0:00}:{1:00}", min, sec);

        if (min == 10)
        {
            StageManager.Instance.gameState = GameState.Boss;
        }
    }
}
