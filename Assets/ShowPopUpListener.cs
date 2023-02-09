using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPopUpListener : MonoBehaviour, MMEventListener<TopDownEngineEvent>
{
    [SerializeField] private GameObject popupFrame;
    [SerializeField] private TextMeshProUGUI titleTextField;
    [SerializeField] private TextMeshProUGUI detailTextField;
    [SerializeField] private string titleText;
    [SerializeField] private string detailText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            this.MMEventStartListening<TopDownEngineEvent>();



        }

    }

    void OnEnable()
    {

    }
    void OnDisable()
    {
        this.MMEventStopListening<TopDownEngineEvent>();
    }
    public void OnMMEvent(TopDownEngineEvent topdownEngineEvent)
    {
        if (topdownEngineEvent.EventType == TopDownEngineEventTypes.CharacterSwitch)
        {
            ShowPopup();
        }

    }

    private void ShowPopup()
    {
        gameObject.SetActive(false);
        this.MMEventStopListening<TopDownEngineEvent>();
        popupFrame.SetActive(true);
        titleTextField.text = titleText;
        detailTextField.text = detailText;

    }

}
