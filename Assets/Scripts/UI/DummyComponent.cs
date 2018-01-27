using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyComponent : MonoBehaviour {


    private float spore;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 

        if(Input.GetKeyDown("w"))
        {
            float newSpore = spore + 100;
            setSpore(spore, newSpore);
        }
        if (Input.GetKeyDown("s"))
        {
            float newSpore = spore - 100;
            setSpore(spore, newSpore);
        }
        if (Input.GetKeyDown("e"))
        {
            float newSpore = spore + 0.5f;
            setSpore(spore, newSpore);
        }
    }

    public void setSpore(float oldValue, float newValue)
    {
        spore = newValue;
        EventManager.Instance.SendSporeChange(oldValue, newValue);
    }
}
