using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    DummyLoader dummyLoader;

    [Header("Button")]
    [SerializeField] Button galleryButton;
    [SerializeField] Button playButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button goMapSceneButton;

    [SerializeField] GameObject buttonMenu;
    [SerializeField] GameObject headerMenu;
    [SerializeField] GameObject settingScenePrefab;
    [SerializeField] GameObject dummyLoaderPrefab;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] CanvasGroup dialogFaderGroup;

    [SerializeField] SettingManager settingManager;
    [SerializeField] Image LoadingBg;
    [SerializeField] Sprite[] LoadingImg;

    [SerializeField] AudioClip MenuSound;
    private void Awake()
    {
        if (Utils.soundManager)
            Utils.soundManager.PlayLoop(MenuSound);

        LevelData.LoadLevelStateData();
        dummyLoader = dummyLoaderPrefab.GetComponent<DummyLoader>();
        SetListenner();

        int randomValue = Random.Range(0, 4);
        LoadingBg.sprite = LoadingImg[randomValue];
    }

    void SetListenner()
    {
        galleryButton.onClick.AddListener(GalleryButton);
        playButton.onClick.AddListener(PlayButton);
        settingButton.onClick.AddListener(SettingButton);   
    }

    void GalleryButton()
    {
        SceneManager.LoadSceneAsync("GalleryScene", LoadSceneMode.Single);
    }
    void PlayButton()
    {
        DeleteMenuUi();
        StartCoroutine(LoadingAndDialogProgress());
    }

    void SettingButton()
    {
        settingScenePrefab.SetActive(true);
        settingManager.LoadSetting();
    }
    private void DeleteMenuUi()
    {
        buttonMenu.SetActive(false);
        headerMenu.SetActive(false);
    }

    IEnumerator LoadingAndDialogProgress()
    {
        float loadingDuration = dummyLoader.loadingDuration;

        dummyLoaderPrefab.SetActive(true);
        yield return new WaitForSeconds(loadingDuration);
        dummyLoaderPrefab.SetActive(false);
        SceneManager.LoadSceneAsync("MapScene", LoadSceneMode.Single);
    }
}
    
