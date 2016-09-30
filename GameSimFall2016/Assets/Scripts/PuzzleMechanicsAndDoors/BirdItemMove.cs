using UnityEngine;
using System.Collections;

public class BirdItemMove : MonoBehaviour {

    Bird kira;
    new public Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        kira = GameObject.FindObjectOfType<Bird>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Action") && kira.playerState.Equals(Player.State.DEFAULT) &&
            Vector3.Distance(kira.rigidbody.position, rigidbody.position) < 2)
        {
            kira.addForeignRunnable(move);
            kira.playerState = Player.State.FOREIGN;
            kira.rigidbody.position = transform.position + Vector3.up;
        }
    }

    public void move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 cameraFoward = PlayerManager.getInstance().camera.transform.forward;
        float angle = (Mathf.Atan2(cameraFoward.x, cameraFoward.z)) * Mathf.Rad2Deg;
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(h, 0, v);

        direction *= Time.deltaTime * kira.movementSpeed;

        rigidbody.position += direction;
        kira.rigidbody.position += direction;

        if (!Input.GetButton("Action") || Vector3.Distance(kira.rigidbody.position, rigidbody.position) > 2)
        {
            kira.playerState = Player.State.DEFAULT;
            kira.clearForeignRunnable();
        }
    }
}
