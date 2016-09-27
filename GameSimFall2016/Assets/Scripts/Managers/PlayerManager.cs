using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : Singleton<PlayerManager> {

    new public Camera camera;
    public float followDistance = 2.0f;
    public Player currentPlayer { get; private set; }
    public Player[] players { get; private set; }

    // Use this for initialization
    override protected void Init ()
    {
        players = GameObject.FindObjectsOfType<Player>();
        currentPlayer = players.First(player => { return (player as Girl) != null; });
        if ( currentPlayer == null )
        {
            currentPlayer = players.First(player => { return player != null; });
        }
    }

    void Update ()
    {
        if (Input.GetButtonDown("Toggle"))
        {
            int index = System.Array.IndexOf<Player>(players, currentPlayer);
            index = (++index) % players.Length;
            currentPlayer = players[index];
        }

        if (currentPlayer is Girl)
        {
            for (int index = 0; index < players.Length; index++)
            {
                Player player = players[index];
                if (player != null && currentPlayer != player &&
                    Vector3.Distance(player.transform.position, currentPlayer.transform.position) < 5.0f)
                {
                    Player target = null;
                    for (int j = index, breakCount = 0; target == null && breakCount < players.Length;
                            j = (--j >= 0 ? j : j + players.Length), breakCount++)
                    {
                        if (players[j] != null && players[j] != player &&
                            Vector3.Distance(players[j].transform.position, currentPlayer.transform.position) < 5.0f)
                        {
                            target = players[j];
                        }
                    }

                    if (target != null)
                    {
                    player.smoothRotateTowards(target.transform.eulerAngles, Time.deltaTime * player.rotationSmoothSpeed);
                    player.transform.position = Vector3.MoveTowards(player.transform.position,
                                                                    target.transform.position - (player.transform.forward * followDistance),
                                                                    currentPlayer.movementSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
