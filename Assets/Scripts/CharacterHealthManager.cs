using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthManager : MonoBehaviour, MMEventListener<HealthChangeEvent>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()

    {
        this.MMEventStartListening<HealthChangeEvent>();
    }
    void OnDisable()
    {
        this.MMEventStopListening<HealthChangeEvent>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMMEvent(HealthChangeEvent eventType)
    {
       // Debug.Log(eventType.NewHealth);
    }
}
