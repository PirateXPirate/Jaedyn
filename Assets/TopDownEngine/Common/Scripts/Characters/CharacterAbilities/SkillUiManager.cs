using Suriyun.MCS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUiManager : MonoBehaviour
{
    [SerializeField] private UniversalButton SkillButton;
    [SerializeField] private UniversalButton ActionButton;

    [SerializeField] Image SkillButtonOverlayImage;
    [SerializeField] Image ActionButtonOverlayImage;

    [SerializeField] Image SkillImage;

    bool isSkillCountdown = false;
    bool isActionCountdown = false;
    float countingSkillNumber = 0;
    float countingActionNumber = 0;
    float skillCoolDown;
    float actionCoolDown;
    public void SetSkillCooldown(float cooldown)
    {
        SkillButtonOverlayImage.fillAmount = 1;
        SkillButtonOverlayImage.GetComponentInParent<UniversalButton>().SetActiveState(false);
        SkillImage.fillAmount = 1;

        isSkillCountdown = true;
        skillCoolDown = cooldown;
        countingSkillNumber = cooldown;
        SetActionCooldown(1);


    }

    public void SetActionCooldown(float cooldown)
    {
        ActionButtonOverlayImage.fillAmount = 1;
        ActionButtonOverlayImage.GetComponentInParent<UniversalButton>().SetActiveState(false);
        isActionCountdown = true;
        actionCoolDown = cooldown;
        countingActionNumber = cooldown;


    }
    private void Update()
    {
        if (isSkillCountdown)
        {
            float tik = (1 / skillCoolDown) * countingSkillNumber;
            countingSkillNumber -= Time.deltaTime;
            SkillButtonOverlayImage.fillAmount = tik;
            SkillImage.fillAmount = tik;
            if (SkillButtonOverlayImage.fillAmount <=0)
            {
                StopSkillcount();
            }
        }

        if (isActionCountdown)
        {
            float tik = (1 / actionCoolDown) * countingActionNumber;
            countingActionNumber -= Time.deltaTime;
            ActionButtonOverlayImage.fillAmount = tik;
            
            if (ActionButtonOverlayImage.fillAmount <= 0)
            {
                StopActioncount();
            }
        }
    }
    private void StopActioncount()
    {
        ActionButtonOverlayImage.GetComponentInParent<UniversalButton>().SetActiveState(true);
        isActionCountdown = false;
        countingActionNumber = 0;


    }
    private void StopSkillcount()
    {
        SkillButtonOverlayImage.GetComponentInParent<UniversalButton>().SetActiveState(true);
        SkillImage.fillAmount = 1;
        isSkillCountdown = false;
        countingSkillNumber = 0;


    }
}
