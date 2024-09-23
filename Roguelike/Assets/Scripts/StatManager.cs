using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
}
