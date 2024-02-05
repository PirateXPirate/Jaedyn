using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    [SerializeField] GameObject Logo1;
    IEnumerator Start()
    {
        Logo1.GetComponent<Image>().DOFade(1, 3);
        yield return new WaitForSeconds(3);
        Logo1.GetComponent<Image>().DOFade(0, 3);
        yield return new WaitForSeconds(3);
        Logo1.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
