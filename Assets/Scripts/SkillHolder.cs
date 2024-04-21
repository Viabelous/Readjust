using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillState
{
    ready,
    active,
    cooldown
}

public class SkillHolder : MonoBehaviour
{

    public static SkillHolder Instance;

    public List<GameObject> skillPrefs = new List<GameObject>();
    // public List<Sprite> skillImgs = new List<Sprite>();
    // public List<SkillState> skillStates = new List<SkillState>() {
    //     SkillState.ready,
    //     SkillState.ready,
    //     SkillState.ready,
    //     SkillState.ready,
    //     SkillState.ready,
    //     SkillState.ready,
    //     SkillState.ready,
    // };

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
