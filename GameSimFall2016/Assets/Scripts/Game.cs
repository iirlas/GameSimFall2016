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
	void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach ( GameObject player in players )
        {
            switch (player.name)
            {
            case "Girl":
                this.players[0] = player.GetComponent<Player>();
                break;
            case "Bird":
                this.players[1] = player.GetComponent<Player>();
                break;
            case "Rabbit":
                this.players[2] = player.GetComponent<Player>();
                break;
            case "Cat":
                this.players[3] = player.GetComponent<Player>();
                break;
            }
        }
        currentPlayer = this.players[0];
	}
	
	// Update is called once per frame
	void Update () {
        Player lastPlayer = currentPlayer;

        float h = Input.GetAxis("Switch_Horizontal");
        float v = Input.GetAxis("Switch_Vertical");

        int switchIndex = ( h > 0 ? 1 : 
                          ( h < 0 ? 3 :
                          ( v > 0 ? 0 : 
                          ( v < 0 ? 2 : -1 ))));




        if ( switchIndex != -1 )
        {
            currentPlayer = players[switchIndex];
        }

        if (Input.GetButtonDown("Toggle"))
        {
            for (int i = 0, next = 0; i < players.Length; i++)
            {
                if (currentPlayer == players[i])
                {
                    do 
                    {
                        next = (i + 1) % players.Length;
                    }
                    while (!players[next] && next != i);

                    currentPlayer = (players[next]);
                    break;
                }
            }
        }
        foreach ( Player player in players )
        {
            if ( !!player )
            {
                bool isPlayer = (player == currentPlayer);
                player.gameObject.SetActive(isPlayer);
                if ( isPlayer && currentPlayer != lastPlayer )
                {
                    currentPlayer.transform.eulerAngles = lastPlayer.transform.eulerAngles;
                    currentPlayer.transform.position = lastPlayer.transform.position;
                }
            }
        }
	}
}
