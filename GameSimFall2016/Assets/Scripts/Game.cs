using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public enum GameState
    {
        FREEROAM,
        STRAFE,
        PUZZLE,
        CUTSCENE,
        PAUSE,
    }

    public enum InputState
    {
        KEYBOARD,
        JOYSTICK
    }

    static private Game instance;

    public GameState gameState;

    public InputState inputState;

    [HideInInspector]
    public Player currentPlayer = null;
    public Player[] players = new Player[4];

    private Player myLastPlayer;

    public static Game getInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Game>();
            if (instance == null)
            {
                GameObject singleton = new GameObject("Singleton.Game");
                instance = singleton.AddComponent<Game>();
                DontDestroyOnLoad(singleton);
            }
        }
        return instance;
    }

	// Use this for initialization
	void Start () 
    {
        Application.targetFrameRate = 60;

        players[0] = GameObject.FindObjectOfType<Girl>();
        players[1] = GameObject.FindObjectOfType<Bird>();
        players[2] = GameObject.FindObjectOfType<Rabbit>();
        players[3] = GameObject.FindObjectOfType<Cat>();

        currentPlayer = this.players[0];
        myLastPlayer = currentPlayer;

        StartCoroutine(activeSet());
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void OnGUI()
    {
        if ( Event.current.isKey || Event.current.isMouse )
        {
            inputState = InputState.KEYBOARD;
        }
        else if (Input.GetKey(KeyCode.Joystick1Button0) ||
                  Input.GetKey(KeyCode.Joystick1Button1) ||
                  Input.GetKey(KeyCode.Joystick1Button2) ||
                  Input.GetKey(KeyCode.Joystick1Button3) ||
                  Input.GetKey(KeyCode.Joystick1Button4) ||
                  Input.GetKey(KeyCode.Joystick1Button5) ||
                  Input.GetKey(KeyCode.Joystick1Button6) ||
                  Input.GetKey(KeyCode.Joystick1Button7) ||
                  Input.GetKey(KeyCode.Joystick1Button8) ||
                  Input.GetKey(KeyCode.Joystick1Button9) ||
                  Input.GetKey(KeyCode.Joystick1Button10) ||
                  Input.GetKey(KeyCode.Joystick1Button11) ||
                  Input.GetKey(KeyCode.Joystick1Button12) ||
                  Input.GetKey(KeyCode.Joystick1Button13) ||
                  Input.GetKey(KeyCode.Joystick1Button14) ||
                  Input.GetKey(KeyCode.Joystick1Button15) ||
                  Input.GetKey(KeyCode.Joystick1Button16) ||
                  Input.GetKey(KeyCode.Joystick1Button17) ||
                  Input.GetKey(KeyCode.Joystick1Button18) ||
                  Input.GetKey(KeyCode.Joystick1Button19))
        {
            inputState = InputState.JOYSTICK;
        }
    }



    IEnumerator activeSet ( )
    {
        while ( true )
        {

            myLastPlayer = currentPlayer;

            float h = Input.GetAxis("Switch_Horizontal");
            float v = Input.GetAxis("Switch_Vertical");

            int switchIndex = (h > 0 ? 1 :
                              (h < 0 ? 3 :
                              (v > 0 ? 0 :
                              (v < 0 ? 2 : -1))));




            if (switchIndex != -1 && players[switchIndex])
            {
                currentPlayer = players[switchIndex];
                yield return null;
            }

            if (Input.GetButtonDown("Toggle"))
            {
                for (int i = 0, next = 0; i < players.Length; i++)
                {
                    if (currentPlayer == players[i])
                    {
                        next = i;
                        do
                        {
                            next = (next + 1) % players.Length;
                        } while (!players[next] && next != i);

                        currentPlayer = players[next];
                        yield return null;
                        break;
                    }
                }
            }

            foreach (Player player in players)
            {
                if (player != null)
                {
                    bool isCurrentPlayer = (player == currentPlayer);
                    player.gameObject.SetActive(isCurrentPlayer);
                    if (isCurrentPlayer && currentPlayer != myLastPlayer)
                    {
                        currentPlayer.transform.eulerAngles = myLastPlayer.transform.eulerAngles;
                        currentPlayer.transform.position = myLastPlayer.transform.position;
                    }
                }
            }
            yield return null;
        }
    }
}
