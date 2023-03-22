using DG.Tweening;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekcCollide : MonoBehaviour
{

    public GameObject hitObstacle;

    public GameObject particle;
    public AudioClip sound;
    public string tagName;

    public GameObject activeObject;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Trunk"))
        {
            hitObstacle = collision.gameObject;
            DOTween.KillAll();
            LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = true;
            LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
            LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = true;
            LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>().enabled = true;
            LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().ScriptDrivenInput = false;
            LevelManager.Instance.Players[0].GetComponent<Collider>().enabled = true;
            var coll = GetComponentsInChildren<Collider>();
            foreach (var col in coll)
            {
                // col.enabled = true;
            }
        }
        if (!string.IsNullOrEmpty(tagName))
        {
            if (collision.transform.CompareTag(tagName))
            {
                if (particle)
                {
                    gameObject.SetActive(false);
                    Instantiate(particle, transform.position, Quaternion.identity);


                }
                if (activeObject)
                    activeObject.SetActive(true);
                if (sound)
                    Utils.soundManager.PlayFX(sound);
            }
        }
       
    }
    

}
