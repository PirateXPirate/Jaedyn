using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBox : MonoBehaviour
{
    Animator animator;

    public ChestType currentChestType;
    public ParticleSystem openParticle;

    public GameObject PopuptoShow;
    public GameObject ResistancePopup;

    AddItem addItem;

    AudioSource audi;

    public bool canOpen;

    public CoinManager coinManager;

    public KeyManager keyManager;

    public GameObject[] activeObjs;
     public enum ChestType
    {
        Coin,
        Key,
        Potion
    }
    private void Start()
    {
        audi = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        addItem = GetComponent<AddItem>();
    }
    public void Perform()
    {
        if (!canOpen) return;
        animator.SetBool("openchest", true);
        openParticle.Play();
    }

    public void PlaySound()
    {
        audi.Play();
    }

    public void SpawnDrops()
    {
        if (activeObjs.Length > 0)
        {
            foreach (var ob in activeObjs)
            {
                ob.SetActive(true);
            }
        }
        canOpen = false;
        if (currentChestType == ChestType.Potion)
        {
            var randomPotion = Random.Range(0, 100);
            if (randomPotion >= 70)
            {
                PopuptoShow.SetActive(true);
                addItem.GetPotionChest();
            }
            else
            {
                ResistancePopup.SetActive(true);

                addItem.GetResistanceChest();
            }

        }
        
        if (currentChestType == ChestType.Coin)
        {
            PopuptoShow.SetActive(true);
            var randomCoin = Random.Range(0,101);
            int numCoin;
            if (randomCoin <= 65)
                numCoin = 1;
            else if (randomCoin <= 85)
                numCoin = 2;
            else if (randomCoin <= 95)
                numCoin = 5;
            else
                numCoin = 10;
            coinManager.AddCoin(numCoin);
        }
        if (currentChestType == ChestType.Key)
        {
            PopuptoShow.SetActive(true);
            keyManager.AddKey(1);
        }

            
    }
}
