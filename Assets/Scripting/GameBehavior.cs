using UnityEngine;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Manager initialize...";
        Debug.Log(_state);
    }

    public bool showWinScreen = false;

    public string labelText = "Collect all 4 items and win your freedom!";
    public const int maxItems = 4;

    private int _itemsCollected = 0;

    public bool snowLossScreen = false;
    public int Items
    {
        get
        {
            return _itemsCollected;
        }

        set
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;

                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only "
                    + (maxItems - _itemsCollected) + " mote to go!";
            }
            Debug.LogFormat("Items: {0}", _itemsCollected);
        }
    }
    private int _playerHP = 1;

    public int HP
    {
        get { return _playerHP; }

        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                snowLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's got hurt";
            }
            Debug.LogFormat("HP: {0}", _playerHP);
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);

        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "You Won!"))
            {
                Utilities.RestartLevel();
            }
        }

        if (snowLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel(0);
            }
        }
    }

}

