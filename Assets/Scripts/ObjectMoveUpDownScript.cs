using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveUpDownScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y", transform.localPosition.y+0.3f);
        ht.Add("isLocal", true);
        ht.Add("time", 2f);
        ht.Add("easetype", iTween.EaseType.easeInOutQuad);
        ht.Add("looptype", iTween.LoopType.pingPong);
        iTween.MoveTo(gameObject, ht);
    }

    
}
