using System.Linq;
using UnityEngine;

public class Move : MonoBehaviour
{
    //the unit's position, used for relocation
    public int[] _CurrentPos = new int[2];
    //the type of unit this is
    public string type;
    //if the unit currenly selected to move
    public bool _Moving = false;
    //used to calculate the distance between the current and target location when moving
    private float _distance;
    //how far the unit can move in a single turn
    public int _range;
    public Logic logic;
    // Start by connecting to localHost and initializing the position
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("LocalHost").GetComponent<Logic>();
        _CurrentPos[0] = Mathf.RoundToInt(transform.position[0]);
        _CurrentPos[1] = Mathf.RoundToInt(transform.position[1]);
    }

    // Update is called once per frame
    void Update()
    {
        //if the left mouse is clicked, start the move script
        if (Input.GetMouseButtonDown(0))
        {
            if (logic._MousePosition.SequenceEqual(_CurrentPos))
            {
                //toggle if the unit is moving when it's clicked
                
                if (_Moving)
                {
                    
                    _Moving = false;
                    logic._Controling = false;
                }
                else if (!logic._Controling)
                {
                    
                    _Moving = true;
                    logic._Controling = true;
                    logic._StartingPosition = _CurrentPos;
                    logic._range = _range;
                }
            }
            else if (_Moving)
            {
                //if is alread selected to move and a different tile is clicked, check the distance and move there if it's close enough
                _distance = Mathf.Sqrt((logic._MousePosition[0] - _CurrentPos[0]) * (logic._MousePosition[0] - _CurrentPos[0]) + (logic._MousePosition[1] - _CurrentPos[1]) * (logic._MousePosition[1] - _CurrentPos[1]));
                if (_distance <= _range)
                {
                    transform.position = new Vector2(logic._MousePosition[0], logic._MousePosition[1]);
                    _CurrentPos[0] = Mathf.RoundToInt(transform.position[0]);
                    _CurrentPos[1] = Mathf.RoundToInt(transform.position[1]);
                    logic._Controling = false;
                    _Moving = false;
                }
                else
                {
                    logic._Controling = false;
                    _Moving = false;
                }
            }
        }
    }

}
    
        


