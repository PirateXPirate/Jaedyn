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
    private void Awake()
    {
        LevelData.LoadLevelStateData();
        dummyLoader = dummyLoaderPrefab.GetComponent<DummyLoader>();
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
        GoNextScene("GalleryScene");
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

    private void GoMapSceneButton()
    {
        GoNextScene("MapScene");
    }

    private void DeleteMenuUi()
    {
        buttonMenu.SetActive(false);
        headerMenu.SetActive(false);
    }

    IEnumerator LoadingAndDialogProgress()
    {
        float loadingDuration = dummyLoader.loadingDuration;
        float dialogFadeDuration = 2; //block player to spam click before read some dialog?

        dummyLoaderPrefab.SetActive(true);
        yield return new WaitForSeconds(loadingDuration);
        dummyLoaderPrefab.SetActive(false);

        dialogPanel.SetActive(true);
        dialogFaderGroup.DOFade(1, dialogFadeDuration);
        yield return new WaitForSeconds(dialogFadeDuration);

        goMapSceneButton.gameObject.SetActive(true);
        goMapSceneButton.onClick.AddListener(GoMapSceneButton);
        yield return null;
    }

    private void GoNextScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
    
