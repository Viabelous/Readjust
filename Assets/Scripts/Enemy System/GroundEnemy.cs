using UnityEngine;

public class GroundEnemy : MonoBehaviour
{

    [SerializeField] private float pivotOffset;
    private SpriteRenderer spriteRenderer;
    private MobController mobController;
    private GameObject player;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mobController = GetComponent<MobController>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // kalau mob sedang di atas player
        if (transform.position.y + pivotOffset > player.transform.position.y - 0.85f && !mobController.onSkillTrigger)
        {
            spriteRenderer.sortingOrder = -1;
        }
        // kalau mob sedang di bawah player
        else
        {
            spriteRenderer.sortingOrder = 11;
        }
    }

    public float GetPivotOffset()
    {
        return pivotOffset;
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            // kalau musuh di atas object
            if (transform.position.y + pivotOffset > other.transform.position.y)
            {
                spriteRenderer.sortingOrder = -10;
            }
            // musuh di bawah object
            else
            {
                spriteRenderer.sortingOrder = -1;
            }
        }

        if (other.CompareTag("Player"))
        {
            spriteRenderer.sortingLayerName = other.GetComponent<PlayerController>().spriteRenderers[0].sortingLayerName;
        }

        if (other.CompareTag("Object"))
        {
            // kalau musuh di atas object
            if (transform.position.y + pivotOffset > other.transform.position.y)
            {
                spriteRenderer.sortingLayerName = "Chr Back";
            }
            // musuh di bawah object
            else
            {
                spriteRenderer.sortingLayerName = "Chr Front";
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            // kalau musuh di atas object
            if (transform.position.y + pivotOffset > other.transform.position.y)
            {
                spriteRenderer.sortingLayerName = "Chr Back";
            }
            // musuh di bawah object
            else
            {
                spriteRenderer.sortingLayerName = "Chr Front";
            }
        }
    }



}
