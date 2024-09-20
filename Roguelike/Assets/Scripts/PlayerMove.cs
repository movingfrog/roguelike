using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("The Others Script")]

    [Header("Value")]
    //공격 관련
    [SerializeField]
    private float radius = 3f;
    private bool isEnemy;

    //이동 관련
    [SerializeField]
    private float speedPower = 3f;
    [SerializeField]
    private float jumpPower = 5f;
    private bool isJump = false;
    [SerializeField]
    private LayerMask layer;

    [Header("Components")]
    Rigidbody2D rb;
    public static Animator ani;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        isJump = Physics2D.Raycast(transform.position, Vector2.down, 1f, layer);
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 1f);
        
        if (Input.GetKeyDown(KeyCode.W) && !isEnemy && isJump)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetBool("isJump", true);
        }
        if(rb.velocity.y < 0)
        {
            ani.SetTrigger("isDown");
        }
        if (isJump && rb.velocity.y == 0)
        {
            ani.SetBool("isJump", false);
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
                if (!StatManager.instance.startCorutine)
                {
                    StartCoroutine(StatManager.instance.Attack(1.5f));
                }
                rb.velocity = new Vector2(0, rb.velocity.y);
                Debug.Log(rb.velocity);
                isEnemy = true;
                i++;
            }
            else if(i == 0 && !StatManager.instance.startCorutine)
            {
                isEnemy = false;
                rb.velocity = new Vector2(speedPower, rb.velocity.y);
            }
        }
        if (rb.velocity.x > 0)
            ani.SetBool("isWalk", true);
        else
            ani.SetBool("isWalk", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
