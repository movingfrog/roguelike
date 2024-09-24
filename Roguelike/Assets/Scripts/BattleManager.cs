using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public enum Choice
{
    Attack,
    Defense,
    Recovery,
}

public class BattleManager : MonoBehaviour
{
    [Header("Chackings")]
    public bool isTurn;
    public bool isBattle;

    public int EnemyChoice;

    public Choice choice = new Choice();

    private void Update()
    {
        if (isTurn&&!isBattle)
        {
            isBattle = true;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                choice = Choice.Attack;
                isTurn = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                choice = Choice.Defense;
                isTurn = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                choice = Choice.Recovery;
                isTurn = false;
            }
            else
            {
                Debug.Log("잘못된 버튼입니다");
            }
        }
        if (isBattle && !isTurn)
        {
            switch (choice)
            {
                case Choice.Attack:
                    Player.ani.SetTrigger("isAttack");
                    if(Player.ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                    {
                        StatManager.instance
                    }
                    break;
                case Choice.Defense:
                    break;
                case Choice.Recovery:
                    break;
            }
            EnemyChoice = Random.Range(0, 4);
            switch (EnemyChoice)
            {
                case 1:
                case 2:
                case 3:
                    break;
                default:
                    break;
            }
            isBattle = false;
        }
    }
}
