using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public enum State
    {
        FREEROAM,
        PUZZLE,
        CUTSCENE,
        PAUSE,
    }

    static private Game instance;

    public State state;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
