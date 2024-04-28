using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillHolder : MonoBehaviour
{

    public static SkillHolder Instance;

    public List<GameObject> skillPrefs = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
