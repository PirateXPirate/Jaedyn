using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkinManager : MonoBehaviour


{
    [SerializeField] Button changeSkinBut;
    [SerializeField] Image changeSkinImg;
    [SerializeField] Material[] leoMats;
    [SerializeField] Material[] tigerMats;
    [SerializeField] Material[] owlMats;

    [SerializeField] ParticleSystem skinFx;
    [SerializeField] AudioClip skinSound;

    protected ParticleSystem _instantiatedVFX;
    bool canChange = true;

    int leoMatIndex = 0;
    int tigerMatIndex = 0;
    int owlMatIndex = 0;
    float coolDownTime = 3;
    float count;

   
    // Update is called once per frame
    void Update()
    {
        if (canChange == false)
        {
            count += Time.deltaTime;
            if (count > coolDownTime)
            {
                canChange = true;
                changeSkinBut.enabled = true;
                changeSkinImg.color = new Color32(255, 255, 255, 255);
                count = 0;
            }
        }
    }
    public void OnChangeSkin()
    {
        if (!canChange) return;
        canChange = false;
        changeSkinBut.enabled = false;
        changeSkinImg.color = new Color32(255, 255, 255, 100);

        _instantiatedVFX = Instantiate(skinFx, LevelManager.Instance.Players[0].transform.position, Quaternion.Euler(-90,0,0));


        Utils.soundManager.PlayFX(skinSound, true);

        switch (LevelManager.Instance.Players[0].name)
        {

            case "CharacterSwitch_0" or "CharacterAll (3)":
                leoMatIndex += 1;
                if (leoMatIndex > leoMats.Length - 1)
                    leoMatIndex = 0;
                LevelManager.Instance.Players[0].GetComponentInChildren<Renderer>().material = leoMats[leoMatIndex];
                break;
            case "CharacterSwitch_1":
                owlMatIndex += 1;
                if (owlMatIndex > owlMats.Length - 1)
                    owlMatIndex = 0;
                LevelManager.Instance.Players[0].GetComponentInChildren<Renderer>().material = owlMats[owlMatIndex];
                break;
            case "CharacterSwitch_2":
                tigerMatIndex += 1;
                if (tigerMatIndex > tigerMats.Length - 1)
                    tigerMatIndex = 0;
                LevelManager.Instance.Players[0].GetComponentInChildren<Renderer>().material = tigerMats[tigerMatIndex];
                break;
        }

    }
    
}
