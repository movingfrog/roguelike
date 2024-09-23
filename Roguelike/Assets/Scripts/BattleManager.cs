using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Bools")]
    public bool isTurn;
    public bool isBattle;

    public bool isAttack;
    public bool isRecovery;
    public bool isDefense;

    private void Update()
    {
        if (isTurn&&isBattle)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isAttack = true;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                isRecovery = true;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                isDefense = true;
            }
            else
            {
                Debug.Log("잘못된 버튼입니다");
            }
        }
    }
}
