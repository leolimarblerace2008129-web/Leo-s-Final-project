//using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using System.Collections;
using UnityEditor.Experimental.GraphView;
//the defult enemy. Can be attacked by units and acts on the enemy turn
public class EnemyAI : MonoBehaviour
{
    public Logic logic;
    //the position of the enemy
    public int[] _CurrentEnemyPos = new int[2];
    //the type of enemy
    public string _type;
    //the current health of the enemy
    public float _Health;
    //the max health an enemy can have
    public float _MaxHealth;
    //the order in which this enemy will act during the enemy turn
    private int _Order;
    //how far this enemy can hit
    public int _attackRange;
    //how many moves the unit has left this turn
    private int _Actions;
    //minimum damage this enemy can deal
    public int _MinDamage;
    //maximum damage this enemy can deal
    public int _MaxDamage;
    //used to calculate how far a target is
    private float _distance;
    public int _MoveRange;
    //used to tell logic which tile this enemy wants to attack
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
        

        if (CheckForTargets(_CurrentEnemyPos[0], _CurrentEnemyPos[1]) == 1)
        {
            AttackUnits(1);
            if(CheckForTargets(_CurrentEnemyPos[0], _CurrentEnemyPos[1]) == 1)
            {
                AttackUnits(1);
            }
            else
            {
                //Move to nearest target without entering their range
            }
        } else if (CheckForTargets(_CurrentEnemyPos[0], _CurrentEnemyPos[1]) > 1)
        {
            AttackUnits(2);
        } else
        {
            
            for(int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _distance = Mathf.Sqrt((i - 5 - _CurrentEnemyPos[0]) * (i - 5 - _CurrentEnemyPos[0]) + (j - 4 - _CurrentEnemyPos[1]) * (j - 4 - _CurrentEnemyPos[1]));
                    if (_distance < _MoveRange && logic._BoardLayout[i, j] == "Empty")
                    {
                        if (CheckForTargets(i-5, j-4) >= 1 && _Actions == 2)
                        {
                            MoveTo(i,j);
                            AttackUnits(1);
                            
                        }
                        
                    } 
                }
            }
            if(_Actions == 2)
            {
                Debug.Log("Last resort");
                
            }
        }
        StartCoroutine(Dealy(1f));
        logic._EnemiesToMove--;

    }
    //Attack units that are already in range
    public void AttackUnits(int limit)
    {
        int attackcount = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _distance = Mathf.Sqrt((i - 5 - _CurrentEnemyPos[0]) * (i - 5 - _CurrentEnemyPos[0]) + (j - 4 - _CurrentEnemyPos[1]) * (j - 4 - _CurrentEnemyPos[1]));
                if (logic._BoardLayout[i, j] == "Defult" && _distance <= _attackRange && attackcount < limit)
                {
                    StartCoroutine(Dealy(1f));
                    Debug.Log("Attacked" + i + ", " + j);
                    _AttackPos[0] = i - 5;
                    _AttackPos[1] = j - 4;
                    logic._EnemyAttack(_AttackPos, _MinDamage, _MaxDamage);
                    _Actions--;
                    attackcount++;
                }
            }
        }
    }
    //check how many targets there are
    public int CheckForTargets(int x, int y)
    {
        int count = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _distance = Mathf.Sqrt((i - 5 - x) * (i - 5 - x) + (j - 4 - y) * (j - 4 - y));
                if (logic._BoardLayout[i, j] == "Defult" && _distance <= _attackRange)
                {
                    count++;
                }
            }
        }
        return count;
    }
    public void MoveTo(int X, int Y)
    {
        StartCoroutine(Dealy(1f));
        logic._BoardLayout[_CurrentEnemyPos[0] + 5, _CurrentEnemyPos[1] + 4] = "Empty";
        transform.position = new Vector2(X-5, Y-4);
        _CurrentEnemyPos[0] = Mathf.RoundToInt(X-5);
        _CurrentEnemyPos[1] = Mathf.RoundToInt(Y-4);
        logic._BoardLayout[X, Y] = _type;
        _Actions--;
    }
    //wait for thier turn to move
    IEnumerator WaitInLine()
    {
        yield return new WaitUntil(() => logic._EnemiesToMove == _Order);
    }

    IEnumerator Dealy(float i)
    {
       
        yield return new WaitForSeconds(i);
    }

    private void OnMouseOver()
    {
        logic._MousePosition[0] = _CurrentEnemyPos[0];
        logic._MousePosition[1] = _CurrentEnemyPos[1];
    }
}
