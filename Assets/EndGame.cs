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

    [SerializeField] private bool isTutorialLevel = false;
    [SerializeField] private int levelIndex = 0;
    private bool isPictureTaken = false;
    [SerializeField] private int levelState;

    [SerializeField] private enum Mode { easy, hard }
    [SerializeField] private Mode mode;

    [SerializeField] private Button settingBut;
    [SerializeField] private Button homeBut;

    private void Awake()
    {
        controllerUi.SetActive(false);
        CheckPictureInScene();
        CheckEndGameState();

        settingBut.enabled = false;
        homeBut.enabled = false;
        LevelManager.Instance.Players[0].GetComponent<Health>().ImmuneToDamage = true;
    }

    void Start()
    {
        SaveToPlayerPerf();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("MapScene");
        }
    }

    void CheckPictureInScene()
    {
        if (!picture.activeSelf)
        {
            isPictureTaken = true;
        }
    }

    void CheckEndGameState()
    {
        if (isPictureTaken)
        {
            levelState = 2;
        }

        else
        {
            levelState = 1;
        }
    }
    void SaveToPlayerPerf()
    {
        if (isTutorialLevel)
        {
            LevelData.TutorialComplete();
            return;
        }

        else
        {
            LevelData.SaveLevelStateData(mode.ToString(), levelIndex, levelState);
        }
    }
}
