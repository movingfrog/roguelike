using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("The Others Script")]
    public StatManager statManager;

    [Header("Value")]
    //공격 관련
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask layer;
    private bool isEnemy;

    //이동 관련
    [SerializeField]
    private float speedPower;
    [SerializeField]
    private float jumpPower;


    [Header("Components")]
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isEnemy)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        int i = 0;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D colliders in collider)
        {
            if (colliders.CompareTag("Enemy"))
            {
                if (!statManager.startCorutine)
                {
                    StartCoroutine(statManager.Attack(3f));
                }
                rb.velocity = new Vector2(0, rb.velocity.y);
                Debug.Log(rb.velocity);
                isEnemy = true;
                i++;
            }
            else if(i == 0)
            {
                isEnemy = false;
                rb.velocity = new Vector2(speedPower, rb.velocity.y);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
