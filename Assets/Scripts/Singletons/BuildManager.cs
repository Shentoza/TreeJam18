using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Manager", true)]
public class BuildManager : Singleton<BuildManager>
{

    public GameObject mushroomPrefab;
    float mushroomCost = 5.0f;
    float objectHeight;

    int floorMask;

    // Use this for initialization
    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        objectHeight = mushroomPrefab.transform.localScale.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);      // von wo der Raycast starten soll
            RaycastHit hit;
            if (Physics.Raycast(myRay, out hit, floorMask))                            // wenn der ray was trifft werden die infos des treffers in hit gespeichert
            {
                Build(new Vector3(hit.point.x, objectHeight, hit.point.z));
            }
        }
    }

    void Build(Vector3 pos)
    {
        if (ResourceManager.Instance.reduce_spore(mushroomCost))
        {
            MushroomManager.Instance.addMushroom(Instantiate(mushroomPrefab, pos,
                Quaternion.identity).GetComponent<Mushroom>());
            return;
        }
        else
        {
            //Fehlersound
        }

    }
}
