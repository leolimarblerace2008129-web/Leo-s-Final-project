using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Logic, aka local host, script. This script keeps track of all the data in the game, and it acts like a gateway used by
//other scripts to talk to each other. Almost all other scripts connect to this one via "getComponent<Logic>()" in void Start()
public class Logic : MonoBehaviour
{
    public int[] _MousePosition = new int[2];
    public int[] _StartingPosition = new int[2];
    public int _range;
    public bool _Controling = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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

