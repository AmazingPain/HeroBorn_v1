using CustomExstansion;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public Stack<string> lootStack = new Stack<string>();
    public Queue<string> activePlayers = new Queue<string>();

    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Print;

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    void Start()
    {
        Initialize();

        InventoryList<string> inventoryList = new InventoryList<string>();

        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);


    }


    public void Initialize()
    {
        _state = "Manager initialize...";
        _state.FancyDebug();
        Debug.Log(_state);

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Winged Boot");
        lootStack.Push("Golden Key");
        lootStack.Push("Mythril Bracers");

        activePlayers.Enqueue("Harrison");
        activePlayers.Enqueue("Ford");
        activePlayers.Enqueue("Ferrari");

        debug(_state);
        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");

        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();

        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }


    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
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
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }
            }
        }
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        var itemFound = lootStack.Contains("Golden Key");

        Debug.LogFormat("you find a {0}!", itemFound);

        Debug.LogFormat("You got a {0}!", nextItem);

        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);

        var currentPlayers = activePlayers.Peek();
        Debug.LogFormat("you Player a {0}!", currentPlayers);


    }

}

