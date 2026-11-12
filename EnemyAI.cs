using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class EnemyAI : MonoBehaviour
{
    public Logic logic;
    //the position of the enemy
    private int[] _CurrentEnemyPos = new int[2];
    //the type of enemy
    public string _type;
    //the current health of the enemy
    public int _Health = 5;
    //final field that resets logic's attack position
    public int[] _AttackReset = { -999, -999 };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("LocalHost").GetComponent<Logic>();
        _CurrentEnemyPos[0] = Mathf.RoundToInt(transform.position[0]);
        _CurrentEnemyPos[1] = Mathf.RoundToInt(transform.position[1]);
        Debug.Log("Real pos: " + _CurrentEnemyPos[0] + "," + _CurrentEnemyPos[1]);
        logic._BoardLayout[_CurrentEnemyPos[0] + 5, _CurrentEnemyPos[1] + 4] = _type;
    }

    // Update is called once per frame
    void Update()
    {
        if (logic._AttackPos.SequenceEqual(_CurrentEnemyPos))
        {
            _Health -= logic._MaxDamage;
            logic._Attack(_AttackReset, 0, 0);
            if (_Health <= 0)
            {
                Destroy(gameObject);
                logic._BoardLayout[_CurrentEnemyPos[0] + 5, _CurrentEnemyPos[1] + 4] = "Empty";
            }
        }
    }
    
}

