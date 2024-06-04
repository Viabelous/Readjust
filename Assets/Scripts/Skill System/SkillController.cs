using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    public Skill skill;
    public Skill playerSkill;

    // [HideInInspector] public bool validAttack = true;

    // [SerializeField] private Collider2D groundCollider, flyingCollider;

    private void Awake()
    {
        playerSkill = skill.Clone();
    }

    private void Start()
    {
        playerSkill.Activate(gameObject);
    }

    private void Update()
    {
        playerSkill.OnActivated(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (playerSkill.Element != Element.Air && other.GetComponent<MobController>().enemy.type == EnemyType.Flying)
            {
                return;
            }
        }

        playerSkill.HitEnemy(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            if (playerSkill.Element != Element.Air && mobController.enemy.type == EnemyType.Flying)
            {
                return;
            }

            if (mobController.enemy.type == EnemyType.Ground)
            {
                if (transform.position.y > other.transform.position.y)
                {
                    mobController.spriteRenderer.sortingLayerName = "Chr Back";
                }
                else
                {
                    mobController.spriteRenderer.sortingLayerName = "Chr Front";
                }

                mobController.onSkillTrigger = true;
            }

        }

        playerSkill.WhileHitEnemy(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerSkill.AfterHitEnemy(other);
            other.GetComponent<MobController>().onSkillTrigger = false;
        }
    }

    private void OnAnimationEnd()
    {

        Destroy(gameObject);
    }


}