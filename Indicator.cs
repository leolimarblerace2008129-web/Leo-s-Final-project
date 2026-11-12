using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    private int _X;
    private float _MouseX;
    private float _MouseY;
    private int _Y;
    public Logic logic;
   // Start by connecting to localHost
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("LocalHost").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update()
    {
        _MouseX = Input.mousePosition[0] / 108 - 9;
        _MouseY = Input.mousePosition[1] / 108 - 5;

        _X = Mathf.RoundToInt(_MouseX);
        if(_X > 5)
        {
            _X = 5;
        } else if (_X < -5)
        {
            _X = -5;
        }

        _Y = Mathf.RoundToInt(_MouseY);
        if (_Y > 4)
        {
            _Y = 4;
        }
        else if (_Y < -4)
        {
            _Y = -4;
        }

        transform.position = new Vector2(_X, _Y);
        logic._MousePosition[0] = _X;
        logic._MousePosition[1] = _Y;
    }
}
