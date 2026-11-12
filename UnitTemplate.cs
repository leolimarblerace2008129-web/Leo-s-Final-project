using NUnit.Framework.Internal;
using System.Linq;
using UnityEngine;
//The defult Unit code, includes basic move and attack code. all unit variations will extend this script
public class Move : MonoBehaviour
{
    //the unit's position, used for relocation
    public int[] _CurrentPos = new int[2];
    //the type of unit this is
    public string _type;
    //if the unit currenly selected to move
    public bool _Moving = false;
    //if the unit currenly selected to attack
    public bool _Attacking = false;
    public int _MinDamage;
    public int _MaxDamage;
    //used to calculate the distance between the current and target location when moving
    private float _distance;
    //how far the unit can move in a single turn
    public int _moveRange;
    //how far the unit can attack in a single turn
    public int _attackRange;
    public int _Actions = 2;
    public Logic logic;
    public AudioClip MoveRejected;
    public AudioSource AudioSource;
    // Start by connecting to localHost and initializing the position
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("LocalHost").GetComponent<Logic>();
        _CurrentPos[0] = Mathf.RoundToInt(transform.position[0]);
        _CurrentPos[1] = Mathf.RoundToInt(transform.position[1]);
        logic._BoardLayout[_CurrentPos[0] + 5, _CurrentPos[1] + 4] = _type;
    }

    // Update is called once per frame
    void Update()
    {
        //The move script, triggered by the left mouse button
        //send all relevent information to localHost when moving
        if (logic._PlayerTurn && _Actions > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (logic._MousePosition.SequenceEqual(_CurrentPos))
                {
                    //toggle if the unit is moving when it's clicked
                    if (_Moving)
                    {

                        _Moving = false;
                        logic._Controling = false;
                    }
                    else if (!logic._Controling && !logic._Attacking)
                    {
                        _Moving = true;
                        logic._Controling = true;
                        logic._StartingPosition = _CurrentPos;
                        logic._range = _moveRange;
                    }
                }
                else if (_Moving)
                {
                    //if it is already selected to move and a different tile is clicked, move there if it's a valid destination
                    _distance = Mathf.Sqrt((logic._MousePosition[0] - _CurrentPos[0]) * (logic._MousePosition[0] - _CurrentPos[0]) + (logic._MousePosition[1] - _CurrentPos[1]) * (logic._MousePosition[1] - _CurrentPos[1]));
                    if (_distance <= _moveRange && logic._BoardLayout[logic._MousePosition[0] + 5, logic._MousePosition[1] + 4].Equals("Empty"))
                    {
                        logic._BoardLayout[_CurrentPos[0] + 5, _CurrentPos[1] + 4] = "Empty";
                        transform.position = new Vector2(logic._MousePosition[0], logic._MousePosition[1]);
                        _CurrentPos[0] = Mathf.RoundToInt(transform.position[0]);
                        _CurrentPos[1] = Mathf.RoundToInt(transform.position[1]);
                        logic._BoardLayout[logic._MousePosition[0] + 5, logic._MousePosition[1] + 4] = _type;
                        logic._Controling = false;
                        _Moving = false;
                        _Actions--;
                        if (_Actions == 0)
                        {
                            logic._UnitsToMove--;
                        }
                    }
                    else
                    {
                        AudioSource.clip = MoveRejected;
                        AudioSource.Play();

                    }
                }
            }
            //Attacking code
            if (Input.GetMouseButtonDown(0))
            {
                if (logic._MousePosition.SequenceEqual(_CurrentPos))
                {
                    //toggle if the unit is moving when it's clicked

                    if (_Attacking)
                    {

                        _Attacking = false;
                        logic._Attacking = false;
                    }
                    else if (!logic._Controling && !logic._Attacking)
                    {
                        _Attacking = true;
                        logic._Attacking = true;
                        logic._StartingPosition = _CurrentPos;
                        logic._range = _attackRange;
                    }
                }
                else if (_Attacking)
                {
                    //if it is already selected to move and a different tile is clicked, move there if it's a valid destination
                    _distance = Mathf.Sqrt((logic._MousePosition[0] - _CurrentPos[0]) * (logic._MousePosition[0] - _CurrentPos[0]) + (logic._MousePosition[1] - _CurrentPos[1]) * (logic._MousePosition[1] - _CurrentPos[1]));
                    if (_distance <= _attackRange && logic._BoardLayout[logic._MousePosition[0] + 5, logic._MousePosition[1] + 4].Equals("Defult Enemy"))
                    {
                        logic._Attack(logic._MousePosition, _MinDamage, _MaxDamage);
                        logic._Attacking = false;
                        _Attacking = false;
                        _Actions--;
                        if(_Actions == 0)
                        {
                            logic._UnitsToMove--;
                        }

                    }
                    else
                    {
                        AudioSource.clip = MoveRejected;
                        AudioSource.Play();

                    }
                }
            }
        }
        
    }

    public void Reset()
    {
        _Actions = 2;
        logic._UnitsToMove++;
    }

}
    
        

