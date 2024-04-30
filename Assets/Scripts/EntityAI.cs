using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityAI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Rigidbody2D rigBody2D;

    public LayerMask playerMask;

    [SerializeField] float moveSpeed;
    [SerializeField] float rayDistance;
    [SerializeField] float searchLenght;
    private bool lineOfSight = false;
    private bool previousLineOfSight = false;
    void Start()
    {
        rigBody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (lineOfSight)
        {
            Debug.Log("IShowSpeed");
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
         if (previousLineOfSight)
            {
                ChasePlayer();
            }
        }
        previousLineOfSight = lineOfSight;

    }
    void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, rayDistance, playerMask);
        if (ray.collider != null)
        {
            Debug.Log(ray.collider.name);
            lineOfSight = ray.collider.CompareTag("Player");
            if (lineOfSight)
            {
                Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.red);
            }
        }
        else
        {
            lineOfSight = false;
        }
    }

    private IEnumerator ChasePlayer()
    {
        for (int i = 0; i < searchLenght; i++)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
