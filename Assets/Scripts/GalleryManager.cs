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

    [SerializeField] AudioClip GalleryBgSound;

    [SerializeField] AudioClip[] MusicSound;

    [SerializeField] Button endCreditButton;

    private void Awake()
    {
        LevelData.LoadLevelStateData();
    }
    void Start()
    {
        CheckPictureState();
        SetListener();
    }

    void CheckPictureState()
    {
        pictureLock[0].gameObject.SetActive(!LevelData.isTutorialCompleteWithPicture);

        for (int i = 1; i < pictureLock.Length; i++)
        {
            if(i % 2 == 1 && LevelData.easyModeState[i] == 2)
            {
               pictureLock[i].gameObject.SetActive(false);
            }

            if (i % 2 == 0 && LevelData.hardModeState[i] == 2)
            {
               pictureLock[i].gameObject.SetActive(false);
            }
        }
    }

    void SetListener()
    {
        LockedPictureButton();
        PictureButton();
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(HomeButton);


        if (PlayerPrefs.GetInt("CanEnterMusic")==1)
        {
            musicButton.onClick.RemoveAllListeners();
            musicButton.onClick.AddListener(MusicModeButton);

            endCreditButton.onClick.RemoveAllListeners();
            endCreditButton.onClick.AddListener(OnClickEndCredit);
        }
        else
        {
            musicButton.gameObject.SetActive(false);
        }
    

        pictureButton.onClick.RemoveAllListeners();
        pictureButton.onClick.AddListener(PictureModeButton);

        exitPopupButton.onClick.RemoveAllListeners();
        exitPopupButton.onClick.AddListener(ExitPopupButton);
    }

    private void OnClickEndCredit()
    {
        SceneManager.LoadScene("EndScene3");
    }

    public void PlayMusic(int index)
    {
        if (Utils.soundManager)
            Utils.soundManager.PlayLoop(MusicSound[index]);
    }
    void PictureButton()
    {
        if (Utils.soundManager) 
        Utils.soundManager.PlayLoop(GalleryBgSound);
        for (int i = 0; i < picture.Length; i++)
        {
            var index = i;
            picture[i].onClick.RemoveAllListeners();
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
                exitButton.onClick.RemoveAllListeners();
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
            pictureLock[i].onClick.RemoveAllListeners();
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
