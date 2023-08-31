using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    public Image OpenedGuideImage;
    public Sprite OpenSprite;

    public Button LeftButton;
    public Button RightButton;

    public GameObject GuideObject;

    public GameObject[] SlideObject;

    int index = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickedOpenGuide()
    {
        GuideObject.SetActive(true);
        OpenedGuideImage.sprite = OpenSprite;
    }

    public void OnclickClose()
    {
        GuideObject.SetActive(false);
    }

    public void OnclickLeft()
    {
        foreach (var a in SlideObject)
        {
            a.SetActive(false);
        }

        index -= 1;
        if (index < 0)
            index = SlideObject.Length - 1;
        SlideObject[index].SetActive(true);
    }

    public void OnclickRight()
    {
        foreach (var a in SlideObject)
        {
            a.SetActive(false);
        }

        index += 1;
        if (index > SlideObject.Length - 1)
            index = 0;
        SlideObject[index].SetActive(true);
    }
}
