using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{

    public Text score, time, aerus, extra_aerus, exp, extra_exp;

    public void SetScore(float score)
    {
        this.score.text = score.ToString();
    }
    public void SetEndTime(string time)
    {
        this.time.text = time;
    }
    public void SetEndAerus(float aerus, float extra)
    {
        this.aerus.text = aerus.ToString();
        if (extra != 0)
        {
            this.extra_aerus.text = "+" + extra.ToString();
        }
        else
        {
            this.extra_aerus.text = "";
        }
    }

    public void SetEndExp(float exp, float extra)
    {
        this.exp.text = exp.ToString();
        if (extra != 0)
        {
            this.extra_exp.text = "+" + extra.ToString();
        }
        else
        {
            this.extra_exp.text = "";
        }
    }

}