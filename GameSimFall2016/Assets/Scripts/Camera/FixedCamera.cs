using UnityEngine;
using System.Collections;

public class FixedCamera : MonoBehaviour {

    public Transform viewPoint;

    [HideInInspector]
    public new Transform transform;

    private Player myPlayer;

    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.getInstance().currentPlayer)
            myPlayer = PlayerManager.getInstance().currentPlayer;
    }

    public void LateUpdate()
    {
        if (!myPlayer)
            return;

        Vector3 target = viewPoint.position;


        if (true)//myPlayer is Girl && myPlayer.playerState.Equals(Girl.State.PUZZLE))
        {
            target = myPlayer.transform.position; 
        }

        transform.LookAt(target, Vector3.up);
    }
}
