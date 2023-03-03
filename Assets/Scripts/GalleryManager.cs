using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    [SerializeField] GameObject picturePopup;
    [SerializeField] GameObject[] pictureExpand;
    [SerializeField] GameObject lockedPopup;

    [Header("Mode")]
    [SerializeField] GameObject pictureMode;
    [SerializeField] GameObject musicMode;

    [Header("Button")]
    [SerializeField] Button[] picture;
    [SerializeField] Button[] pictureLock;
    [SerializeField] Button[] exitExpandPicture;
    [SerializeField] Button homeButton;
    [SerializeField] Button musicButton;
    [SerializeField] Button pictureButton;
    [SerializeField] Button exitPopupButton;

    void Start()
    {
        CheckPictureState();
        SetListener();
    }

    void CheckPictureState()
    {
        for (int i = 0; i < pictureLock.Length; i++)
        {
            if (LevelData.easyModeState[i] == 2)
            {
                pictureLock[i].gameObject.SetActive(false);
            }
        }
    }

    void SetListener()
    {
        LockedPictureButton();
        PictureButton();
        homeButton.onClick.AddListener(HomeButton);
        musicButton.onClick.AddListener(MusicModeButton);
        pictureButton.onClick.AddListener(PictureModeButton);
        exitPopupButton.onClick.AddListener(ExitPopupButton);
    }

    void PictureButton()
    {
        for (int i = 0; i < picture.Length; i++)
        {
            var index = i;
            picture[i].onClick.AddListener(() => ShowExpandPicture(index));
        }

        void ShowExpandPicture(int index)
        {
            foreach (GameObject picture in pictureExpand)
            {
                picture.SetActive(false);
            }

            foreach (Button exitButton in exitExpandPicture)
            {
                exitButton.onClick.AddListener(() => picturePopup.SetActive(false));
            }

            picturePopup.SetActive(true);
            pictureExpand[index].SetActive(true);
        }
    }
    void LockedPictureButton()
    {
        for (int i = 0; i < pictureLock.Length; i++)
        {
            pictureLock[i].onClick.AddListener(ShowLockedPopup);
        }

        void ShowLockedPopup()
        {
            lockedPopup.SetActive(true);
        }
    }

    void HomeButton()
    {
        SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
    }

    void MusicModeButton()
    {
        musicMode.SetActive(true);
        pictureMode.SetActive(false);
    }

    void PictureModeButton()
    {
        musicMode.SetActive(false);
        pictureMode.SetActive(true);
    }

    void ExitPopupButton()
    {
        lockedPopup.SetActive(false);
    }
}
