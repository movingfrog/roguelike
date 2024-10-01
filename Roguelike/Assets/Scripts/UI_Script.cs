using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public Choice choice;
    public static GameObject[] skillRoading;

    private void Awake()
    {
        for(int i = 0;i<3;i++)
        {
            skillRoading = GameObject.FindGameObjectsWithTag("Attacks");
            Debug.Log(skillRoading[i]);
            Debug.Log(skillRoading[i].GetComponent<Image>());
        }
    }

    public void ATK()
    {
        if(BattleManager.instance.delayTime >= BattleManager.instance.maxDelayTime)
        {
            choice = Choice.Attack;
            BattleManager.instance.isBattle = true;
            BattleManager.instance.isTurn = false;
            StartCoroutine(BattleManager.instance.BattleSystem(BattleManager.instance.giveAndTake, choice));
        }
    }

    public void Defense()
    {
        if (BattleManager.instance.delayTime >= BattleManager.instance.maxDelayTime)
        {
            choice = Choice.Defense;
            BattleManager.instance.isBattle = true;
            BattleManager.instance.isTurn = false;
            StartCoroutine(BattleManager.instance.BattleSystem(BattleManager.instance.giveAndTake, choice));
        }
    }
    public void Recovery()
    {
        if (BattleManager.instance.delayTime >= BattleManager.instance.maxDelayTime)
        {
            choice = Choice.Recovery;
            BattleManager.instance.isBattle = true;
            BattleManager.instance.isTurn = false;
            StartCoroutine(BattleManager.instance.BattleSystem(BattleManager.instance.giveAndTake, choice));
        }
    }
}
