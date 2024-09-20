using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [Header("Any")]
    [HideInInspector]
    public bool startCorutine;
    int playerNum;
    public static StatManager instance;

    [Header("PlayerStats")]
    private GameObject PlayerName;

    //private float[] PlayerHP;
    public float PlayerHP;
    [SerializeField]
    private float MaxHP;
    //private float[] MaxHP;

    //private float[] PlayerMP;
    private float PlayerMP;
    [SerializeField]
    private float MaxMP;
    //private float[] MaxMP;

    [SerializeField]
    private float PlayerATK;
    private float PlayerSkillATK1;
    private float PlayerSkillATK2;

    public float PlayerLevelAmount;
    private float MaxEXP = 100;
    [HideInInspector]
    public int level = 1;

    [Header("EnemyStats")]
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> EnemiesTag = new List<GameObject>();
    public List<float> EnemyHP = new List<float>();
    public List<float> EnemyATK = new List<float>();
    public float[] EnemyEXP;

    private void Awake()
    {
        PlayerHP = MaxHP;
        PlayerMP = MaxMP;
        PlayerName = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        //playerNum = PlayerName.name == "PlayerKnite" ? 0 : PlayerName.name == "PlayerFighter" ? 2 : 1;
    }

    public void StatUp()
    {
        PlayerHP += MaxHP/10;
        MaxHP += MaxHP/10;
        PlayerMP += MaxMP/10;
        MaxMP += MaxMP/10;
        PlayerATK += PlayerATK / 10;
    }

    public void LevelUp()
    {
        if (PlayerLevelAmount >= MaxEXP)
        {
            Debug.Log("adfs");
            PlayerLevelAmount -= MaxEXP;
            MaxEXP *= 1.5f;
            level++;
            StatUp();
        }
    }

    public IEnumerator Attack(float Time)
    {
        startCorutine = true;
        Debug.Log("IsCorutine");
        Player.ani.SetTrigger("isAttack");
        EnemyHP[0] -= PlayerATK;
        if(PlayerMP < MaxHP)
        {
            PlayerMP += 5;
        }
        if (EnemyHP[0] <= 0)
        {
            Debug.Log(Enemies[0].GetComponentInChildren<Transform>().tag);
            PlayerLevelAmount += (EnemiesTag[0].CompareTag("Level1")) ? EnemyEXP[0] : (EnemiesTag[0].CompareTag("Level2")) ? EnemyEXP[1] : EnemyEXP[2];
            LevelUp();
            yield return new WaitForSeconds(0.5f);
            Destroy(Enemies[0]);
            Enemies.RemoveAt(0);
            EnemiesTag.RemoveAt(0);
            EnemyHP.RemoveAt(0);
            EnemyATK.RemoveAt(0);
        }
        Debug.Log("PlayerAttack");
        yield return new WaitForSeconds(Time);
        startCorutine = false;
        if (Enemies != null)
        {
            PlayerHP -= EnemyATK[0];
            Debug.Log("EnemyAttack");
        }
    }
}
