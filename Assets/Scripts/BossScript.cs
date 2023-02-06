using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Animator anim;
    public Transform[] wayPoint;
    public GameObject player;
    public CapsuleCollider col;

    public AudioClip fxBossAttack;
    public AudioClip fxBossPain;

    public bool isTutorial = true;

    private int numPath = 0;
    private bool isDetect = false;
    private int rndPath = 0;
    private bool isPlayerInArea = false;
    private bool isAttack = false;
    private bool isSpinAttack = false;

    private int BossHeart = 1;
    private bool isDie = false;

    void Start()
    {
        numPath = wayPoint.Length;
        if(!isTutorial)Invoke("StartWalk", 5f);
    }

    void Update()
    {
        if (isSpinAttack || isAttack || isDie) return;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 2f && !isAttack) {
            //Call Attack
            isAttack = true;
        } else if (dist<5f) {
            isPlayerInArea = true;
            isAttack = false;
        } else {
            isPlayerInArea = false;
            isAttack = false;
        }
    }

    public void GotAttack(int damage=1) {
        if (isDie) return;

        Utils.soundManager.PlayFX(fxBossPain);
        BossHeart-= damage;
        if (BossHeart <= 0) {
            BossHeart = 0;
            isDie = true;
            anim.SetTrigger("isDie");
            col.enabled = false;
        }
    }

    //All
    void StartWalk() {
        if (isDie) return;

        anim.SetBool("isWalk", true);
        rndPath = Mathf.FloorToInt(Random.Range(0, numPath));

        Vector3 meLook = transform.position;
        meLook.x = wayPoint[rndPath].transform.position.x;
        meLook.z = wayPoint[rndPath].transform.position.z;

        Hashtable ht = new Hashtable();
        ht.Add("looktarget", meLook);
        ht.Add("time", 0.5f);
        ht.Add("easeType", iTween.EaseType.linear);
        iTween.LookTo(gameObject, ht);

        ht.Clear();
        ht.Add("x", meLook.x);
        ht.Add("y", meLook.y);
        ht.Add("z", meLook.z);
        ht.Add("time", Vector3.Distance(transform.position, wayPoint[rndPath].transform.position)*0.5f);
        ht.Add("easeType", iTween.EaseType.linear);
        ht.Add("oncompletetarget", gameObject);
        ht.Add("oncomplete", "WhatToDoNext");
        iTween.MoveTo(gameObject, ht);
    }

    void WhatToDoNext() {
        if (isDie) return;

        isSpinAttack = false;

        if (isAttack) {
            anim.SetTrigger("isAttack");
            Utils.soundManager.PlayFX(fxBossAttack);
            Invoke("ResetAttack", 4f);
        }

        anim.SetBool("isWalk", false);
        if (isPlayerInArea) {
            anim.SetBool("isWalk", true);
            //gameObject.transform.LookAt(wayPoint[rndPath].transform);

            Vector3 meLook = transform.position;
            meLook.x = player.transform.position.x;
            meLook.z = player.transform.position.z;

            Hashtable ht = new Hashtable();
            ht.Add("looktarget", meLook);
            ht.Add("time", 0.5f);
            ht.Add("easeType", iTween.EaseType.linear);
            iTween.LookTo(gameObject, ht);

            ht.Clear();
            ht.Add("x", meLook.x);
            ht.Add("y", meLook.y);
            ht.Add("z", meLook.z);
            ht.Add("time", Vector3.Distance(transform.position, wayPoint[rndPath].transform.position) * 0.5f);
            ht.Add("easeType", iTween.EaseType.linear);
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", "WhatToDoNext");
            iTween.MoveTo(gameObject, ht);

            return;
        }

        int rnd = Mathf.FloorToInt(Random.Range(0, 10));
        if (rnd < 5) {
            StartWalk();
        }else if (rnd<7) {
            SpinAttack();
        } else {
            Invoke("StartWalk", Random.Range(2,5));
        }
    }

    void SpinAttack() {
        if (isDie) return;
        isSpinAttack = true;

        Utils.soundManager.PlayFX(fxBossAttack);
        anim.SetBool("isSpinLoop", true);
        rndPath = Mathf.FloorToInt(Random.Range(0, numPath));

        Vector3 meLook = transform.position;
        meLook.x = wayPoint[rndPath].transform.position.x;
        meLook.z = wayPoint[rndPath].transform.position.z;

        Hashtable ht = new Hashtable();
        ht.Add("x", meLook.x);
        ht.Add("y", meLook.y);
        ht.Add("z", meLook.z);
        ht.Add("time", Vector3.Distance(transform.position, wayPoint[rndPath].transform.position) * 0.2f);
        ht.Add("easeType", iTween.EaseType.linear);
        ht.Add("oncompletetarget", gameObject);
        ht.Add("oncomplete", "StopSpin");
        iTween.MoveTo(gameObject, ht);
    }

    void StopSpin() {
        isSpinAttack = false;
        anim.SetBool("isSpinLoop", false);
        WhatToDoNext();
    }

    void ResetAttack() {
        isAttack = false;
        WhatToDoNext();
    }
}
