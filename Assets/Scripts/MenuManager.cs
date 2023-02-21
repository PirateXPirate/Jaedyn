using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    FadeController FadeController;

    [Header("Button")]
    [SerializeField] Button galleryButton;
    [SerializeField] Button playButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button goMapSceneButton;

    [SerializeField] GameObject buttonMenu;
    [SerializeField] GameObject headerMenu;
    [SerializeField] GameObject settingScenePrefabs;

    [SerializeField] SettingManager settingManager;
    private void Awake()
    {
        FadeController = gameObject.GetComponent<FadeController>();
        SetListenner();
    }

    void SetListenner()
    {
        galleryButton.onClick.AddListener(GalleryButton);
        playButton.onClick.AddListener(PlayButton);
        settingButton.onClick.AddListener(SettingButton);   
    }

    void GalleryButton()
    {
        Debug.Log("GalleryButton Press!!");
        //SceneManager.LoadSceneAsync("", LoadSceneMode.Single);
    }
    void PlayButton()
    {
        FadeController.isPlayButtonPressed = true;
        StartCoroutine(DeleteMenuUi());
        StartCoroutine(ReadyToGoMapScene());
    }

    void SettingButton()
    {
        Debug.Log("SettingButton Press!!");
        settingScenePrefabs.SetActive(true);
        settingManager.LoadSetting();
    }

    void GoMapSceneButton()
    {
        StartCoroutine(GoMapScene());
    }

    IEnumerator DeleteMenuUi()
    {
        yield return new WaitForSeconds(2.0f);
        buttonMenu.SetActive(false);
        headerMenu.SetActive(false);
        yield return null;
    }

    IEnumerator ReadyToGoMapScene()
    {
        float delayafterpressplay = 7.0f;

        yield return new WaitForSeconds(delayafterpressplay);
        goMapSceneButton.gameObject.SetActive(true);
        goMapSceneButton.onClick.AddListener(GoMapSceneButton);
        yield return null;
    }

    IEnumerator GoMapScene()
    {
        float waitforfade = 2.0f;

        FadeController.isGotoMapScenePressed = true;
        yield return new WaitForSeconds(waitforfade);
        SceneManager.LoadSceneAsync("MapScene", LoadSceneMode.Single);
        yield return null;
    }
}
    
