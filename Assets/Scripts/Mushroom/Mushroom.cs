﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Node
{
    public float range;

    static int sporeCost = 5,
        blockedMask;

    [SerializeField]
    int upgradeLevel,
        currentUpgradeTime = 0,
        upgradeTime = 30;

    public int Cost
    {
        get
        {
            return sporeCost;
        }
    }
    public List<Mushroom> mushroomNeighbors;

    //Baeume in Naehrstoffreichweite
    public List<ShroomTree> treeNeighbors;

    public GameObject[] smallMushrooms;

    // Use this for initialization
    void Start()
    {
        blockedMask = LayerMask.GetMask("Blocked");
        mushroomNeighbors = new List<Mushroom>();
        initalizeNeighbour();
        NodeManager.Instance.add_Nodes(this);
        EventManager.OnSecondPassed += upgradeTimer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) //for debug
            spawnSmallMushroom();
    }

    public void deleteTree(ShroomTree s)
    {
        treeNeighbors.Remove(s);
        if (treeNeighbors.Count == 0)
            die();
    }

    public void die()
    {
        foreach (ShroomTree s in treeNeighbors)
        {
            s.remove_Node(this);
            s.removeIntersectedShroom(this);
        }
        remove_Node(this);
        Destroy(this.gameObject);
    }

    void upgradeTimer()
    {
        if (++currentUpgradeTime == upgradeTime)
        {
            currentUpgradeTime = 0;
            spawnSmallMushroom();
        }
    }

    public void spawnSmallMushroom()
    {
        if (upgradeLevel++ < 5)
        {
            Vector3 randomPos;
            GameObject newShroom;
            bool placed = false;
            int i = 0;
            Ray ray;
            setRange(++range);
            while (!placed && i++ < 10)
            {
                randomPos = transform.position + new Vector3(UnityEngine.Random.Range(-range + 1, range - 1),
                    0.0f, UnityEngine.Random.Range(-range + 1, range - 1)); // This places in a rectangle around the shroom, shold be circle
                ray = new Ray(randomPos + new Vector3(0f, 100f, 0f), Vector3.down);
                if (!Physics.Raycast(ray, Mathf.Infinity, blockedMask))
                {
                    if (UnityEngine.Random.value > 0.5f)
                    {
                        newShroom = Instantiate(smallMushrooms[0], transform);
                    }
                    else
                    {
                        newShroom = Instantiate(smallMushrooms[1], transform);
                    }

                    newShroom.transform.SetPositionAndRotation(randomPos,
                        Quaternion.Euler(0, UnityEngine.Random.Range(0.0f, 360.0f), 0));
                    placed = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (!c.isTrigger)
        {
            if (c.gameObject.GetComponent<ShroomTree>())
            {
                treeNeighbors.Add(c.gameObject.GetComponent<ShroomTree>());
                ResourceManager.Instance.add_Tree(c.gameObject.GetComponent<ShroomTree>());
                add_Node(c.gameObject.GetComponent<ShroomTree>());

            }
            else if (c.gameObject.GetComponent<Mushroom>())
            {
                mushroomNeighbors.Add(c.gameObject.GetComponent<Mushroom>());
                add_Node(c.gameObject.GetComponent<Mushroom>());
            }
        }
    }

    void setRange(float r)
    {
        range = r;
        transform.GetChild(0).localScale = new Vector3(r, r, r);
        transform.GetChild(1).localScale = new Vector3(r, r, r);
        transform.GetChild(2).localScale = transform.GetChild(2).localScale + new Vector3(0.2f, 0.2f, 0.2f);
    }
}
