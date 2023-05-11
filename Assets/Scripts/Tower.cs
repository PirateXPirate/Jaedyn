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

    void Start()
    {
        manager = GetComponentInParent<TowerManager>();
        timeTochange = Random.Range(1, maxTime);
    }

    public void Perform()
    {
        timeTochange -= 1;
        if (timeTochange == 0)
        {
            Complete = true;
            rend.material = blueMat;
            if(obj)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
