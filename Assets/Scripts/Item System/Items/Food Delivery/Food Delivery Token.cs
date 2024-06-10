using UnityEngine;

[CreateAssetMenu(menuName = "Item/Food Delivery Token")]
public class FoodDeliveryToken : Item
{
    [Header("Food Sprite")]
    [SerializeField] private GameObject food;
    [Header("Heal Value")]
    [SerializeField] private float healHPPersenOfMaxHP;
    [SerializeField] private float healManaPersenOfMaxMana;
    [Header("Time Interval")]
    [SerializeField] private float timer;
    private float maxTimer;
    private bool setValue;
    // private BuffSystem buffSystem;

    public override void Activate(GameObject player)
    {
        maxTimer = timer;
        setValue = false;
    }

    public override void OnActivated(GameObject player)
    {
        if (!setValue)
        {
            float healHP = healHPPersenOfMaxHP * player.GetComponent<PlayerController>().player.GetMaxHP();
            float healMana = healManaPersenOfMaxMana * player.GetComponent<PlayerController>().player.GetMaxHP();

            food.GetComponent<FoodDeliveryBehaviour>().healHP = healHP;
            food.GetComponent<FoodDeliveryBehaviour>().healMana = healMana;

            setValue = true;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Debug.Log("Food dropped!");

            DropFood();
            timer = maxTimer;
        }
    }

    private void DropFood()
    {
        Instantiate(food);
    }
}