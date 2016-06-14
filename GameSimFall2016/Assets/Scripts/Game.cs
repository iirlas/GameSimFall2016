using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public enum State
    {
        FREEROAM,
        STRAFE,
        PUZZLE,
        CUTSCENE,
        PAUSE,
    }

    static private Game instance;

    public State state;
    [HideInInspector]
    public Player currentPlayer = null;
    public Player[] players = new Player[4];

    private Player lastPlayer;

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
        lastPlayer = currentPlayer;

        StartCoroutine(activeSet());
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    IEnumerator activeSet ( )
    {
        while ( true )
        {

            lastPlayer = currentPlayer;

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
                    if (isCurrentPlayer && currentPlayer != lastPlayer)
                    {
                        currentPlayer.transform.eulerAngles = lastPlayer.transform.eulerAngles;
                        currentPlayer.transform.position = lastPlayer.transform.position;
                    }
                }
            }
            yield return null;
        }
    }
}
