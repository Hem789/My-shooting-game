using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
  //  public GameObject box;
    public Transform pivot,piv;
    public float motorForce,BrakeForce, steer;
    public Transform FLW,BLW,FRW,BRW;
    public FixedButton right, left,up ,down,brake;
    public GameObject Light1,Light2,carcam,revMotion,maxRunSound;
    public AudioSource motion;
    private bool revStarter=false;
    private float h=0,v=0,maxRunTime=4.8F;
    private GameManager manager;
    public WheelCollider FL,FR,BL,BR;// Start is called before the first frame update
    public void wheel(WheelCollider a,Transform b)
    {
        Quaternion rot=b.rotation;
        Vector3 pos=b.position;
        a.GetWorldPose(out pos,out rot);
        b.position=pos;
        b.rotation=rot;        
    }
    void Awake()
    {
        manager=FindObjectOfType<GameManager>();
    }
    void Update ()
    {
        if(manager.Pause==true)
            { 
        motion.enabled=false;
        revMotion.SetActive(false);
            }
        piv.transform.rotation=Quaternion.Slerp(piv.transform.rotation,Quaternion.Euler(0,transform.rotation.eulerAngles.y,0),.4F*Time.deltaTime);
    }
    void OnEnabled()
    {
        revStarter=false;
        
    }

    // Upfixeddate is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position=carcam.transform.position;
        Camera.main.transform.rotation=carcam.transform.rotation;
        pivot.transform.parent=null;
        pivot.transform.position=transform.position;
        Light1.SetActive(false);
        Light2.SetActive(false);
        
        h=0;
        v=0;
        if(up.Pressed)
        {
            v=1;
            if(maxRunTime>=0)
            {
            motion.enabled=true;
            revMotion.SetActive(false);
            maxRunTime-=Time.fixedDeltaTime;
            }
            else
            {
                motion.enabled=false;
                maxRunSound.SetActive(true);
            }
           if(manager.Pause==false)
            { 
            
            revStarter=true;

            }
        }
        if(!up.Pressed)
        {
            motion.enabled=false;
            if(revStarter==true)
            {
                maxRunTime=4.8F;
                maxRunSound.SetActive(false);
            revMotion.SetActive(true);
            revStarter=false;
            }
        }
        
        if(down.Pressed)
        {
            v=-.5F;
        Light1.SetActive(true);
        Light2.SetActive(true);
        revMotion.SetActive(false);
        }
        if(right.Pressed)
        {
            h=1;
        }
        if(left.Pressed)
        {
            h=-1;
        }
        FL.steerAngle=h*steer;
        FR.steerAngle=h*steer;
        if(v>0 && BL.motorTorque<0)
        {
            BL.motorTorque=0;
            BR.motorTorque=0;
        }    
        if(v<0 && BL.motorTorque>0)
        {
            BL.motorTorque=0;
            BR.motorTorque=0;
        }
        BL.motorTorque=v*motorForce;
        BR.motorTorque=v*motorForce;
        
        if(Input.GetAxis("Jump")>0|| brake.Pressed==true)
        {
            revMotion.SetActive(false);
            BL.brakeTorque=BrakeForce;
            BR.brakeTorque=BrakeForce;
            FL.brakeTorque=BrakeForce;
            FR.brakeTorque=BrakeForce;
            Light1.SetActive(true);
            Light2.SetActive(true);
        }
        else
        {
            BL.brakeTorque=0;
            BR.brakeTorque=0;
            FL.brakeTorque=0;
            FR.brakeTorque=0;
            
        }
        wheel(FL,FLW);
        wheel(FR,FRW);
        wheel(BL,BLW);
        wheel(BR,BRW);
       
       
    }
    
   
}
