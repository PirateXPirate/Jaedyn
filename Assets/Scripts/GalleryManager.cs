using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    FadeController FadeController;

    public GameObject picturePopup;
    public GameObject[] pictureExpand;
    public GameObject lockedPopup;

    [Header("Button")]
    public Button[] picture;
    public Button[] pictureLock;
    public Button homeButton;
    public Button musicButton;
    public Button exitPopupButton;

    void Start()
    {
        FadeController = GetComponent<FadeController>();
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
        musicButton.onClick.AddListener(MusicButton);
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
        StartCoroutine(GoNextScene("MainMenuScene"));
    }

    void MusicButton()
    {
        Debug.Log("MusicButton is pressed!");
        //SceneManager.LoadScene("");
    }

    void ExitPopupButton()
    {
        lockedPopup.SetActive(false);
    }

    IEnumerator GoNextScene(string sceneName)
    {
        float waitforfade = 2.0f;

        FadeController.isGotoNextScenePressed = true;
        yield return new WaitForSeconds(waitforfade);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return null;
    }
}
