using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
     public GameObject trunkObj;

    public int timeTochange;
    public int maxTime;
    public Material blueMat;
    public Renderer rend;
    public ActivateObject obj;
    public bool Complete = false;
    TowerManager manager;
    public WarpDoor warpDoor;
    public ParticleSystem warpDoorParticle;

    public GameObject activeObj;

    public TutorialMarker marker;

    public GameObject completeParticle;
    public GameObject wrongParticle;
    public GameObject transitionParticle;

    public AudioClip wrongSound;
    public AudioClip rightSound;

    public AudioClip redToBlueSound;
    bool enable = true;
    void Start()
    {
        manager = GetComponentInParent<TowerManager>();
        timeTochange = Random.Range(1, maxTime);
    }

    public void Perform()
    {
        if (!enable) return;
        if (Complete) return;
        timeTochange -= 1;

        timeTochange = 0;
        if (timeTochange == 0)
        {
            Complete = true;
            Utils.soundManager.PlayFX(rightSound);
            transitionParticle.SetActive(true);
            Invoke("CompleteFunc", 2f);

         
        }
        else
        {
            wrongParticle.SetActive(true);
            Utils.soundManager.PlayFX(wrongSound);
            enable = false;
            Invoke("Reset", 2.5f); 
         
        }
  
    }
    void CompleteFunc()
    {
    
        Utils.soundManager.PlayFX(redToBlueSound);
        
        completeParticle.SetActive(true);
        rend.material = blueMat;
        if (obj)
            obj.Activate();
        if (manager)
            manager.CheckAll();

        if (warpDoor)
            warpDoor.CanEnter = true;

        if (warpDoorParticle)
        {
            ParticleSystem.MainModule settings = warpDoorParticle.GetComponent<ParticleSystem>().main;
            settings.startColor = new ParticleSystem.MinMaxGradient(Color.green);
        }

        if (activeObj)
        {
            activeObj.SetActive(true);
        }

        if (marker)
            marker.ShowPopup();

        if (completeParticle)
            completeParticle.SetActive(true);
    }
    private void Reset()
    {
        enable = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
