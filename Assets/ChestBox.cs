using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBox : MonoBehaviour
{
    Animator animator;

    public ChestType currentChestType;
    public ParticleSystem openParticle;

    public GameObject PopuptoShow;

    AddItem addItem;

    AudioSource audi;

    public bool canOpen;
    public enum ChestType
    {
        Coin,
        Key,
        Potion
    }
    private void Start()
    {
        audi = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        addItem = GetComponent<AddItem>();
    }
    public void Perform()
    {
        if (!canOpen) return;
        animator.SetBool("openchest",true);
        openParticle.Play();
    }

    public void PlaySound()
    {
        audi.Play();
    }

    public void SpawnDrops()
    {
        PopuptoShow.SetActive(true);
        addItem.GetPotionChest();
    }
}
