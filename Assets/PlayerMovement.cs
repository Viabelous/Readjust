using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D rb;

    public Animator animate;

    Vector2 movement;

    public bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if(!canMove){
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animate.SetFloat("Horizontal", movement.x);
        animate.SetFloat("Vertical", movement.y);
        animate.SetFloat("Speed", movement.sqrMagnitude);
        if(movement.x == 1){
            animate.SetFloat("Face", 1);
        } else if(movement.x == -1){
            animate.SetFloat("Face", 3);
        }
        if(movement.y == 1){
            animate.SetFloat("Face", 2);
        } else if(movement.y == -1){
            animate.SetFloat("Face", 0);
        }
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Camera.main.transform.Translate(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0);
    }
}
