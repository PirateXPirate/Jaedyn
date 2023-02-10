using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthUIManager : MonoBehaviour
{
    [SerializeField] Sprite[] heartSprites;
    [SerializeField] Image[] heartUIImage;
    


    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void SetHP(int value)
    {
        float secment = value / 5;

        for (int i = 0; i < heartUIImage.Length; i++)
        {
            heartUIImage[i].sprite = heartSprites[2];
        }

        if (value == 100)
        {
            for (int i = 0; i < heartUIImage.Length; i++)
            {
                heartUIImage[i].sprite = heartSprites[0];
            }
        }
        else if (value > 80)
        {
            for (int i = 0; i < 5; i++)
            {
                heartUIImage[i].sprite = heartSprites[0];
            }
        }
        else if (value > 60)
        {
            for (int i = 0; i < 4; i++)
            {
                heartUIImage[i].sprite = heartSprites[0];
            }
        }
        else if (value > 40)
        {
            for (int i = 0; i < 3; i++)
            {
                heartUIImage[i].sprite = heartSprites[0];
            }
        }
        else if (value > 20)
        {
            for (int i = 0; i < 2; i++)
            {
                heartUIImage[i].sprite = heartSprites[0];
            }
        }
        else if (value > 0)
        {
            heartUIImage[0].sprite = heartSprites[0];
        }


    }
   
}
