using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageDescription : MonoBehaviour
{
    [SerializeField] MapProperty map;
    [SerializeField] TextMeshProUGUI typeText, nameText, descText, scoreText, timeText, statusText;
    [SerializeField] Image preview;

    public MapProperty GetFocusedMap()
    {
        return map;
    }

    void Start()
    {
        typeText.text = "Stage " + map.map.ToString()[map.map.ToString().Length - 1];
        nameText.text = map.name;
        descText.text = map.description;
        descText.text = map.description;

        Score highScore = Score.GetHighScoreByMap(map.map);
        scoreText.text = highScore != null ? highScore.GetScore().ToString() : "-";

        Score bestTime = Score.GetBestTimeByMap(map.map);
        if (bestTime != null)
        {

            float min = Mathf.FloorToInt(bestTime.GetTime() / 60f);
            float sec = Mathf.FloorToInt(bestTime.GetTime() % 60f);
            timeText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        else
        {

            timeText.text = "-";
        }

        preview.sprite = map.preview;
        statusText.text = GameManager.player.GetProgress(Player.Progress.Story) > map.unlockedProgress ?
            "Cleared" : "Not Yet Cleared";

    }
}
