//using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Logic logic;
    //the position of the enemy
    private int[] _CurrentEnemyPos = new int[2];
    //the type of enemy
    public string _type;
    //the current health of the enemy
    public float _Health;
    public float _MaxHealth;
    private int _Order;
    public int _attackRange;
    //how many moves the unit has left this turn
    private int _Actions;
    public int _MinDamage;
    public int _MaxDamage;
    private float _distance;
    private int[] _AttackPos = new int[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("LocalHost").GetComponent<Logic>();
        _CurrentEnemyPos[0] = Mathf.RoundToInt(transform.position[0]);
        _CurrentEnemyPos[1] = Mathf.RoundToInt(transform.position[1]);
        
        logic._BoardLayout[_CurrentEnemyPos[0] + 5, _CurrentEnemyPos[1] + 4] = _type;
        _Health = _MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //If someone calls attack on the position this enemy is on, take damage
    public void getHit(int[] _pos, int _MinDamage, int _MaxDamage)
    {
        if (_pos.SequenceEqual(_CurrentEnemyPos))
        {
            _Health -= Random.Range(_MinDamage, _MaxDamage + 1);
            if (_Health <= 0)
            {
                logic._BoardLayout[_CurrentEnemyPos[0] + 5, _CurrentEnemyPos[1] + 4] = "Empty";
                logic._EnemiesRemaining--;
                Destroy(gameObject);
            }
        }
    }
    //Start their turn (After waiting for the enemies before it)
    public void StartTurn(int order)
    {
        _Order = order;
        _Actions = 2;
        StartCoroutine(WaitInLine());
        
        //Attack units that are already in range
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _distance = Mathf.Sqrt((i - 5 - _CurrentEnemyPos[0]) * (i - 5 - _CurrentEnemyPos[0]) + (j - 4 - _CurrentEnemyPos[1]) * (j - 4 - _CurrentEnemyPos[1]));
                if (logic._BoardLayout[i, j] == "Defult" && _distance <= _attackRange && _Actions > 0)
                {
                    Debug.Log("Attacked" + i + ", " + j);
                    _AttackPos[0] = i-5;
                    _AttackPos[1] = j-4;
                    logic._EnemyAttack(_AttackPos, _MinDamage,_MaxDamage);
                    _Actions--;
                }
            }
        }
        logic._EnemiesToMove--;

    }
    //wait for thier turn to move
    IEnumerator WaitInLine()
    {
        yield return new WaitUntil(() => logic._EnemiesToMove == _Order);
    }
    
}
