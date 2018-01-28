using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("Prefabs/UI", true)]
public class UIManager : Singleton<UIManager> {

    [SerializeField]
    Text sporeScore;
    float sporeIncrements = 1;
    float currentSporeValue, newSporeValue;

    int maxTrees;
    int currentInfectedTrees;
    float treeRatio;
    [SerializeField]
    RectTransform infectionRatioRect;

    //Tree ToolTip
    ShroomTree currentTree;
    [SerializeField]
    GameObject tooltipPanel;

    [SerializeField]
    RectTransform treeHealthbar;
    float oldHealthValue, newHealthValue;
    [SerializeField]
    Vector3 deathPosition;
    [SerializeField]
    Vector3 healthyPosition;


    float oldInfestation, newInfestation;
    [SerializeField]
    Texture2D healthyLeaf;
    [SerializeField]
    Texture2D sickLeaf;
    [SerializeField]
    List<RawImage> leafes;
    public bool tooltipShowing { private set; get; }

    // Use this for initialization
    void Start () {
        EventManager.OnSporeChange += sporeValueChanged;
        EventManager.OnTreeCountChange += treeValueChanged;
        EventManager.OnMaxTreeCountChange += maxTreesChanged;
        currentSporeValue = newSporeValue = 0;
        tooltipPanel.SetActive(false);
        tooltipShowing = false;
    }

    void Update()
    {
        if(Mathf.Abs(currentSporeValue - newSporeValue) >= 1.0f)
        {
            if (newSporeValue > currentSporeValue)
            {
                currentSporeValue = Mathf.Clamp(currentSporeValue + sporeIncrements, currentSporeValue, newSporeValue);
            }
            else
            {
                currentSporeValue = Mathf.Clamp(currentSporeValue - sporeIncrements, newSporeValue, currentSporeValue);
            }
            sporeScore.text = ""+currentSporeValue;
        }
    }
	
	void Destroy()
    {
        EventManager.OnSporeChange -= sporeValueChanged;
        EventManager.OnTreeCountChange -= treeValueChanged;
        EventManager.OnMaxTreeCountChange -= maxTreesChanged;
    }

    void OnGUI()
    {
        if (tooltipShowing)
        {
            Vector2 mousePosLocked = Event.current.mousePosition;
            Vector2 halfBounds = tooltipPanel.GetComponent<RectTransform>().rect.size;
            halfBounds += new Vector2(10, 10);
            halfBounds *= 0.5f;
            mousePosLocked.x = Mathf.Clamp(mousePosLocked.x + halfBounds.x, halfBounds.x, Camera.main.pixelWidth - halfBounds.x);
            mousePosLocked.y = Mathf.Clamp(Camera.main.pixelHeight - (mousePosLocked.y + halfBounds.y), halfBounds.y, Camera.main.pixelHeight- halfBounds.y);
            Vector3 mouse = new Vector3(mousePosLocked.x, mousePosLocked.y, 0);
            tooltipPanel.transform.position = mouse;

            if (currentTree)
            {
                newHealthValue = currentTree.getHP() / 100.0f;
                if(newHealthValue != oldHealthValue)
                {
                    StopCoroutine("lerpHealthbar");
                    StartCoroutine(lerpHealthbar(oldHealthValue, newHealthValue, 1.0f));
                    oldHealthValue = newHealthValue;
                }

                newInfestation = currentTree.getIntegrity() * leafes.Count;
                if ((int)oldInfestation != (int)newInfestation)
                    paintLeaves();
                oldInfestation = newInfestation;

            }
            
        }
    }

    void sporeValueChanged(float oldValue, float newValue)
    { 
        this.newSporeValue = newValue;
    }

    void treeValueChanged(int change)
    {
        currentInfectedTrees += change;
        UpdateInfectionUI();
    }

    void maxTreesChanged(int value)
    {
        maxTrees = value;
        UpdateInfectionUI();
    }

    void UpdateInfectionUI()
    {
        treeRatio = (float)currentInfectedTrees / (float)maxTrees;
        infectionRatioRect.localScale = new Vector3(treeRatio, infectionRatioRect.localScale.y, infectionRatioRect.localScale.z);
    }

    public void showTooltip(ShroomTree tree)
    {
        currentTree = tree;
        oldHealthValue = newHealthValue = currentTree.getHP();
        tooltipPanel.SetActive(true);
        tooltipShowing = true;
    }

    public void hideTooltip()
    {
        tooltipShowing = false;
        tooltipPanel.SetActive(false);
    }

    void paintLeaves()
    {
        for(int i = 0; i< leafes.Count; ++i)
        {
            leafes[i].texture = (int)newInfestation <= i ? healthyLeaf : sickLeaf;
        }
    }

    IEnumerator lerpHealthbar(float oldProgress, float progress, float timeNeeded)
    {
        float alpha = 0f;
        while(alpha < 1.0f)
        {
            float currentProgress = Mathf.Lerp(oldProgress, progress, alpha);
            treeHealthbar.localPosition = Vector3.Lerp(deathPosition, healthyPosition, Mathf.SmoothStep(oldProgress, progress, alpha));
            alpha += Time.deltaTime / timeNeeded;
            yield return null;
        }
        yield break;
    }
}
