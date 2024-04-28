using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//To solve: GroundCheckBox() always returning false.
public class PlayerController : MonoBehaviour
{
    PlayerAnimator PlayerAnimator;
    Stats Stats;

    public Rigidbody2D rigBody2D;
    Collider2D collider;

    public float dirX;

    public LayerMask groundMask;
    public Transform Spawn;

    [SerializeField] float walkSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float jumpForce;

    public float speed;
    public bool right = true;
    public float jumpCount;
    public float maxJumpCount;
    public bool previousGroundCheck;

    private float minJumpTime = 0.3f;
    private float currentJumpTime;
    void Start()
    {
        PlayerAnimator = GetComponent<PlayerAnimator>();
        Stats = GetComponent<Stats>();

        rigBody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        jumpCount = 0f;
        currentJumpTime = 0f;

        rigBody2D.MovePosition(Spawn.position);

        Stats.staminaRegen = false;
    }
    void Update()
    {
        Move();
        //No fucking idea.
        if (currentJumpTime <= 0)
        {
            //Checks if there is ground beneath player.
            if (GroundCheckBox())
            {
                Debug.Log("GroundCheckBox: " + GroundCheckBox());
                jumpCount = 0f;
            }
            //Sets triggers for stop animation while jumping. Uses JumpForce() through animation event.
            PlayerAnimator.Jump();
        }
        else
        {
            //Calculates jump time.
            currentJumpTime += Time.deltaTime;
            if (currentJumpTime >= minJumpTime)
            {
                currentJumpTime = 0;
            }
        }
        //Checks if there was ground beneath player in previous frame. For JumpAnimator() and stop animation.
        previousGroundCheck = GroundCheckBox();
    }
    void Move()
    {
        //Reads horizontal input and sets it as direction.
        dirX = Input.GetAxisRaw("Horizontal");
        //Checks if player tries to run and if he has enough stamina.
        if (Input.GetButton("Run") && Stats.stamina >= 2)
        {
            //Checks if there is already an existing stamina running coroutine.
            if (Stats.staminaRun == false)
            {
                StartCoroutine(Stats.StaminaRun());
            }
            //Calculates acceleration. Restricts actual speed to speed limit.
            speed = speed + (maxSpeed * Time.deltaTime * acceleration);
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            //Calculates running speed.
            float rigVY = rigBody2D.velocity.y;
            rigBody2D.velocity = new Vector2(dirX * speed, rigVY);
        }
        else
        {
            //Checks if there is already an existing stamina regeneration running coroutine.
            if (Stats.staminaRegen == false)
            {
                StartCoroutine(Stats.StaminaRegen());
            }
            speed = walkSpeed;
            //Calculates walking speed.
            float rigVY = rigBody2D.velocity.y;
            rigBody2D.velocity = new Vector2(dirX * walkSpeed, rigVY);

        }
    }
    public void JumpForce()
    {
        //Sets jump force. Used through PlayerAnimator in animation event.
        currentJumpTime += Time.deltaTime;
        float rigVX = rigBody2D.velocity.x;
        rigBody2D.velocity = new Vector2(rigVX, jumpForce);
        jumpCount++;
    }
    public bool GroundCheckBox()
    {
        //Checks if there is ground beneath player.
        Vector3 orginVector = new Vector3(collider.bounds.size.x - 0.5f, collider.bounds.size.y, collider.bounds.size.z);
        return Physics2D.BoxCast(collider.bounds.center, orginVector, 0f, Vector2.down, 0.1f, groundMask);
    }
}
