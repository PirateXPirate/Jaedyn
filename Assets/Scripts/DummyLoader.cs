using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DummyLoader : MonoBehaviour
{
    [SerializeField] private Slider loadProgressBar;
    [SerializeField] private TextMeshProUGUI loadingNumberText;
    public float loadingDuration = 3;

    private void OnDisable()
    {
        loadProgressBar.value = 0;
    }

    private void OnEnable()
    {
        StartDummyLoader();
    }

    private void Update()
    {
        loadingNumberText.text = $"Loading... {(int)loadProgressBar.value} %";
    }

    private void StartDummyLoader()
    {
        loadProgressBar.DOValue(100, loadingDuration);
        
    }
}
