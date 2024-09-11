using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [Header("Any")]
    public bool startCorutine;

    [Header("PlayerStats")]
    public float PlayerHP;
    public float PlayerMP;
    public float PlayerATK;
    public float PlayerSkillATK1;
    public int PlayerSkillATK2;
    public float PlayerLevelAmount;

    [Header("EnemyStats")]
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> EnemiesTag = new List<GameObject>();
    public List<float> EnemyHP = new List<float>();
    public List<float> EnemyATK = new List<float>();
    public float[] EnemyEXP;


    public IEnumerator Attack(float Time)
    {
        startCorutine = true;
        Debug.Log("IsCorutine");
        EnemyHP[0] -= PlayerATK;
        if (EnemyHP[0] <= 0)
        {
            Debug.Log(Enemies[0].GetComponentInChildren<Transform>().tag);
            PlayerLevelAmount += (EnemiesTag[0].CompareTag("Level1")) ? EnemyEXP[0] : (EnemiesTag[0].CompareTag("Level2")) ? EnemyEXP[1] : EnemyEXP[2];
            Destroy(Enemies[0]);
            Enemies.RemoveAt(0);
            EnemiesTag.RemoveAt(0);
            EnemyHP.RemoveAt(0);
            EnemyATK.RemoveAt(0);
        }
        Debug.Log("PlayerAttack");
        yield return new WaitForSeconds(Time);
        if(Enemies != null)
        {
            PlayerHP -= EnemyATK[0];
            Debug.Log("EnemyAttack");
        }
        startCorutine = false;
    }
}
