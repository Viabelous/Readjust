using UnityEngine;
using UnityEngine.UI;


public class EnemyBar : MonoBehaviour
{
    [SerializeField] private Image green, red;
    private MobController mobController;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        mobController = transform.parent.GetComponent<MobController>();
        switch (mobController.enemy.type)
        {
            case EnemyType.Ground:
                offset = mobController.GetComponent<GroundEnemy>().GetPivotOffset();
                break;
            case EnemyType.Flying:
                offset = mobController.GetComponent<FlyingEnemyShadow>().GetPivotOffset();
                break;
        }
        transform.position = transform.parent.position + new Vector3(0, offset + 0.01f, 0);
    }

    public void UpdateBar()
    {
        green.fillAmount = mobController.enemy.GetHP() / mobController.enemy.GetMaxHP();
    }
}
