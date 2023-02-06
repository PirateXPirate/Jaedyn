using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform position;
    public Rigidbody rig;
    public float speed=100f;
    public Vector3 pos;

    void Start()
    {
        //pos = gameObject.transform.position;
        //Destroy(rig);
        //rig.useGravity = false;
        //rig.isKinematic = true;
        //rig.detectCollisions = false;

        Hashtable ht = new Hashtable();
        //ht.Add("position", position.position);
        //ht.Add("time", 2f);
        //ht.Add("easeType", iTween.EaseType.linear);
        //iTween.MoveTo(gameObject, ht);
        //ht.Add("x", 0f);
        //ht.Add("y", 0f);
        //ht.Add("z", 0f);
        //ht.Add("time", 2f);
        //ht.Add("easeType", iTween.EaseType.linear);
        //iTween.ScaleTo(gameObject, ht);


        //gameObject.transform.position = position.position;
    }

    void Update() {
        //transform.position = Vector3.Lerp(pos, position.position, Time.time);
    }
}

