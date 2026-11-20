using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
//The tiles on the battle field. Lights up to tell the player which tiles are valid when moving or attacking
//Now also tracks the mouse position
public class Squarecode : MonoBehaviour
{
    //this square's position on the board
    public int[] _pos = new int[2];
    public Logic logic;
    //used to calculate if this is a valid destination when a unit moves
    private float _distence;
    public SpriteRenderer SpriteRender;
    // Start by connecting to localHost and initializing the position
    void Start()
    {
        _pos[0] = Mathf.FloorToInt(transform.position[0]);
        _pos[1] = Mathf.FloorToInt(transform.position[1]);
        logic = GameObject.FindGameObjectWithTag("LocalHost").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update()
    {
       

        //recieve the data from logic and turn blue if this a unit is trying to move and this square is a vaild destination
        if (logic._Controling)
        {
            
            _distence = Mathf.Sqrt((logic._StartingPosition[0] - _pos[0]) * (logic._StartingPosition[0] - _pos[0]) + (logic._StartingPosition[1] - _pos[1]) * (logic._StartingPosition[1] - _pos[1]));
            if(_distence <= logic._range && logic._BoardLayout[_pos[0] + 5, _pos[1] + 4].Equals("Empty"))
            {
                SpriteRender.color = Color.lightBlue;
            }

        } else if (logic._Attacking)
        {
            _distence = Mathf.Sqrt((logic._StartingPosition[0] - _pos[0]) * (logic._StartingPosition[0] - _pos[0]) + (logic._StartingPosition[1] - _pos[1]) * (logic._StartingPosition[1] - _pos[1]));
            if (_distence <= logic._range)
            {
                if (logic._BoardLayout[_pos[0] + 5, _pos[1] + 4].Equals("Defult Enemy"))
                {
                    SpriteRender.color = Color.softRed;
                }
                else
                {
                    SpriteRender.color = Color.pink;
                }
                
            }
        }
        else
        {
            SpriteRender.color = Color.white;
        }
    }
    //if the mouse is over this square, send it to logic
    private void OnMouseOver()
    {
        logic._MousePosition[0] = _pos[0];
        logic._MousePosition[1] = _pos[1];
    }
}
