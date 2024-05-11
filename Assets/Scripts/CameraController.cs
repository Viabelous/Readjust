using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameState gameState;
    public Transform target; // Objek target yang diikuti oleh kamera
    public Vector3 offset; // Jarak relatif dari kamera ke target

    [SerializeField] private Vector2 minMap, maxMap, minOffset, maxOffset;



    private void Start()
    {
        target = GameObject.Find("Player").transform;



    }

    void LateUpdate()
    {
        switch (gameState)
        {
            case GameState.OnStage:
                minMap = StageManager.instance.minMap + maxOffset;
                maxMap = StageManager.instance.maxMap + minOffset;
                break;
        }

        Vector3 pos = target.position + offset;

        pos.x = Mathf.Clamp(pos.x, minMap.x, maxMap.x);
        pos.y = Mathf.Clamp(pos.y, minMap.y, maxMap.x);

        transform.position = pos;
    }

}
