using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager> {

    new public Camera camera;
    public Player currentPlayer { get; private set; }
    public Player[] players { get; private set; }
    public Dictionary<string, Item> items;

    // Use this for initialization
    override protected void Init ()
    {
        players = new Player[4];
        items = new Dictionary<string, Item>();

        players[0] = GameObject.FindObjectOfType<Girl>();
        players[1] = GameObject.FindObjectOfType<Bird>();
        players[2] = GameObject.FindObjectOfType<Rabbit>();
        players[3] = GameObject.FindObjectOfType<Cat>();

        foreach (Player player in players)
        {
            if( player != null )
            {
                currentPlayer = player;
                break;
            }
        }



        StartCoroutine(activeSet());
    }

    // Update is called once per frame
    void Update ()
    {
    }

    IEnumerator activeSet ()
    {
        while (true)
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

            if (currentPlayer == players[0])
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
                                                                            target.transform.position - (player.transform.forward),
                                                                            player.movementSpeed * Time.deltaTime);
                        }
                    }
                }
            }
            yield return null;
        }
    }
}

public class Item
{
	// holy shit delete this
}