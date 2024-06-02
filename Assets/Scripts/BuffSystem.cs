using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuffType
{
    HP,
    Mana,
    Shield,
    ATK,
    DEF,
    AGI,
    FOC,
    Thorn,
    Fire,
    Earth,
    Water,
    Air,
    Harmony,
    Idiosyncrasy,
    Breezewheel,
    Custom
}

public class Buff
{
    public string id;
    public string name;
    public BuffType type;
    public float value;
    public float timer;
    public IEnumerator coroutine;

    public Buff(string id, string name, BuffType type, float value, float timer)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.value = value;
        this.timer = timer;
    }
}

// dikasih ke skill
public class BuffSystem : MonoBehaviour
{

    public CharacterType type;

    [HideInInspector]
    public List<Buff> buffsActive = new List<Buff>();
    private MonoBehaviour chrController;

    void Start()
    {
        switch (type)
        {
            case CharacterType.Player:
                chrController = GetComponent<PlayerController>();
                break;
            case CharacterType.Enemy:
                chrController = GetComponent<MobController>();
                break;
        }
    }

    void Update()
    {
        switch (type)
        {
            case CharacterType.Player:
                // print("Apakah ada buff shield? " + CheckBuff(BuffType.Shield));

                // kalau sedang pakai buff shield
                if (CheckBuff(BuffType.Shield))
                {
                    // cari index buff shield
                    int index = buffsActive.FindIndex(buff => buff.type == BuffType.Shield);

                    // kalau shield yg dipakai saat ini sudah habis, 
                    // maka hapus dari list buff yg sedang dipakai
                    if (((PlayerController)chrController).player.shield <= 0)
                    {
                        RemoveBuff(buffsActive[index]);
                    }
                }

                break;
        }
    }

    public void ActivateBuff(Buff buff)
    {
        if (buff.timer == 0)
        {
            AddBuff(buff);
        }
        else
        {
            buff.coroutine = CoroutineBuff(buff);
            StartCoroutine(buff.coroutine);
        }
    }


    public void DeactivateBuff(Buff buff)
    {
        RemoveBuff(buff);
    }

    public void DeactivateBuff(BuffType buffType)
    {
        RemoveBuff(buffsActive.Find(buff => buff.type == buffType));
    }

    public void DeactivateAllRelatedBuff(string name)
    {
        List<Buff> buffDetected = buffsActive.FindAll(buff => buff.name == name);

        foreach (Buff buff in buffDetected)
        {
            RemoveBuff(buff);
        }
    }

    public string GetBuffNameOfType(BuffType buffType)
    {
        return buffsActive.Find(buff => buff.type == buffType).name;
    }

    public IEnumerator CoroutineBuff(Buff buff)
    {
        AddBuff(buff);
        yield return new WaitForSeconds(buff.timer);

        // kalau misalnya sebelum waktunya habis
        // tapi buffnya sudah hilang,
        // tidak perlu remove lagi
        // biasanya terjadi saat efek skill te reset 
        // karena player menekan skill sebelum efek skill habis
        if (CheckBuff(buff))
        {
            RemoveBuff(buff);
        }
    }

    public bool CheckBuff(BuffType type)
    {
        if (buffsActive.FindIndex(buff => buff.type == type) != -1)
        {
            return true;
        }
        return false;
    }

    public bool CheckBuff(string name)
    {
        if (buffsActive.FindIndex(buff => buff.name == name) != -1)
        {
            return true;
        }
        return false;
    }

    public bool CheckBuff(Buff buff)
    {
        if (buffsActive.Contains(buff))
        {
            return true;
        }
        return false;
    }

    public float GetBuffValues(BuffType type)
    {
        float totalValue = 0;
        List<Buff> buffs = buffsActive.FindAll(buff => buff.type == type);

        foreach (Buff buff in buffs)
        {
            totalValue += buff.value;
        }

        return totalValue;
    }

    private void AddBuff(Buff buff)
    {

        if (buffsActive.Contains(buff))
        {
            return;
        }

        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                // untuk semua buff yg memiliki waktu,
                // maka buff waktu akan direset
                // dan efek dari buff tidak akan berganda
                if (buff.timer > 0)
                {
                    ResetSimiliarBuff(buff);
                }

                switch (buff.type)
                {
                    // will of fire
                    case BuffType.ATK:
                        playerController.player.Upgrade(Stat.ATK, buff.value);
                        break;

                    case BuffType.DEF:
                        playerController.player.Upgrade(Stat.DEF, buff.value);

                        break;

                    // light step
                    case BuffType.AGI:
                        playerController.player.Upgrade(Stat.AGI, buff.value);
                        break;

                    // calm
                    case BuffType.FOC:
                        playerController.player.Upgrade(Stat.FOC, buff.value);
                        break;

                    // sacrivert
                    case BuffType.Mana:
                        playerController.player.Heal(Stat.Mana, buff.value);
                        break;

                    // sanare
                    case BuffType.HP:
                        playerController.player.Heal(Stat.HP, buff.value);
                        break;

                    // preserve & invitro
                    case BuffType.Shield:
                        // int index = buffsActive.FindIndex(buff => buff.type == BuffType.Shield);

                        // kalau sedang menggunakan shield, 
                        // maka hapus buff shield sebelumnya
                        ResetSimiliarBuff(buff);

                        playerController.player.Upgrade(Stat.Shield, buff.value);

                        break;

                    // thorn cover
                    case BuffType.Thorn:
                        break;


                }

                break;

                // case CharacterType.Enemy:
                //     MobController mobController = (MobController)chrController;
                //     break;

        }

        buffsActive.Add(buff);
    }

    private void RemoveBuff(Buff buff)
    {
        if (!buffsActive.Contains(buff))
        {
            return;
        }

        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                switch (buff.type)
                {
                    // will of fire
                    case BuffType.ATK:
                        playerController.player.Downgrade(Stat.ATK, buff.value);
                        break;

                    // fudoshin
                    case BuffType.DEF:
                        playerController.player.Downgrade(Stat.DEF, buff.value);
                        break;

                    // calm
                    case BuffType.FOC:
                        playerController.player.Downgrade(Stat.FOC, buff.value);
                        break;

                    // calm
                    case BuffType.AGI:
                        playerController.player.Downgrade(Stat.AGI, buff.value);
                        break;
                }
                break;
        }

        buffsActive.Remove(buff);
    }

    public float GetAllBuffValues(BuffType type)
    {
        List<Buff> buffs = buffsActive.FindAll(buff => buff.type == type);
        float sum = 0;
        foreach (Buff buff in buffs)
        {
            sum += buff.value;
        }
        return sum;
    }

    private void ResetSimiliarBuff(Buff buff)
    {
        // kalau sedang menggunakan shield, 
        // maka hapus buff shield sebelumnya
        if (CheckBuff(buff.name))
        {
            int prevBuffIndex = buffsActive.FindIndex(buffActive => buffActive.name == buff.name);
            RemoveBuff(buffsActive[prevBuffIndex]);
            // StopCoroutine(buff.coroutine);
        }

    }

    private bool BuffIsSkill(Buff buff)
    {
        if (buff.id.Contains("skill"))
        {
            return true;
        }
        return false;

    }

}