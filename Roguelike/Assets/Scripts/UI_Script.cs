using UnityEngine;

public class UI_Script : MonoBehaviour
{
    public Choice choice;

    public void ATK()
    {
        choice = Choice.Attack;
        BattleManager.instance.isBattle = true;
        BattleManager.instance.isTurn = false;
        StartCoroutine(BattleManager.instance.BattleSystem(BattleManager.instance.giveAndTake, choice));
    }

    public void Defense()
    {
        choice = Choice.Defense;
        BattleManager.instance.isBattle = true;
        BattleManager.instance.isTurn = false;
        StartCoroutine(BattleManager.instance.BattleSystem(BattleManager.instance.giveAndTake, choice));
    }
    public void Recovery()
    {
        choice = Choice.Recovery;
        BattleManager.instance.isBattle = true;
        BattleManager.instance.isTurn = false;
        StartCoroutine(BattleManager.instance.BattleSystem(BattleManager.instance.giveAndTake, choice));
    }
}
