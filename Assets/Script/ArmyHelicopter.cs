﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyHelicopter : MonoBehaviour
{
    private RaycastHit hit,hit2;
    public CharacterController Controller;
    private Vector3 Direction;
    private GameObject Player;
    public GameObject fan,backfan, Left,Right,bullet,fire,effect1,effect2;
    private float delay=0.1F,bulletCount=50, fireGap,decrease,Destr=15,seenCount=0;
    public float Speed,health;
    public AudioSource firing,flying;
    private GameManager manager;
    void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.tag=="Bullet")
        {
            health--;
        }
        if(a.gameObject.tag=="BigBullet")
        {
            health-=4;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
    fire.SetActive(false);
    manager=FindObjectOfType<GameManager>();
    effect1.SetActive(false);
    effect2.SetActive(false);
    }
    void OnEnable()
    {
        health=100;
        fire.SetActive(false);
        firing.enabled=true;
        delay=0.1F;
        bulletCount=50;
        seenCount=0;
        Destr=15;
    }

    // Update is called once per frame
    void Update()
    {
        if(seenCount<16)
        {
        seenCount+=Time.deltaTime;
        }
        
        if(manager.Pause==true)
        {
            flying.enabled=false;
            firing.enabled=false;
        }
        if(manager.Pause==false)
    {
        flying.enabled=true;
        if(health>0)
        {
            //firing.enabled=false;
       
        fan.transform.Rotate(0,30,0);
        backfan.transform.Rotate(20,0,0);
        Player=GameObject.FindWithTag("Player");
        Direction=Player.transform.position-transform.position;
        if(Physics.Raycast(transform.position,Direction,out hit2,250))
        {
            if(hit2.transform.gameObject.tag=="Player")
            {
                seenCount=0;
            }
        }
        if(transform.position.y<Player.transform.position.y)
        {
            transform.position+=new Vector3(0,0.1F,0);
        }
        if(transform.position.y<=15)
        {
            Direction.y=0;
        }
        if(Direction.magnitude<=2000 && Direction.magnitude>50)
        {
            Quaternion look=Quaternion.LookRotation(new Vector3(Direction.x,0,Direction.z));
            Quaternion look1=Quaternion.Euler(15,look.eulerAngles.y,look.eulerAngles.z);
            transform.rotation=Quaternion.Slerp(transform.rotation,look1,0.05F);
            Controller.Move(Direction*Time.deltaTime*Speed);
        }
        if(Direction.magnitude<=55)
        {
            Quaternion look=Quaternion.LookRotation(new Vector3(Direction.x,0,Direction.z));
            transform.rotation=Quaternion.Slerp(transform.rotation,look,1F);
            Right.transform.LookAt(Player.transform.position+new Vector3(0,1,0));
            Left.transform.LookAt(Player.transform.position+ new Vector3(0,1,0));
            if(delay<=0 && fireGap<=0 && seenCount<=15)
            {
            MF_AutoPool.Spawn(bullet,Left.transform.position,Left.transform.rotation);
            MF_AutoPool.Spawn(bullet,Right.transform.position,Right.transform.rotation);
            firing.enabled=true;
            effect1.SetActive(true);
            effect2.SetActive(true);
           // MF_AutoPool.Spawn(effect,Left.transform.position-transform.forward*0.1F,Left.transform.rotation);
            //MF_AutoPool.Spawn(effect,Right.transform.position-transform.forward*0.1F,Right.transform.rotation);
            bulletCount--;
            delay=0.1F;
            }
            if(seenCount>15)
            {
                MF_AutoPool.Despawn(gameObject);
                //Quaternion look=Quaternion.LookRotation(new Vector3(Direction.x,0,Direction.z));
                //transform.rotation=Quaternion.Slerp(transform.rotation,look*Quaternion.Euler(0,180,0),1F);
                
            }
            
            if(delay>0)
            {
                delay-=Time.deltaTime;
            }
            if(bulletCount<=0)
            {
                effect1.SetActive(false);
                effect2.SetActive(false);
                fireGap=5;
                bulletCount=50;
            }
            if(fireGap>0)
            {
                firing.enabled=false;
                fireGap-=Time.deltaTime;
            }

        }
        }
        else
        {
            if(decrease==0)
            {
            manager.enemy();
            manager.enemy();
            decrease++;
            }
            if(Destr<=0)
            {
                // Destr=15;
                 fire.SetActive(false);
                // health=100;
                  MF_AutoPool.Despawn(gameObject);
            }
            if(Destr>0)
            {
                Destr-=Time.deltaTime;
            
            //Destroy(gameObject,15);
            
            fire.SetActive(true);
            fire.transform.rotation=Quaternion.Euler(0,0,0);
            }
            
            firing.enabled=false;
                        //MF_AutoPool.Spawn(fire,fan.transform.position+transform.forward*2,transform.rotation);
            if(transform.position.y>-10)
            {
             if(Physics.Raycast(transform.position,new Vector3(0,-1,0),out hit,50))
             {   
            if(hit.transform.gameObject.tag!="Ground" || hit.distance>.5F)
            {
            fan.transform.Rotate(0,10,0);
            backfan.transform.Rotate(10,0,0);
            transform.position+=transform.forward* .1F-transform.up*0.1F+new Vector3(0,-.1F,0);
            transform.Rotate(-1,2.5F,0F);
            }
            }
            }
            else
            {
                flying.enabled=false;
                fire.SetActive(false);
            }

            
        }
    }    
    }
}