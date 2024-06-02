using UnityEngine;



public class cheatCodeTime : MonoBehaviour
{
    public GameObject stageManager;
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
    public GameObject[] spawner;

    void Update()
    {
        if (Input.GetKeyDown(sequence[sequenceIndex]))
        {
            if (++sequenceIndex == sequence.Length)
            {
                sequenceIndex = 0;

                float time = stageManager.GetComponent<StageManager>().time;
                stageManager.GetComponent<StageManager>().time =
                60 + (time - (time % 60));
                foreach (GameObject spawn in spawner)
                {
                    spawn.GetComponent<EnemySpawner>().UpdateProbabilities();
                }


            }
        }
        else if (Input.anyKeyDown) sequenceIndex = 0;
    }

}
