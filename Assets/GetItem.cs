using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private KeyManager keyManager;
    int hpPotionQuantity = 0;
    int resistancePotionQuantity = 0;
    [SerializeField] ItemType itemType;
    public AudioClip SoundClip;
    public enum ItemType
    {
        Key,
        Lock,
        HpPotion,
        ResistancePotion
    }
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        if (SoundClip)
            Utils.soundManager.PlayFX(SoundClip);
        switch (itemType)
        {
            case ItemType.Key:
                LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
                LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
                LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
                keyManager.AddKey(1);
                break;
            case ItemType.Lock:
                keyManager.GetLock();
                break;
        }
       
    }
}
