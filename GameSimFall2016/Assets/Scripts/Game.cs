using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public enum GameState
    {
        CUTSCENE,
        PAUSE,
        PLAY,
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

    public static Game getInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Game>();
            if (instance == null)
            {
                GameObject singleton = new GameObject("Game");
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

        StartCoroutine(activeSet());
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void OnGUI()
    {
        //set current  input state
        if (Event.current.isKey || Event.current.isMouse)
        {
            inputState = InputState.KEYBOARD;
        }
        else
        {
            bool isJoystick = false;
            for ( KeyCode key = KeyCode.JoystickButton0; key != KeyCode.JoystickButton19 && !isJoystick; key++ )
            {
                isJoystick |= Input.GetKey(key);
            }


            if ( isJoystick )
            {
                inputState = InputState.JOYSTICK;

            }
        }
    }

    IEnumerator activeSet ()
    {
        while ( true )
        {
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

            if ( currentPlayer == players[0] )
            {
                for ( int index = 0; index < players.Length; index++ )
                {
                    Player player = players[index];
                    if (player != null && currentPlayer != player &&
                        Vector3.Distance(player.transform.position, currentPlayer.transform.position) < 5.0f)
                    {
                        Player target = null;
                        for (int j = index, breakCount = 0; target == null && breakCount < players.Length; 
                            j = (--j >= 0 ? j : j + players.Length), breakCount++ )
                        {
                            if (players[j] != null && players[j] != player && 
                                Vector3.Distance(players[j].transform.position, currentPlayer.transform.position) < 5.0f)
                            {
                                target = players[j];
                            }
                        }

                        if ( target != null )
                        {
                            player.smoothRotateTowards(target.transform.eulerAngles, Time.deltaTime * player.rotationSmoothSpeed);
                            player.transform.position = Vector3.MoveTowards(player.transform.position, 
                                                                            target.transform.position - 
                                                                                (player.transform.forward), 
                                                                            player.movementSpeed * Time.deltaTime   );
                        }
                    }
                }
            }
            yield return null;
        }
    }
}
