using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatManager : MonoBehaviour
{
    [Header("StatManager")]
    [Header("Any")]
    public bool isDie = false;
    //int playerNum;

    [Header("PlayerStats")]
    //protected GameObject PlayerName;

    //private float[] PlayerHP;
    [SerializeField]
    protected float PlayerHP;
    [SerializeField]
    protected float MaxHP = 100;
    //private float[] MaxHP;

    //private float[] PlayerMP;
    [SerializeField]
    protected float PlayerMP;
    [SerializeField]
    protected float MaxMP = 100;
    //private float[] MaxMP;

    [SerializeField]
    protected float PlayerATK = 10;
    //protected float PlayerSkillATK1;
    //protected float PlayerSkillATK2;

    public float PlayerLevelAmount;
    protected float MaxEXP = 100;
    [HideInInspector]
    public int level = 1;

    [Header("EnemyStats")]
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> EnemiesTag = new List<GameObject>();
    public List<float> EnemyHP = new List<float>();
    public List<float> EnemyATK = new List<float>();
    public List<Animator> EnemyAni = new List<Animator>();
    public float[] EnemyEXP;

    protected virtual void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayerHP = MaxHP;
        PlayerMP = MaxMP;
        //PlayerName = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        //playerNum = PlayerName.name == "PlayerKnite" ? 0 : PlayerName.name == "PlayerFighter" ? 2 : 1;
    }

    protected void Die()
    {
        if(PlayerHP <= 0)
        {
            if (!isDie)
                Player.ani.SetTrigger("isDie");
            StartCoroutine(die(3f));
        }
    }

    protected IEnumerator die(float time)
    {
        isDie = true;
        yield return new WaitForSeconds(time);
        Debug.Log("YouAreDead");
        Time.timeScale = 0;
    }

    protected void StatUp()
    {
        PlayerHP += MaxHP/10;
        MaxHP += MaxHP/10;
        PlayerMP += MaxMP/10;
        MaxMP += MaxMP/10;
        PlayerATK += PlayerATK / 10;
    }

    protected void LevelUp()
    {
        if (PlayerLevelAmount >= MaxEXP)
        {
            Debug.Log("asdf");
            PlayerLevelAmount -= MaxEXP;
            MaxEXP *= 1.5f;
            level++;
            StatUp();
        }
    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
