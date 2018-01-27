using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhereIClick : MonoBehaviour {

    Ray myRay;      // initializiere den ray
    RaycastHit hit; // initializiere den raycasthit
    public GameObject objectToinstantiate;  //Object das gespawned wird
    int layer_mask;       //Welcher Layer nur getroffen werden soll
    float objectHeight;

    public MushroomManager mushMan;

    void Start () {
        layer_mask = LayerMask.GetMask("Floor");
        objectHeight = objectToinstantiate.transform.localScale.y/2;
	}
	
	// Update is called once per frame
	void Update () {
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);      // von wo der Raycast starten soll

        if (Physics.Raycast(myRay, out hit, layer_mask))                            // wenn der ray was trifft werden die infos des treffers in hit gespeichert
        {
            if (Input.GetMouseButtonDown(0))                            // what to do if i press the left mouse button
            {
                Vector3 hitPos = new Vector3(hit.point.x, objectHeight, hit.point.z);     //always spawn on height of the floor + half height of the object
                mushMan.addMushroom(Instantiate(objectToinstantiate, hitPos, Quaternion.identity).GetComponent<Mushroom>()); // instatiate a prefab on the position where the ray hits the floor.
                Debug.Log(hit.point);                                   // debugs the vector3 of the position where I clicked
            }
        }   
    }                                                                   
}
