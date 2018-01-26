using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour {


    public static GameTime Instance;
    [SerializeField]
    float timeScale = 1.0f;

    float secondsPassed;
    int minutesPassed;
	// Use this for initialization
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        secondsPassed = 0.0f;
        minutesPassed = 0;
        Instance = this;
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
        }

	}

    void Destroy()
    {
        Instance = null;
    }
}
