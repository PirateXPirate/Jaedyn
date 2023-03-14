using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public GameObject tiger;
    public GameObject leo;
    public GameObject owl;
    public Image btSwitch;
    public Text txtHeartItem;
    public Text txtBarierItem;
    public Text txtMapItem;
    public Text txtImageItem;
    public GameObject[] heartShow;
    public int myHeart = 5;
    public int myHeartItem = 0;

    public GameObject characterAll;
    public GameObject[] characters;
    public Sprite[] spCharacters;
    public float[] StatJump;
    public float[] StatAttack;
    //public Animator[] AnimatorController;
    public int currentSelect = 0;
    public CharacterController control;
    public Rigidbody rd;

    public GameObject dimHeart;
    public GameObject dimKey;
    public GameObject dimGotImage;
    public GameObject dimFinish;
    public GameObject dimStory;
    public GameObject dimBoss;
    public GameObject dimGameOver;

    public GameObject owlAttack;
    public GameObject tigerAttack;

    public GameObject BlockNoFlyBack;

    //Sound
    public AudioClip fxPopup;
    public AudioClip fxAttack;
    public AudioClip fxClick;
    public AudioClip fxHint;
    public AudioClip fxCollect;

    public AudioClip fxTigerAttack;
    public AudioClip fxOwlMagic;
    public AudioClip fxBossAttack;

    //FX Sound Speak
    public AudioClip fxSpeak0;
    public AudioClip fxSpeak1;
    public AudioClip fxSpeak2;
    public AudioClip fxSpeak3;
    public AudioClip fxSpeak4;
    public AudioClip fxSpeak5;

    private float g = 0.15f;
    private bool isDie = false;
    private bool isFreeze = false;
    private bool isBarier = false;

    private float[] speed= { 0.035f,0.03f,0.06f};

    //Skin
    public Material[] leoSkin;
    public Material[] tigerSkin;
    public Material[] owlSkin;
    public SkinnedMeshRenderer leoSkinMesh;
    public SkinnedMeshRenderer tigerSkinMesh;
    public SkinnedMeshRenderer owlSkinMesh;
    public SkinnedMeshRenderer owlWingLSkinMesh;
    public SkinnedMeshRenderer owlWingRSkinMesh;
    public SkinnedMeshRenderer owlWingTailSkinMesh;
    private int cntLeoSkin=0;
    private int cntTigerSkin = 0;
    private int cntOwlSkin = 0;
    private int maxLeoSkin = 2;
    private int maxTigerSkin = 2;
    private int maxOwlSkin = 4;

    float j = 0f;
    bool isJump2Start = false;


    private void Start() {
        Utils.soundManager.PlayFX(fxSpeak0);
    }

    public void SetDirection(float moveH, float moveV, bool isJump = false, bool isJump2 = false,bool isGround=true) {
        if (isDie || isFreeze) return;

        //characters[0].transform.Rotate(new Vector3(0f, moveH * 0.7f, 0f));
        //characters[1].transform.Rotate(new Vector3(0f, moveH * 0.7f, 0f));
        //characters[2].transform.Rotate(new Vector3(0f, moveH * 0.7f, 0f));

        Vector3 movement = new Vector3(moveH, 0.0f, moveV);
        if (moveH != 0 || moveV != 0) {
            characters[0].transform.rotation = Quaternion.LookRotation(movement);
            characters[1].transform.rotation = Quaternion.LookRotation(movement);
            characters[2].transform.rotation = Quaternion.LookRotation(movement);
        }

        if (!isJump2Start && isJump) {
            if (currentSelect != 2) {
                //Not Owl
                j = 2.5f;
                characters[currentSelect].GetComponent<Animator>().SetTrigger("isJump");
            }
        }else if (!isJump2Start && isJump2) {
            if (currentSelect == 0 ) {
                isJump2Start = true;
                Invoke("ResetJump", 2f);
                //is Leo
                j = 2.5f;
            }
        } else {
            j -= g;
        }

        //if (moveH != 0 || moveV != 0) {
            movement.y = j;
            control.Move(movement * speed[currentSelect]*3f);
        //}

        if(moveH==0 && moveV == 0) {
            characters[currentSelect].GetComponent<Animator>().SetBool("isWalk", false);
        } else {
            characters[currentSelect].GetComponent<Animator>().SetBool("isWalk", true);
        }
    }

    void ResetJump() {
        isJump2Start = false;
    }

    public int SwitchCharacter() {
        if (onFly != null || isDie || isFreeze)return currentSelect;

        currentSelect++;
        if (currentSelect > 2) {
            currentSelect = 0;
        }


        characters[0].SetActive(false);
        characters[1].SetActive(false);
        characters[2].SetActive(false);

        characters[currentSelect].SetActive(true);

        btSwitch.sprite = spCharacters[currentSelect];

        //if (currentSelect == 2) {
        //    g = 0;
        //} else {
        //    g = 0.05f;
        //}

        return currentSelect;
    }

    bool isFly = false;
    Vector3 speedMoveOwl;
    void Updadte() {
        if (isFreeze) {
            control.Move(speedMoveOwl);
        }
    }

    //bt1
    public void OnInteractive() {
        //Action
        if (isDie || isFreeze) return;

        if (currentSelect == 0) {
            //lion Push
            characters[currentSelect].GetComponent<Animator>().SetTrigger("isAttack");
            if (destDestroy != null) {
                Hashtable ht = new Hashtable();
                ht.Add("x", 0);
                ht.Add("y", 0);
                ht.Add("easeType", iTween.EaseType.easeInBack);
                ht.Add("time", 0.5f);
                iTween.ScaleTo(destDestroy, ht);
                destDestroy.GetComponent<BoxCollider>().enabled = false;

                if (destDestroy.name == "BlockBoss") {
                    dimBoss.SetActive(true);
                    InvokeRepeating("BossAlert", 0.5f, 0.5f);
                }
            }
        } else if (currentSelect == 1) {
            //tiger Attack 10%
            characters[currentSelect].GetComponent<Animator>().SetTrigger("isAttack");
            Utils.soundManager.PlayFX(fxTigerAttack, true);
            
            if (!tigerAttack.activeSelf) {
                tigerAttack.SetActive(true);
                Invoke("HideOwlAttack", 1.5f);
            }

        } else if (currentSelect == 2) {
            //owl magic
            characters[currentSelect].GetComponent<Animator>().SetTrigger("isMagic");
            Utils.soundManager.PlayFX(fxOwlMagic);
            if (!owlAttack.activeSelf) {
                owlAttack.SetActive(true);
                Invoke("HideOwlAttack", 1.5f);
            }
        }
    }

    //bt2
    public void OnSkill() {
        if (isDie || isFreeze) return;

        if (currentSelect == 0) {
            //Leon Jump 
        }else if (currentSelect == 1) {
            //Tiger Attack
            characters[currentSelect].GetComponent<Animator>().SetTrigger("isAttack");
            Utils.soundManager.PlayFX(fxTigerAttack, true);

            if (!tigerAttack.activeSelf) {
                tigerAttack.SetActive(true);
                Invoke("HideOwlAttack", 1.5f);
            }

            if (destDestroy != null) {
                Hashtable ht = new Hashtable();
                ht.Add("x", 0);
                ht.Add("y", 0);
                ht.Add("easeType", iTween.EaseType.easeInBack);
                ht.Add("time", 0.5f);
                iTween.ScaleTo(destDestroy, ht);
                destDestroy.GetComponent<BoxCollider>().enabled = false;

                if (destDestroy.name== "BlockBoss") {
                    dimBoss.SetActive(true);
                    InvokeRepeating("BossAlert", 0.5f, 0.5f);
                }
            }
        } else {
            //Owl Fly
            characters[currentSelect].GetComponent<Animator>().SetTrigger("isWalk");
            if (onFly != null) {
                isFreeze = true;
                //isFly = true;
                onFly.gameObject.SetActive(false);

                posWarp = onFly.GetComponent<OnFlyScript>().destination.position;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                characters[currentSelect].GetComponent<BoxCollider>().enabled = true;

                speedMoveOwl = (posWarp - characterAll.transform.position)*0.1f;
                Hashtable ht = new Hashtable();
                ht.Add("position", posWarp);
                ht.Add("time", 2f);
                ht.Add("easetype", iTween.EaseType.linear);
                iTween.MoveTo(characters[currentSelect], ht);

                if (!owlAttack.activeSelf) {
                    owlAttack.SetActive(true);
                    Invoke("HideOwlAttack", 1.5f);
                }

                Invoke("MoveToPosition", 0.3f);
                //MoveToPosition();
            }
        }
    }

    Vector3 posWarp;
    void MoveToPosition() {
        //gameObject.transform.position = posWarp;
        //Hashtable ht = new Hashtable();
        //ht.Add("x", 1);
        //ht.Add("y", 1);
        //ht.Add("z", 1);
        //ht.Add("time", 0.5f);
        //ht.Add("easetype", iTween.EaseType.easeOutBack);
        //iTween.ScaleTo(characters[currentSelect], ht);
        characters[currentSelect].transform.position = new Vector3(0f, -0.127f, 0f);
        gameObject.transform.position = posWarp;
        isFly = false;


        Invoke("ResetFreeze", 1.5f);
    }

    public Vector3 target;
    void MoveTowardsTarget() {
        var cc = GetComponent<CharacterController>();
        var offset = target - transform.position;
        //Get the difference.
        if (offset.magnitude > .1f) {
            offset = offset.normalized * 0.1f;
            cc.Move(offset * Time.deltaTime);
        }
    }


    //bt3
    public void OnActive() {
        if (isDie || isFreeze) return;

        //Open Close Switch

    }

    //bt Change Skin
    public void OnClickChangeSkin() {
        switch (currentSelect) {
            case 0:
                cntLeoSkin++;
                if (cntLeoSkin >= maxLeoSkin) {
                    cntLeoSkin = 0;
                }
                leoSkinMesh.material = leoSkin[cntLeoSkin];
                break;
            case 1:
                cntTigerSkin++;
                if (cntTigerSkin >= maxTigerSkin) {
                    cntTigerSkin = 0;
                }
                tigerSkinMesh.material = tigerSkin[cntTigerSkin];
                break;
            case 2:
                cntOwlSkin++;
                if (cntOwlSkin >= maxOwlSkin) {
                    cntOwlSkin = 0;
                }
                owlSkinMesh.material = owlSkin[cntOwlSkin];
                owlWingLSkinMesh.material = owlSkin[cntOwlSkin];
                owlWingRSkinMesh.material = owlSkin[cntOwlSkin];
                owlWingTailSkinMesh.material = owlSkin[cntOwlSkin];
                break;
        }
    }

    

    GameObject destDestroy = null;
    GameObject destDestroyHide = null;
    GameObject destBoss = null;
    GameObject onFly = null;
    float hitTime = 0f;
    private void OnTriggerEnter(Collider other) {
        if (isDie) return;
        print(other.tag);
        if (other.gameObject.tag == "Destroyable") {
            destDestroy = other.gameObject;
            Utils.soundManager.PlayFX(fxTigerAttack, true);
        } else if (other.gameObject.tag == "Fly" && currentSelect==2) {
            onFly = other.gameObject;
        } else if (other.gameObject.tag == "NotiJump") {
            Utils.soundManager.PlayFX(fxSpeak2);
            dimStory.GetComponent<ClickToCloseScript>().SetupStory("กดปุ่ม 1 สองครั้ง เพ่ือกระโดดสูง");
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "NotiJump0") {
            Utils.soundManager.PlayFX(fxSpeak1);
            dimStory.GetComponent<ClickToCloseScript>().SetupStory("กดปุ่ม 1 เพื่อกระโดดธรรมดา");
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "NotiFlyer") {
            Utils.soundManager.PlayFX(fxSpeak4);
            dimStory.GetComponent<ClickToCloseScript>().SetupStory("สงสัยตรงนี้ต้องบินผ่านไปแล้วล่ะ");
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "TigerMode") {
            Utils.soundManager.PlayFX(fxSpeak3);
            dimStory.GetComponent<ClickToCloseScript>().SetupStory("กดเปลี่ยนตัวละครเป็นเสือโคร่ง เพื่อทำลายกล่อง");
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "NotiKey") {
            Utils.soundManager.PlayFX(fxPopup);
            dimStory.GetComponent<ClickToCloseScript>().SetupStory("อย่าลืมเก็บกุญแจด้วยนะ เพื่อเปิดรูปภาพในแกลลอรี่");
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "Key") {
            Utils.soundManager.PlayFX(fxPopup);
            dimKey.SetActive(true);
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "Gallery") {
            Utils.soundManager.PlayFX(fxPopup);
            txtImageItem.text = "1/1";
            dimGotImage.SetActive(true);
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "Heart") {
            Utils.soundManager.PlayFX(fxPopup);
            dimHeart.SetActive(true);
            myHeartItem++;
            txtHeartItem.text = myHeartItem.ToString();
            
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "Finish") {
            Utils.soundManager.PlayFX(fxPopup);
            //End Game
            dimFinish.SetActive(true);
        } else if (other.gameObject.tag == "Boss") {
            if ((Time.timeSinceLevelLoad-hitTime)>2f) {
                Utils.soundManager.PlayFX(fxAttack);
                //Damage
                hitTime = Time.timeSinceLevelLoad;
                if(!isBarier)myHeart--;
                ShowHeart();
                if (myHeart <= 0) {
                    //Die
                    isDie = true;
                    dimGameOver.SetActive(true); 
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (onFly != null) {
            Invoke("ResetTrigger", 1f);
        }

        destDestroy = null;
    }

    //Button
    public void OnClickHeartItem() {
        Utils.soundManager.PlayFX(fxClick);
        if (myHeartItem > 0 && myHeart<5) {
            myHeartItem--;
            txtHeartItem.text = myHeart.ToString();

            myHeart++;
            if (myHeart > 5) myHeart = 5;
            ShowHeart();
        }else if (Utils.potion>0 && myHeart < 5) {
            Utils.potion--;
            myHeart++;
            txtHeartItem.text = myHeart.ToString();
            PlayerPrefs.SetInt("potion", Utils.potion);
            PlayerPrefs.Save();
        }
    }

    public void OnClickBarierItem() {
        Utils.soundManager.PlayFX(fxClick);
        if (Utils.potion_barier > 0) {
            Utils.potion_barier--;

            isBarier = true;
            Invoke("ResetBarier", 10f);

            txtBarierItem.text = Utils.potion_barier.ToString();
            PlayerPrefs.SetInt("potionBarier", Utils.potion_barier);
            PlayerPrefs.Save();
        }
    }

    //All
    private void ResetBarier() {
        isBarier = false;
    }

    private void ResetFreeze() {
        isFreeze = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        characters[currentSelect].SetActive(true);
        BlockNoFlyBack.SetActive(true);
        if (onFly != null) {
            onFly.gameObject.SetActive(true);
            onFly = null;
        }
    }

    private void HideObject() {
        destDestroyHide.SetActive(false);
    }

    private void HideOwlAttack() {
        owlAttack.SetActive(false);
        tigerAttack.SetActive(false);
    }

    private void ResetTrigger() {
        if (onFly != null) {
            onFly = null;
        }
    }

    public void ShowHeart() {
        for(int i=0;i<5;i++) heartShow[i].SetActive(false);
        for(int i = 0; i < myHeart; i++) {
            heartShow[i].SetActive(true);
        }
    }

    int bossAlertCnt = 0;
    private void BossAlert() {
        isFreeze = false;
        bossAlertCnt++;
        dimBoss.SetActive(!dimBoss.activeSelf);
        if (bossAlertCnt > 6) {
            bossAlertCnt = 0;
            dimBoss.SetActive(false);
            CancelInvoke("BossAlert");
        }
    }
}
