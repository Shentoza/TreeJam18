using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/GameTime", true)]
public class GameTime : Singleton<GameTime>
{

    [SerializeField]
    float timeScale = 1.0f;

    [SerializeField]
    float secondsPassed,
        lastSecondsPassed;
    int minutesPassed;
    // Use this for initialization
    void Start()
    {
        secondsPassed = 0.0f;
        lastSecondsPassed = 0.0f;
        minutesPassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime * timeScale;
        if (secondsPassed - lastSecondsPassed >= 1.0f)
        {
            EventManager.Instance.SendSecondPassed();
            ++lastSecondsPassed;
        }

        if (secondsPassed >= 60.0f)
        {
            EventManager.Instance.SendMinutePassed();
            secondsPassed -= 60f;
            lastSecondsPassed = 0.0f;
            minutesPassed++;
        }

    }
}
