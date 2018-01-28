using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/GameTime", true)]
public class GameTime : Singleton<GameTime> {

    public float timeScale = 1.0f;

    float secondsPassed;
    int minutesPassed;
	// Use this for initialization
    void Start()
    {
        secondsPassed = 0.0f;
        minutesPassed = 0;
    }
	
	// Update is called once per frame
	void Update () {
        secondsPassed += Time.deltaTime * timeScale;

        if(secondsPassed >= 1.0f)
        {
            EventManager.Instance.SendSecondPassed();
        }

        if(secondsPassed >= 60.0f)
        {
            EventManager.Instance.SendMinutePassed();
            minutesPassed++;
            secondsPassed -= 60.0f;
        }
	}
}
