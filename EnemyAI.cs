//using System;
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
    public float _Health;
    public float _MaxHealth;
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
        _Health = _MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getHit(int[] _pos, int _MinDamage, int _MaxDamage)
    {
        //If someone calls attack on the position this enemy is on, take damage
        if (_pos.SequenceEqual(_CurrentEnemyPos))
        {
            _Health -= Random.Range(_MinDamage, _MaxDamage);
            if (_Health <= 0)
            {
                logic._BoardLayout[_CurrentEnemyPos[0] + 5, _CurrentEnemyPos[1] + 4] = "Empty";
                logic._EnemiesRemaining--;
                Destroy(gameObject);
            }
        }
    }
    
}
