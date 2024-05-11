using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeliveryBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject food, shadow;
    [SerializeField] private float speed;
    [SerializeField] private float startY;
    [HideInInspector] public float healHP, healMana;
    private Vector2 minRange, maxRange;
    private Vector3 finalPos;
    private float initDistance, distance;
    private Vector3 initPos, shadowInitScale;
    private Transform player;
    private bool isGround;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        minRange = StageManager.instance.minMap;
        maxRange = StageManager.instance.maxMap;

        float x, y1, y2, z;
        x = UnityEngine.Random.Range(minRange.x, maxRange.y);
        x = UnityEngine.Random.Range(minRange.x, maxRange.y);
        y1 = startY;
        y2 = UnityEngine.Random.Range(minRange.y, maxRange.y);
        z = 0;

        initPos = new Vector3(x, y1, z);
        finalPos = new Vector3(x, y2, z);
        initDistance = Vector3.Distance(initPos, finalPos);

        transform.position = finalPos;
        food.transform.position = initPos;

        shadow.transform.position = finalPos + new Vector3(0.02f, -0.15f, 0);
        shadowInitScale = shadow.transform.localScale;
        shadow.transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {

        if (isGround)
        {
            // kalau player ada di bawah food
            if (player.position.y < food.transform.position.y + 1f)
            {
                food.GetComponent<SpriteRenderer>().sortingLayerName = "Skill Back";
            }
            // kalau player ada di atas food
            else
            {
                food.GetComponent<SpriteRenderer>().sortingLayerName = "Skill Front";
            }
        }
        else
        {

            distance = Vector3.Distance(initPos, food.transform.position);

            // kalau belum selesai turun, terus turunkan ke bawah
            if (distance < initDistance)
            {
                food.transform.Translate(Vector3.down * speed * Time.deltaTime);

                float tX = Mathf.InverseLerp(0, initDistance, distance);
                float tY = Mathf.InverseLerp(0, initDistance, distance); // Contoh, bisa diubah sesuai kebutuhan
                float tZ = Mathf.InverseLerp(0, initDistance, distance);

                Vector3 newScale = new Vector3(
                Mathf.Lerp(0, shadowInitScale.x, tX),
                Mathf.Lerp(0, shadowInitScale.y, tY),
                Mathf.Lerp(0, shadowInitScale.z, tZ)
            );
                shadow.transform.localScale = newScale;
            }
            else
            {
                food.transform.position = finalPos;
                isGround = true;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isGround)
        {
            if (other.CompareTag("Player"))
            {
                Player player = other.GetComponent<PlayerController>().player;
                player.Heal(Stat.HP, healHP);
                player.Heal(Stat.Mana, healMana);
                Destroy(gameObject);
            }
        }
    }

}