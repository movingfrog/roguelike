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
        if(!BattleManager.instance.isDie)
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
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            bool isUse = true;
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach(Collider2D colliders in collider)
            {
                if (colliders.CompareTag("Enemy"))
                {
                    if (!BattleManager.instance.isTurn&&!BattleManager.instance.isBattle)
                    {
                        BattleManager.instance.isTurn = true;
                    }
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    isEnemy = true;
                    isUse = false;
                }
                else if(isUse && !BattleManager.instance.isTurn && !BattleManager.instance.isBattle)
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
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
