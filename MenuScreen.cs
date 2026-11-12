using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
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
    //tile attacked, used by units and enemies to tell each other to get hit
    public int[] _AttackPos = { -999, -999 };
    //minimum damage of the current attack
    public int _MinDamage;
    //maximum damage of the current attack
    public int _MaxDamage;
    //the current status of every tile on the board
    public string[,] _BoardLayout = new string[11,9];
    public EnemyAI enemy;
    
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

    // Update is called once per frame
    void Update()
    {

    }
    //Start the game, referenced by the start button
    public void _Start()
    {
        SceneManager.LoadScene("Level 1");
     
    }
    //Used by units and enemies to create an attack
    public void _Attack(int[] _pos, int _MinDamage, int _MaxDamage)
    {

        _AttackPos = _pos;
        this._MaxDamage = _MaxDamage;
        this._MinDamage = _MinDamage;
        
    }
}
