using UnityEngine;
using System.IO;

public class PlayerData
{
    public float aerus;
    public float exp;
    public float venetia;

    public float maxHPLevel, maxManaLevel, atkLevel, defLevel, agiLevel, focLevel;

    public float story;

    public PlayerData(Player player)
    {
        this.aerus = player.aerus;
        this.exp = player.exp;
        this.venetia = player.venetia;
        this.story = player.story;

        this.maxHPLevel = player.maxHPLevel;
        this.maxManaLevel = player.maxManaLevel;
        this.atkLevel = player.atkLevel;
        this.defLevel = player.defLevel;
        this.agiLevel = player.agiLevel;
        this.focLevel = player.focLevel;
    }
}