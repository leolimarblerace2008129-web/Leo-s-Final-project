using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System.Collections;
//Logic, aka local host, script. This script keeps track of all the data in the game, and it acts like a gateway used by
//other scripts to talk to each other. Almost all other scripts connect to this one via "getComponent<Logic>()" in void Start()
public class Logic : MonoBehaviour
{
    //the XY position of the mouse with respect to the board
    public int[] _MousePosition = new int[2];
    //used by a unit to tell the board where it is trying to move from
    public int[] _StartingPosition = new int[2];
    //used by a unit to tell the board how far it can move
    public int _range;
    //is a unit currently selected to move?
    public bool _Controling = false;
    //is a unit currently selected to attack?
    public bool _Attacking = false;
    //the current status of every tile on the board
    public string[,] _BoardLayout = new string[11,9];
    //if it is the player's turn
    public bool _PlayerTurn = true;
    //units who still have actions remaining this turn
    public int _UnitsToMove;
    public int _EnemiesToMove;
    public int _EnemyMoving;
    //enemies still alive
    public int _EnemiesRemaining;
    //units still alive
    public int _UnitsRemaining;
    public EnemyAI enemy;
    public Move Unit;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        for(int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (_BoardLayout[i,j] == null)
                {
                    _BoardLayout[i, j] = "Empty";
                }
            }
        }

    }


    void Update()
    {
        //Switch to enemy turn if no units can move
        if(_PlayerTurn && _UnitsToMove == 0)
        {
            EnemyTurn();
        }
        //Win and lose conditions
        if(SceneManager.GetActiveScene().name == "Level 1" && _EnemiesRemaining == 0)
        {
            SceneManager.LoadScene("Win");
        }
        if (SceneManager.GetActiveScene().name == "Level 1" && _UnitsRemaining == 0)
        {
            SceneManager.LoadScene("Lose");
        }
    }
    //Start the game, referenced by the start button
    public void _Start()
    {
        SceneManager.LoadScene("Level 1");
        _UnitsToMove = 3;
        _EnemiesRemaining = 3;
        _UnitsRemaining = 3;
        _PlayerTurn = true;
     
    }
    //Used by units to tell enemies they are being attacked
    public void _Attack(int[] _pos, int _MinDamage, int _MaxDamage)
    {
        EnemyAI[] enemies = FindObjectsByType<EnemyAI>();
        foreach (EnemyAI enemy in enemies)
        {
            enemy.getHit(_pos, _MinDamage, _MaxDamage);
        }
        
    }
    //Used by enemies to tell units they are being attacked
    public void _EnemyAttack(int[] _pos, int _MinDamage, int _MaxDamage)
    {
        Move[] units = FindObjectsByType<Move>();
        foreach (Move unit in units)
        {
            unit.getHit(_pos, _MinDamage, _MaxDamage);
        }

    }
    //Start the enemy turn
    public void EnemyTurn()
    {
        _PlayerTurn = false;
        _EnemiesToMove = _EnemiesRemaining;
        _EnemyMoving = 0;
        EnemyAI[] enemies = FindObjectsByType<EnemyAI>();
        foreach (EnemyAI enemy in enemies)
        {
            _EnemyMoving++;
            enemy.StartTurn(_EnemyMoving);
        }
        StartCoroutine(WaitForEnemies());
        StartCoroutine(WaitToStart());

        PlayerTurn();
    }
    //Start the player turn
    public void PlayerTurn()
    {
        //search for the units and run their reset method
        
        Move[] units = FindObjectsByType<Move>();
        foreach (Move unit in units)
        {
            unit.Reset();
        }
        _UnitsToMove = _UnitsRemaining;
        _PlayerTurn = true;
    }
    //Wait for the enemies to finish thier turn
    IEnumerator WaitForEnemies()
    {
        yield return new WaitUntil(() => _EnemiesToMove == 0);
    }
    //short delay before player turn restarts
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
