using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeyOnDeath : MonoBehaviour
{
    public KeyManager keyMAnager;
    Health health;
    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += OnObjDead;
    }
    private void OnObjDead()
    {
        keyMAnager.CheckKey();

    }// Update is called once per frame
        void Update()
    {
        
    }
}
