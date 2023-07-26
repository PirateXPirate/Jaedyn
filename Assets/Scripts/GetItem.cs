using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private KeyManager keyManager;
    [SerializeField] private PowerUpManager powerManager;

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
        if (other.tag != "Player") return;
        
        if (SoundClip)
            Utils.soundManager.PlayFX(SoundClip);
        switch (itemType)
        {
            case ItemType.Key:
                LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
                LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
                LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
                keyManager.AddKey(1);
                gameObject.SetActive(false);
                break;
            case ItemType.Lock:
                if(keyManager.gotKey)
                    gameObject.SetActive(false);
                keyManager.GetLock();
                break;
            case ItemType.HpPotion:
                LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
                LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
                LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
                powerManager.AddHpPotion(1);
                gameObject.SetActive(false);
                break;
            case ItemType.ResistancePotion:
                LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
                LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
                LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
                powerManager.AddResistancePotion(1);
                gameObject.SetActive(false);
                break;
        }
       
    }
}
