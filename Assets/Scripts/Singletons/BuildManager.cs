using System.Collections;
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


   

    // Use this for initialization
    void Start()
    {
        layer_mask = LayerMask.GetMask("Floor");
        blocked_mask = LayerMask.GetMask("Blocked");
        buildable_mask = LayerMask.GetMask("Buildable");
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
        }
    }

    void Build(int index, Vector3 pos)
    {
        GameObject m = mushroomPrefabs[index];
        int cost = m.GetComponent<Mushroom>().Cost;
        if (ResourceManager.Instance.reduce_spore(cost))
        {
            GameObject temp = Instantiate(m, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            AudioClip clip = Resources.Load<AudioClip>("planting");
            
            temp.AddComponent<AudioSource>().PlayOneShot(clip);
            

            return;
        }
        else
        {
            //Fehlersound
        }

    }
}
