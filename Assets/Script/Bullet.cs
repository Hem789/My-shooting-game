using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float time,timeStore;
    private Vector3 Direction;
    public float Speed;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider b)
    {
        //if(b.gameObject.tag!="Bullet" && b.gameObject.tag!="Bullet0" && b.gameObject.tag!="BigBullet" && b.gameObject.tag!="BigBullet2")
        //{
         //   rigidBody.useGravity=true;
            //rigidBody.isKinematic=true;
            MF_AutoPool.Despawn(gameObject);
            //Destroy(gameObject);
        //}
    }
    void OnEnable()
    {
        
        rigidBody.useGravity=false;
        time=timeStore;
    }
    void OnTriggerStay(Collider b)
    {
        //if(b.gameObject.tag!="Bullet" && b.gameObject.tag!="Bullet0" && b.gameObject.tag!="BigBullet" && b.gameObject.tag!="BigBullet2")
        //{
            rigidBody.useGravity=true;
           // rigidBody.isKinematic=true;
            MF_AutoPool.Despawn(gameObject);
            //Destroy(gameObject);
        //}
    }
    void Start()
    {
        //time=15;
        //timeStore=time;
        rigidBody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidBody.useGravity==false)
        {
        Direction=transform.forward*Speed;
        //rigidBody.AddForce(Direction*100000);
        rigidBody.velocity=Direction;
        //Destroy(gameObject,time);
        if(time<=0)
        {
            MF_AutoPool.Despawn(gameObject,time);
        }
        if(time>=0)
        {
            time-=Time.deltaTime;
        }
        }
    }
}
