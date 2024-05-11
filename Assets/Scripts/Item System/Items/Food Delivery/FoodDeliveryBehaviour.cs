using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeliveryBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject food, shadow;
    [SerializeField] private float speed;
    [SerializeField] private float startY;
    [SerializeField] private Vector2 minRange, maxRange;
    [HideInInspector] public float healHP = 1;
    private Vector3 finalPos;
    private float initDistance, distance;
    private Vector3 initPos, shadowInitScale;
    private Transform player;


    void Start()
    {
        player = GameObject.Find("Player").transform;

        float x, y1, y2, z;
        x = UnityEngine.Random.Range(minRange.x, maxRange.y);
        y1 = startY;
        y2 = UnityEngine.Random.Range(minRange.y, maxRange.y);
        z = 0;

        initPos = new Vector3(x, y1, z);
        food.transform.position = initPos;

        finalPos = new Vector3(x, y2, z);

        initDistance = Vector3.Distance(initPos, finalPos);


        shadow.transform.position = finalPos;
        shadowInitScale = shadow.transform.localScale;
        // shadow.transform.localScale = new Vecto.r3(0, 0, 0);
    }

    void Update()
    {

        // kalau player ada di bawah food
        if (player.position.y < food.transform.position.y + 0.5f)
        {
            food.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy Back";
        }
        // kalau player ada di atas food
        else
        {
            food.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
        }
        distance = Vector3.Distance(initPos, food.transform.position);

        // kalau belum selesai turun, terus turunkan ke bawah
        if (distance < initDistance)
        {
            food.transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (distance <= initDistance * 0.5f)
            {
                // Hitung nilai interpolasi (lerp) untuk masing-masing komponen skala x, y, z
                float tX = Mathf.InverseLerp(shadowInitScale.x, 0, distance);
                float tY = Mathf.InverseLerp(shadowInitScale.y, 0, distance); // Contoh, bisa diubah sesuai kebutuhan
                float tZ = Mathf.InverseLerp(shadowInitScale.z, 0, distance);

                Vector3 newScale = new Vector3(
                    Mathf.Lerp(0, shadowInitScale.x, tX),
                    Mathf.Lerp(0, shadowInitScale.y, tY),
                    Mathf.Lerp(0, shadowInitScale.z, tZ)
                );
                shadow.transform.localScale = newScale;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<PlayerController>().player;
            player.Heal(Stat.HP, healHP);
            Destroy(gameObject);
        }
    }

}