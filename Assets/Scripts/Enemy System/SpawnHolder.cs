using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHolder : MonoBehaviour
{
    public static SpawnHolder instance;
    public List<GameObject> enemyPrefs = new List<GameObject>();

    public GameObject bossPref;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
