using UnityEngine;
using System.Collections;

public class Timer
{

    float myStartTime = -1;

    public static bool operator true ( Timer timer )
    {
        return (timer.isRunning());
    }

    public static bool operator false ( Timer timer )
    {
        return !(timer.isRunning());
    }

    public static bool operator! ( Timer timer )
    {
        return !(timer.isRunning());
    }

    public bool start ()
    {
        if ( myStartTime == -1 )
        {
            myStartTime = Time.time;
            return true;
        }
        return false;
    }

    public float stop ()
    {
        if ( myStartTime != -1 )
        {
            float elapsedTime = Time.time - myStartTime;
            myStartTime = -1;
            return elapsedTime;
        }
        return -1;
    }

    public float reset ()
    {
        float elapsedTime = stop();
        start();
        return elapsedTime;
    }

    public float elapsedTime ()
    {
        if (myStartTime != -1)
        {
            return Time.time - myStartTime;
        }
        return -1;
    }

    public bool isRunning ()
    {
        return myStartTime != -1;
    }
}
