using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : Singleton<LevelManager> {

    public Object start;
    public Object end;
    public int midLevelCount = 1;
    public Object[] midLevels;

	// Use this for initialization
    override protected void Init()
    {
        //Randomize levels
        System.Random randomizer = new System.Random();
        List<Object> levels = new List<Object>();

        List<Object> midLevelList = midLevels.OrderBy(index => randomizer.Next()).ToList();
        midLevelList.RemoveRange( midLevelCount, midLevels.Length - midLevelCount );
        midLevels = midLevelList.ToArray();

        levels.Add(start);
        levels.AddRange(midLevels);
        levels.Add(end);

        setup(levels.ToArray());
    }

    void setup ( Object[] levels )
    {
        Transform prevExitDoor = null;
        for (int index = 0; index < levels.Length; index++ )
        {
            GameObject level = null;
            if ( !GameObject.Find( levels[index].name ) )
            {
                level = Instantiate(levels[index]) as GameObject;
            }
            else
            {
                level = levels[index] as GameObject;
            }
            Transform entranceDoor = level.GetChildComponentWithTag<Transform>("Entrance");
            Transform exitDoor = level.GetChildComponentWithTag<Transform>("Exit");

            if (prevExitDoor != null)
            {
                Transform prevTransform = prevExitDoor.parent.transform;
                float angleAlignment = 90;
                float entranceDoorAngle = Mathf.Round(Extension.angle(entranceDoor.localPosition.z, entranceDoor.localPosition.x) / angleAlignment) * angleAlignment;
                float exitDoorAngle = Mathf.Round(Extension.angle(prevExitDoor.localPosition.z, prevExitDoor.localPosition.x) / angleAlignment) * angleAlignment;
                float angle = (exitDoorAngle + 180) - entranceDoorAngle;
                level.transform.rotation = prevTransform.rotation * Quaternion.Euler(0, -angle, 0);
                level.transform.position = prevExitDoor.position - (entranceDoor.position - level.transform.position);
            }
            else
            {
                //level.transform.position = transform.position;
                //level.transform.rotation = transform.rotation;

            }
            prevExitDoor = exitDoor;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
