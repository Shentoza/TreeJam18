﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Manager", true)]
public class EventManager : Singleton<EventManager> {
    public delegate void SporeChange(float oldValue, float newValue);
    public static event SporeChange OnSporeChange;
    public void SendSporeChange(float oldValue, float newValue)
    {
        if(OnSporeChange != null)
            OnSporeChange(oldValue, newValue);
    }

    public delegate void AlterTreeCount(int change);
    public static event AlterTreeCount OnTreeCountChange;
    public void SendTreeCountChange(int change)
    {
        if (OnTreeCountChange != null)
        {
            OnTreeCountChange(change);
        }
    }

    public delegate void MaxTreeCountChange(int value);
    public static event MaxTreeCountChange OnMaxTreeCountChange;
    public void SendMaxTreeCountChange(int value)
    {
        if (OnMaxTreeCountChange != null)
        {
            OnMaxTreeCountChange(value);
        }
    }

    public delegate void TreeInfectionComplete(ShroomTree tree);
    public static event TreeInfectionComplete OnTreeInfectionComplete;
    public void SendTreeInfectionComplete(ShroomTree tree)
    {
        if(OnTreeInfectionComplete != null)
            OnTreeInfectionComplete(tree);
    }

    public delegate void SecondPassed();
    public static event SecondPassed OnSecondPassed;
    public void SendSecondPassed()
    {
        if(OnSecondPassed != null)
            OnSecondPassed();
    }

    public delegate void MinutePassed();
    public static event MinutePassed OnMinutePassed;
    public void SendMinutePassed()
    {
        if(OnMinutePassed != null)
            OnMinutePassed();
    }
}
