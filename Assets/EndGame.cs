using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject controllerUi;
    [SerializeField] private GameObject picture;

    [SerializeField] private Button settingBut;
    [SerializeField] private Button homeBut;


    [Header("Setting SaveData Input")]
    [SerializeField] private bool isTutorialLevel = false;
    [SerializeField] enum Mode { easy, hard}
    [SerializeField] Mode mode;
    [SerializeField] private int levelIndex = 0;
    private bool isPictureTaken = false;

    void Awake()
    {
        PrepareData();

        controllerUi.SetActive(false);
        settingBut.enabled = false;
        homeBut.enabled = false;
        LevelManager.Instance.Players[0].GetComponent<Health>().ImmuneToDamage = true;
    }

    void Start()
    {
        SaveDataToPlayerPrefs();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("MapScene");
        }
    }

    void PrepareData()
    {
        CheckPictureInScene();
        SetModeData();
        SetLevelState();

        void CheckPictureInScene()
        {
            if (picture == null) return;
            if (!picture.activeSelf)
            {
                isPictureTaken = true;
            }
        }

        void SetModeData()
        {
            switch (this.mode)
            {
                case Mode.easy:
                    LevelData.mode = LevelData.Mode.easy;
                    break;
                case Mode.hard:
                    LevelData.mode = LevelData.Mode.hard;
                    break;
            }
        }

        void SetLevelState()
        {
            if (isPictureTaken)
            {
                LevelData.levelState = LevelData.LevelState.PerfectCleared;
            }

            else
            {
                LevelData.levelState = LevelData.LevelState.ClearWithOutPicture;
            }
        }
    }

    void SaveDataToPlayerPrefs()
    {
        if (isTutorialLevel)
        {
            if(LevelData.levelState == LevelData.LevelState.PerfectCleared)
            {
                LevelData.TutorialComplete(true);
            }

            else
            {
                LevelData.TutorialComplete(false);
            }

            return;
        }

        else
        {
            LevelData.SaveLevelStateData(levelIndex);
        }
    }
}
