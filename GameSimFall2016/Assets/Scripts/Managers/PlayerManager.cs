using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : Singleton<PlayerManager> {
    //public enum SelectedPLayer { Bird, Girl, Rabbit }
    new public Camera camera;
    public float followDistance = 1.0f;
    public float switchDistance = 20.0f;
    public Player currentPlayer { get; set; }
    public Player[] players { get; private set; }
    //public SelectedPLayer sel = SelectedPLayer.Girl;

    // Use this for initialization
    override protected void Init ()
    {
        findPlayers();
    }

    private void FollowPlayer()
    {
        //for (int index = 0; index < players.Length; index++)
        //{
            //Player player = players[index];
            //if ( currentPlayer != player &&
            //    Vector3.Distance(player.transform.position, currentPlayer.transform.position) < 5.0f)
            //{
                //Player target = null;
                for (int j = 0; j < players.Length; j++ )
                {

                    //        j = (--j >= 0 ? j : j + players.Length), breakCount++)
                    //{
                    //    if (players[j] != null && players[j] != player &&
                    //        Vector3.Distance(players[j].transform.position, currentPlayer.transform.position) < 5.0f)
                    //    {
                    //        target = players[j];
                    //    }
                    //}
                    if (!(currentPlayer is Girl))
                    {
                        players[j].AssignFollower();
                        continue;
                    }

                    float minDist = switchDistance;
                    //bad practice probably
                    //triangle problem
                    players[j].AssignFollower(players.FirstOrDefault((possibleFollower) =>
                        {
                           float distcheck = Vector3.Distance(players[j].transform.position, possibleFollower.transform.position);
                           if (//distcheck < minDist && //in range
                              possibleFollower != players[j] && //not us
                              !(possibleFollower is Girl) && //is not the girl
                              //possibleFollower != currentPlayer && //the the current player
                              possibleFollower.follower != players[j])//we are not their follower
                           {
                              return true;
                           }
                           return false;
                        }
                    ));
                }

        //            // distance check between other playrs
 
        //            for (int m = 0; m < players.Length; m++)
        //            {
        //                if (players[j] != players[m])
        //                {

        //                }
        //            }

        //            if (!players[j] is Girl)
        //            {
        //                players[j].smoothRotateTowards(players[minItem].transform.eulerAngles, Time.deltaTime * players[j].rotationSmoothSpeed);

        //                players[j].transform.position = Vector3.MoveTowards(players[j].transform.position,
        //                                                                players[minItem].transform.position - (players[j].transform.forward * followDistance),
        //                                                                currentPlayer.movementSpeed * Time.deltaTime);
        //            }
        //        }
        //    //}
        ////}
    }

    public void findPlayers()
    {
        players = GameObject.FindObjectsOfType<Player>();
        if (players.Length > 0 && currentPlayer == null)
        {
            if (players.Length > 1)
            {
                currentPlayer = players.First(player => { return player != null && player is Girl; });
            }
            if (currentPlayer == null)
            {
                currentPlayer = players.First(player => { return player != null; });
            }
            ignoreCollision();
        }
    }

    void OnDrawGizmosSelected()
    {
        if( currentPlayer != null )
            Gizmos.DrawWireSphere(currentPlayer.transform.position, switchDistance);
    }

    void Update ()
    {
        if (Input.GetButtonDown("Toggle"))
        {
            int index = System.Array.IndexOf<Player>(players, currentPlayer);
            float dist = 0;
            do
            {
                index = (++index) % players.Length;
                dist = Vector3.Distance(players[index].transform.position, currentPlayer.transform.position);
            }
            while (dist >= switchDistance && !(players[index] is Girl));
            currentPlayer = players[index];
            
            //while (Vector3.Distance(players[index].transform.position, currentPlayer.transform.position) > switchDistance);
            //currentPlayer = players[index];
            //ignoreCollision();
        }
        //switch (sel)
        //{
        //    case SelectedPLayer.Girl: break;
        //}

        FollowPlayer();
        if (currentPlayer is Girl)
        {
            //for (int index = 0; index < players.Length; index++)
            //{
            //    Player player = players[index];
            //    if (player != null && currentPlayer != player &&
            //        Vector3.Distance(player.transform.position, currentPlayer.transform.position) < 5.0f)
            //    {
            //        //Player target = null;
            //        for (int j = index, breakCount = 0; target == null && breakCount < players.Length;
            //                j = (--j >= 0 ? j : j + players.Length), breakCount++)
            //        {
            //            if (players[j] != null && players[j] != player &&
            //                Vector3.Distance(players[j].transform.position, currentPlayer.transform.position) < 5.0f)
            //            {
            //                target = players[j];
            //            }
            //        }

            //        if (target != null)
            //        {
            //        player.smoothRotateTowards(target.transform.eulerAngles, Time.deltaTime * player.rotationSmoothSpeed);
            //        player.transform.position = Vector3.MoveTowards(player.transform.position,
            //                                                        target.transform.position - (player.transform.forward * followDistance),
            //                                                        currentPlayer.movementSpeed * Time.deltaTime);
            //        }
            //    }
            //}
        }
    }

    void ignoreCollision ()
    {
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < players.Length; j++)
            {
                if ( players[i] != players[j] )
                {
                    Physics.IgnoreCollision(players[i].collider, players[j].collider);
                }
            }
        }
    }
}
