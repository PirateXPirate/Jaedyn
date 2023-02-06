using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlManagerCustom : MonoBehaviour
{
    public GameObject controllerBG;
    public GameObject controller;
    public GameObject character;
    public GameObject cam;
    public GameObject[] characterList;
    public CharacterControl cc;
    public CharacterController ccer;

    private int stamina=3;
    public Image[] staminaFull;
    public Text txtPotion;
    public Text txtArmor;

    //Button Icon Change
    public Image bt1;
    public Image bt2;
    public Sprite[] bt1Sprite;
    public Sprite[] bt2Sprite;

    private float halfW;
    private bool isMouseDown = false;
    private bool isDragScreenDown = false;
    private bool isDragScreenDownStart = false;
    private Vector3 dragScreenDownPos = new Vector3();
    private float maxDistance = 60f;
    private Vector3 camDistance;
    private bool isJump = false;
    private bool isJump2 = false;

    private int touchControlID;
    private int touchDragScreenID;
    private int characterType = 1;
    private Vector3 controlPosDef;

    public AudioClip fxClick;

    public GameObject DimHome;

    void Start()
    {
        halfW = Screen.width * 0.5f;
        camDistance = character.transform.position - cam.transform.position;
        controlPosDef = controllerBG.transform.position;

        txtPotion.text = Utils.potion.ToString();
        txtArmor.text = Utils.potion_barier.ToString();
    }

    void Update()
    {
        float v = 0;
        float h = 0;
        float dragv = 0;
        float dragh = 0;

#if UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_STANDALONE
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        ControlCharacter(h, v);

#else
        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.touches[i];

                if (touch.phase == TouchPhase.Began) {
                    if (!isMouseDown && touch.position.x < halfW) {
                        touchControlID = touch.fingerId;
                        isMouseDown = true;
                        controllerBG.transform.position = new Vector3(touch.position.x, touch.position.y, 1);

                    }
                    //if (!isDragScreenDown && touch.position.x > halfW) {
                    //    if (Vector2.Distance(new Vector2(Screen.width, 0f), new Vector2(touch.position.x, touch.position.y)) > (Screen.width * 0.5f)) {
                    //        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
                    //            touchDragScreenID = touch.fingerId;
                    //            isDragScreenDown = true;
                    //            dragScreenDownPos = touch.position;
                    //        }
                    //    }
                    //}
                } else if (touch.phase == TouchPhase.Ended) {
                    if (touchControlID == touch.fingerId) {
                        isMouseDown = false;
                        controller.transform.position = controllerBG.transform.position;
                        controllerBG.transform.position = controlPosDef;

                    }
                    //if (touchDragScreenID == touch.fingerId) {
                    //    touchDragScreenID = -1;
                    //    isDragScreenDown = false;
                    //    dragh = 0;
                    //    dragv = 0;
                    //}
                    //character.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                if (isMouseDown && touchControlID == touch.fingerId) {
                    Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
                    float distance = Vector3.Distance(touchPos, controllerBG.transform.position);
                    Vector3 div = touchPos - controllerBG.transform.position;
                    float rad = Mathf.Atan2(div.y, div.x);
                    if (distance > maxDistance) controllerBG.transform.position += new Vector3(Mathf.Cos(rad) * (distance - maxDistance), Mathf.Sin(rad) * (distance - maxDistance), 0);
                    distance = Mathf.Min(distance, maxDistance);

                    Vector3 posController = new Vector3(Mathf.Cos(rad) * distance, Mathf.Sin(rad) * distance, 0);
                    posController += controllerBG.transform.position;
                    controller.transform.position = posController;

                    h = Mathf.Cos(rad);
                    v = Mathf.Sin(rad);
                }

                //if(isDragScreenDown && touchDragScreenID == touch.fingerId){
                //        Vector3 diff = dragScreenDownPos-vector2ToVector3(touch.position);
                //        dragh = diff.x;
                //        dragv = diff.y;
                //}

            }
            ControlCharacter(h, v);
        }
#endif
        cam.transform.position = character.transform.position - camDistance;
    }

    void ControlCharacter(float dx, float dy) {
        cc.GetComponent<CharacterControl>().SetDirection(dx,dy, isJump,isJump2,ccer.isGrounded);

        isJump = false;
        isJump2 = false;
    }

    bool isResetJump = true;
    public void OnClickJump() {
        Utils.soundManager.PlayFX(fxClick);
        if (ccer.isGrounded) {
            isJump = true;
        } else if (!ccer.isGrounded && !isJump2) {
            isJump2 = true;
        }
    }

    public void OnClickSwitch() {
        Utils.soundManager.PlayFX(fxClick);
        int select=cc.GetComponent<CharacterControl>().SwitchCharacter();

        //Change Icon Button Skill
        bt1.sprite = bt1Sprite[select];
        bt2.sprite = bt2Sprite[select];
    }

    //Bt2
    public void OnClickSkill() {
        Utils.soundManager.PlayFX(fxClick);
        if (cc.currentSelect == 0) {
            if (stamina > 0) {
                stamina--;
                StaminaDisplay();
                StartRestoreStamina();
                //Leon Jump
                OnClickJump();
            }
        } else if (cc.currentSelect == 1) {
            //Tiger Attack 50%
            if (stamina >= 2) {
                stamina-=2;
                StaminaDisplay();
                StartRestoreStamina();
                //Damage Attack
            }
        } else {
            //Owl Magic Fly
            if (stamina >= 3) {
                stamina-=3;
                StaminaDisplay();
                StartRestoreStamina();
                //Fly Throught
            }
        }
        cc.GetComponent<CharacterControl>().OnSkill();
    }

    //Bt1
    public void OnClickInteractive() {
        Utils.soundManager.PlayFX(fxClick);
        if (stamina >= 1) {
            stamina --;
            StaminaDisplay();
            StartRestoreStamina();
            cc.GetComponent<CharacterControl>().OnInteractive();
        }
    }

    //Bt3
    public void OnClickActive() {
        //Open Close Switch
        cc.GetComponent<CharacterControl>().OnActive();
    }

    public void OnClickHome() {
        DimHome.SetActive(true);
    }

    //Stamina
    private void StaminaDisplay() {
        for(int i = 0; i < 3; i++) {
            if (i < stamina) {
                staminaFull[i].gameObject.SetActive(true);
            } else {
                staminaFull[i].gameObject.SetActive(false);
            }
        }
        
    }

    private void StartRestoreStamina() {
        CancelInvoke("RestoreStamina");
        Invoke("RestoreStamina", 3f);
    }

    private void RestoreStamina() {
        stamina++;
        StaminaDisplay();
        if (stamina < 3) {
            Invoke("RestoreStamina", 3f);
        }
    }
}
