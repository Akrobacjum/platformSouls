using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigBody2D;
    Collider2D collider;

    Animator animator;
    MeshRenderer renderer;

    float dirX;

    public LayerMask groundMask;
    public Transform Spawn;

    [SerializeField] float walkSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float jumpForce;
    [SerializeField] float maxJumpCount;

    [SerializeField] Material ElfIdleMaterial;
    [SerializeField] Material ElfWalkMaterial;
    [SerializeField] Material ElfRunMaterial;
    [SerializeField] Material ElfJumpMaterial;
    [SerializeField] Material ElfStopMaterial;

    float speed;
    bool previousGroundCheck;
    public bool right = true;
    float jumpCount;

    private float minJumpTime = 0.3f;
    private float currentJumpTime;
    void Start()
    {
        rigBody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        animator = GetComponent<Animator>();
        renderer = GetComponent<MeshRenderer>();

        jumpCount = 0f;
        currentJumpTime = 0f;

        rigBody2D.MovePosition(Spawn.position);
    }
    void Update()
    {
        Move();
        if (currentJumpTime <= 0)
        {
            if (GroundCheckBox())
            {
                jumpCount = 0f;
            }
            Jump();
        }
        else
        {
            currentJumpTime += Time.deltaTime;
            if (currentJumpTime >= minJumpTime)
            {
                currentJumpTime = 0;
            }
        }
        Flip();
        Animate();
        previousGroundCheck = GroundCheckBox();

    }
    void Move()
    {
        if (Input.GetButton("Fire1"))
        {
            speed = speed + (maxSpeed * Time.deltaTime * acceleration);
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            dirX = Input.GetAxisRaw("Horizontal");
            float rigVY = rigBody2D.velocity.y;
            rigBody2D.velocity = new Vector2(dirX * speed, rigVY);
        }
        else
        {
            speed = walkSpeed;
            dirX = Input.GetAxisRaw("Horizontal");
            float rigVY = rigBody2D.velocity.y;
            rigBody2D.velocity = new Vector2(dirX * walkSpeed, rigVY);
        }
        animator.SetBool("Input", dirX != 0);
    }
    void Flip()
    {
        if (dirX < 0 && right)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            right = false;
        }
        if (dirX > 0 && !right)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            right = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            animator.SetTrigger("Jump");
        }
    }
    public void JumpForce()
    {
        currentJumpTime += Time.deltaTime;
        float rigVX = rigBody2D.velocity.x;
        rigBody2D.velocity = new Vector2(rigVX, jumpForce);
        jumpCount++;
        Debug.Log("dsadsa");
    }
    bool GroundCheckBox()
    {
        Vector3 orginVector = new Vector3(collider.bounds.size.x - 0.5f, collider.bounds.size.y, collider.bounds.size.z);
        return Physics2D.BoxCast(collider.bounds.center, orginVector, 0f, Vector2.down, 0.1f, groundMask);
    }
    void Animate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rigBody2D.velocity.x));  
        if (previousGroundCheck == false && GroundCheckBox())
        {
            animator.SetTrigger("Stop");
        }
        else if (previousGroundCheck == true && GroundCheckBox())
        {
            animator.ResetTrigger("Stop");
        }
    }
}
