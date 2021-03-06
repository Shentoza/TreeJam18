﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Manager", true)]
public class BuildManager : Singleton<BuildManager>
{

    [SerializeField]
    GameObject[] mushroomPrefabs;

    private int layer_mask, blocked_mask, buildable_mask;

    private bool inBuildMode = true;

    private int selectedShroom = 0;

    private GameObject audioObject;

    private AudioClip buildClip;

    private AudioClip missingClip;


   

    // Use this for initialization
    void Start()
    {
        layer_mask = LayerMask.GetMask("Floor");
        blocked_mask = LayerMask.GetMask("Blocked");
        buildable_mask = LayerMask.GetMask("Buildable");
        audioObject = new GameObject();
        audioObject.AddComponent<AudioSource>();
        buildClip = Resources.Load<AudioClip>("Sounds/planting") as AudioClip;
        missingClip = Resources.Load<AudioClip>("Sounds/missing") as AudioClip;


    }

    // Update is called once per frame
    void Update()
    {
        if (inBuildMode && Input.GetMouseButtonDown(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);      // von wo der Raycast starten soll
            RaycastHit hit;
            if (Physics.Raycast(myRay, out hit, Mathf.Infinity, layer_mask) &&
                !Physics.Raycast(myRay, Mathf.Infinity, blocked_mask) &&
                Physics.Raycast(myRay, Mathf.Infinity, buildable_mask))                            // wenn der ray was trifft werden die infos des treffers in hit gespeichert
            {
                Build(selectedShroom, hit.point);
            }
            else
            {
                Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                audioObject.transform.position = clickPos;
                audioObject.GetComponent<AudioSource>().PlayOneShot(missingClip);
            }
        }
    }

    void Build(int index, Vector3 pos)
    {
        float rng = Random.value;
        if (rng < 0.3f)
        {
            index = 0;
        }
        else if (rng < 0.6f)
        {
            index = 1;
        }
        else
        {
            index = 2;
        }
        GameObject m = mushroomPrefabs[index];
        int cost = m.GetComponent<Mushroom>().Cost;
        if (ResourceManager.Instance.reduce_spore(cost))
        {
            GameObject temp = Instantiate(m, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));

            audioObject.transform.position = temp.transform.position;
            audioObject.GetComponent<AudioSource>().PlayOneShot(buildClip);


            return;
        }
        else
        {
           Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           audioObject.transform.position = clickPos;
           audioObject.GetComponent<AudioSource>().PlayOneShot(missingClip);
           
        }

    }
}
