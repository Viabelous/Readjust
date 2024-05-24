using UnityEngine;
using System.IO;

public class PlayerData
{
    public float aerus;
    public float exp;
    public float venetia;

    // stat level
    public int maxHPLevel = 0;
    public int maxManaLevel = 0;
    public int atkLevel = 0;
    public int defLevel = 0;
    public int agiLevel = 0;
    public int focLevel = 0;

    // progress
    public int story = 0;
    public int fireSkill = 0;
    public int earthSkill = 0;
    public int waterSkill = 0;
    public int airSkill = 0;

    public PlayerData(Player player)
    {
        this.aerus = player.aerus;
        this.exp = player.exp;
        this.venetia = player.venetia;

        this.maxHPLevel = player.GetProgress(Player.Progress.MaxHP); ;
        this.maxManaLevel = player.GetProgress(Player.Progress.MaxMana);
        this.atkLevel = player.GetProgress(Player.Progress.ATK);
        this.defLevel = player.GetProgress(Player.Progress.DEF);
        this.agiLevel = player.GetProgress(Player.Progress.AGI);
        this.focLevel = player.GetProgress(Player.Progress.FOC);

        this.story = player.GetProgress(Player.Progress.Story);
        this.fireSkill = player.GetProgress(Player.Progress.FireSkill);
        this.earthSkill = player.GetProgress(Player.Progress.EarthSkill);
        this.waterSkill = player.GetProgress(Player.Progress.WaterSkill);
        this.airSkill = player.GetProgress(Player.Progress.AirSkill);
    }
}