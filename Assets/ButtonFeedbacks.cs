using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFeedbacks : MonoBehaviour
{
    

    [Header("EasyMode Level")]
    [SerializeField] Button tutorial;
    [SerializeField] Button easyLevel1;
    [SerializeField] Button easyLevel2;
    [SerializeField] Button easyLevel3;
    [SerializeField] Button easyLevel4;
    [SerializeField] Button easyLevel5;
    [SerializeField] Button easyLevel6;
    [SerializeField] Button easyLevel7;
    [SerializeField] Button easyLevel8;
    [SerializeField] Button easyLevel9;

    [Header("HardMode Level")]
    [SerializeField] Button hardLevel1;
    [SerializeField] Button hardLevel2;
    [SerializeField] Button hardLevel3;
    [SerializeField] Button hardLevel4;
    [SerializeField] Button hardLevel5;
    [SerializeField] Button hardLevel6;
    [SerializeField] Button hardLevel7;
    [SerializeField] Button hardLevel8;
    [SerializeField] Button hardLevel9;

    [Header("Play Button")]
    public Button playButton;
    public Button blockPlayButton;

    void Awake()
    {
        SetListenner();
    }
    void SetListenner()
    {
        tutorial.onClick.AddListener(TutorialButton);
        easyLevel1.onClick.AddListener(EasyLevel1Button);
        easyLevel2.onClick.AddListener(EasyLevel2Button);
        easyLevel3.onClick.AddListener(EasyLevel3Button);
        easyLevel4.onClick.AddListener(EasyLevel4Button);
        easyLevel5.onClick.AddListener(EasyLevel5Button);
        easyLevel6.onClick.AddListener(EasyLevel6Button);
        easyLevel7.onClick.AddListener(EasyLevel7Button);
        easyLevel8.onClick.AddListener(EasyLevel8Button);
        easyLevel9.onClick.AddListener(EasyLevel9Button);

        hardLevel1.onClick.AddListener(HardLevel1Button);
        hardLevel2.onClick.AddListener(HardLevel2Button);
        hardLevel3.onClick.AddListener(HardLevel3Button);
        hardLevel4.onClick.AddListener(HardLevel4Button);
        hardLevel5.onClick.AddListener(HardLevel5Button);
        hardLevel6.onClick.AddListener(HardLevel6Button);
        hardLevel7.onClick.AddListener(HardLevel7Button);
        hardLevel8.onClick.AddListener(HardLevel8Button);
        hardLevel9.onClick.AddListener(HardLevel9Button);
    }

    #region -Easy mode event button-
    void TutorialButton()
    {
        ResetAllSize();
        SizeExpand(tutorial);
        StateChecker(tutorial);
    }

    void EasyLevel1Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel1);
        StateChecker(easyLevel1);
    }
    void EasyLevel2Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel2);
        StateChecker(easyLevel2);
    }
    void EasyLevel3Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel3);
        StateChecker(easyLevel3);
    }
    void EasyLevel4Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel4);
        StateChecker(easyLevel4);
    }
    void EasyLevel5Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel5);
        StateChecker(easyLevel5);
    }
    void EasyLevel6Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel6);
        StateChecker(easyLevel6);
    }
    void EasyLevel7Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel7);
        StateChecker(easyLevel7);
    }
    void EasyLevel8Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel8);
        StateChecker(easyLevel8);
    }

    void EasyLevel9Button()
    {
        ResetAllSize();
        SizeExpand(easyLevel9);
        StateChecker(easyLevel9);
    }
    #endregion

    #region -Hard mode event button-
    void HardLevel1Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel1);
        StateChecker(hardLevel1);

    }
    void HardLevel2Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel2);
        StateChecker(hardLevel2);
    }
    void HardLevel3Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel3);
        StateChecker(hardLevel3);
    }
    void HardLevel4Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel4);
        StateChecker(hardLevel4);
    }
    void HardLevel5Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel5);
        StateChecker(hardLevel5);
    }
    void HardLevel6Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel6);
        StateChecker(hardLevel6);
    }
    void HardLevel7Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel7);
        StateChecker(hardLevel7);
    }
    void HardLevel8Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel8);
        StateChecker(hardLevel8);
    }
    void HardLevel9Button()
    {
        ResetAllSize();
        SizeExpand(hardLevel9);
        StateChecker(hardLevel9);
    }


    #endregion

    void SizeExpand(Button button)
    {
        button.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
    }

    public void ResetAllSize()
    {
        SizeReSet(tutorial);
        SizeReSet(easyLevel1);
        SizeReSet(easyLevel2);
        SizeReSet(easyLevel3);
        SizeReSet(easyLevel4);
        SizeReSet(easyLevel5);
        SizeReSet(easyLevel6);
        SizeReSet(easyLevel7);
        SizeReSet(easyLevel8);
        SizeReSet(easyLevel9);

        SizeReSet(hardLevel1);
        SizeReSet(hardLevel2);
        SizeReSet(hardLevel3);
        SizeReSet(hardLevel4);
        SizeReSet(hardLevel5);
        SizeReSet(hardLevel6);
        SizeReSet(hardLevel7);
        SizeReSet(hardLevel8);
        SizeReSet(hardLevel9);
    }
    void SizeReSet(Button button)
    {
        button.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    void StateChecker(Button parent)
    {
        if (parent.transform.Find("BG_Lock").gameObject.activeSelf)
        {
            blockPlayButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
        }
        else
        {
            blockPlayButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }
            
    }
}
