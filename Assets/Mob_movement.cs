using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Mob_movement : MonoBehaviour
{

    public GameObject Player;
    public GameObject Mob;
    public float speed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animate;

    //private float distance;

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.x != Mob.transform.position.x){
            movement.x = Player.transform.position.x < Mob.transform.position.x ? -1 : 1;
        } else {
            movement.x = 0;
        }
        if(Player.transform.position.y != Mob.transform.position.y){
            movement.y = Player.transform.position.y < Mob.transform.position.y ? -1 : 1;
        } else {
            movement.y = 0;
        }

        animate.SetFloat("Horizontal", movement.y);
        animate.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        if(movement.x != 0 && movement.y != 0){
            Mob.transform.localScale = new Vector3((movement.x > 0.5) ? 1 : -1, 1, 1);
        }
    }
}
