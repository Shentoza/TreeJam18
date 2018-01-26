using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager Instance;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Destroy()
    {
        Instance = null;
    }

    public delegate void SeedChange(int oldValue, int newValue);
    public static event SeedChange OnSeedChange;
    public void SendSeedChange(int oldValue, int newValue)
    {
        OnSeedChange(oldValue, newValue);
    }

    public delegate void SecondPassed();
    public static event SecondPassed OnSecondPassed;
    public void SendSecondPassed()
    {
        OnSecondPassed();
    }

    public delegate void MinutePassed();
    public static event MinutePassed OnMinutePassed;
    public void SendMinutePassed()
    {
        OnMinutePassed();
    }
}
