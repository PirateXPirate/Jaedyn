using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlAttackScript : MonoBehaviour
{
    
    void OnEnable()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //Owl , Tiger Attack
        if (other.tag == "Boss") {
            //Attack Boss
            print("Tiger Attack");
            other.gameObject.GetComponent<BossScript>().GotAttack();
        }
    }
}