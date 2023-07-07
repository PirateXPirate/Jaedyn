using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyManager : MonoBehaviour
{
    public Firefly[] fireflySet;
    int fireflyNum;
    public GameObject particle;
    public ActivateObject[] activatedObjects;
    public AudioClip[] flySound;
    public AudioClip completeSound;
    int index = 0;
    void Start()
    {

    }
    public void ActivateFirefly()
    {
       
        fireflyNum += 1;
        if (fireflyNum == fireflySet.Length)
        {
            particle.SetActive(true);
            foreach (var a in activatedObjects)
            {
              
                a.Activate();
            }
              
            Utils.soundManager.PlayFX(completeSound);
        }
    }
    public void PlayFlySound()
    {
        Utils.soundManager.PlayFX(flySound[index]);
        index += 1;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
