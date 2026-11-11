using System.Linq;
using UnityEngine;

public class Squarecode : MonoBehaviour
{
    //this square's position on the board
    public int[] _pos = new int[2];
    public Logic logic;
    //if the mouse is hovering over this square
    public bool _Huver = false;
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
        //check if the mouse is hovering over this square
        if(logic._MousePosition.SequenceEqual(_pos))
        {
            _Huver = true;
        }
        else
        {
            _Huver = false;
        }

        //recieve the data from logic and turn blue if this a unit is trying to move and this square is a vaild destination
        if (logic._Controling)
        {
            _distence = Mathf.Sqrt((logic._StartingPosition[0] - _pos[0]) * (logic._StartingPosition[0] - _pos[0]) + (logic._StartingPosition[1] - _pos[1]) * (logic._StartingPosition[1] - _pos[1]));
            if(_distence <= logic._range && logic._BoardLayout[_pos[0] + 5, _pos[1] + 4].Equals("Empty"))
            {
                SpriteRender.color = Color.lightBlue;
            }
        } else
        {
            SpriteRender.color = Color.white;
        }
    }
}
