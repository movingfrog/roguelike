using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Choice
{
    Attack,
    Defense,
    Recovery,
}

public class BattleManager : StatManager
{
    [Header("BattleManager")]
    public static BattleManager instance;
    [Header("Chackings")]
    public bool isTurn;
    public bool isBattle;

    public int EnemyChoice;

    public bool isStart;
    
    public Choice choice = new Choice();
    [Header("Time")]
    [SerializeField]
    private float maxDelayTime;
    [SerializeField]
    private float giveAndTake = 1f;
    private float delayTime;

    protected override void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        base.Awake();
    }

    float Damage = 0;

    IEnumerator BattleSystem(float attackTime)
    {
        isStart = true;
        switch (choice)
        {
            case Choice.Attack:
                Player.ani.SetTrigger("isAttack");
                Debug.Log("attack");
                EnemyAni[0].SetTrigger("isDamaged");
                EnemyHP[0] -= PlayerATK;
                break;
            case Choice.Defense:
                Player.ani.SetTrigger("isDefense");
                Damage = EnemyATK[0];
                break;
            case Choice.Recovery:
                break;
        }
        yield return new WaitForSeconds(attackTime);
        if (EnemyHP[0] <= 0)
        {
            Destroy(Enemies[0], 0.8f);
            EnemyAni[0].SetTrigger("isDie");
            PlayerLevelAmount += (EnemiesTag[0].CompareTag("Level1")) ? EnemyEXP[0] : (EnemiesTag[0].CompareTag("Level2")) ? EnemyEXP[1] : EnemyEXP[2];
            if (PlayerLevelAmount >= MaxEXP)
            {
                LevelUp();
            }
            yield return new WaitUntil(() => Enemies[0] == null);
            Enemies.RemoveAt(0);
            EnemiesTag.RemoveAt(0);
            EnemyHP.RemoveAt(0);
            EnemyATK.RemoveAt(0);
            EnemyAni.RemoveAt(0);
        }
        else
        {
            EnemyAni[0].SetTrigger("isDamage");
            EnemyChoice = Random.Range(0, 4);
            switch (EnemyChoice)
            {
                case 4:
                    EnemyAni[0].SetTrigger("isDefense");
                    EnemyHP[0] += PlayerATK / 4;
                    if (choice == Choice.Defense)
                        Player.ani.SetTrigger("isNotDefense");
                    break;
                default:
                    EnemyAni[0].SetTrigger("isAttack");
                    PlayerHP -= EnemyATK[0];
                    if (choice == Choice.Defense)
                    {
                        Player.ani.SetTrigger("isDefensing");
                        EnemyHP[0] -= Damage / 2;
                    }
                    else
                        Player.ani.SetBool("isDamaged", true);
                    break;
            }
        }
        PlayerHP += Damage - 10 < 0 ? 0 : Damage - 10;
        isBattle = false;
        Player.ani.SetBool("isDamaged", false);
        isStart = false;
        Die();
    }

    protected void Update()
    {
        if(!isDie)
        {
            if (delayTime >= maxDelayTime)
            {
                Player.ani.SetBool("isDamaged", false);
                if (isTurn && !isBattle)
                {
                    isBattle = true;
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        choice = Choice.Attack;
                        isTurn = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        choice = Choice.Defense;
                        isTurn = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        choice = Choice.Recovery;
                        isTurn = false;
                    }
                    else
                    {
                        Debug.Log("잘못된 버튼입니다");
                        isBattle = false;
                    }
                }
                if (isBattle && !isTurn)
                {
                    delayTime = 0;
                    StartCoroutine(BattleSystem(giveAndTake));

                }
            }
            else if (!isStart)
            {
                delayTime += Time.deltaTime;
            }
        }
    }
}
