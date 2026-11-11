using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    //the current status of every tile on the board
    public string[,] _BoardLayout = new string[11,9];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}

