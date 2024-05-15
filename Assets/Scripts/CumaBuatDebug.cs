using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CumaBuatDebug : MonoBehaviour
{

    public static CumaBuatDebug instance;

    // public List<GameObject> selectedSkills = new List<GameObject>();
    public List<Item> selectedItems = new List<Item>();

    void Awake()
    {
        instance = this;
    }

    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
