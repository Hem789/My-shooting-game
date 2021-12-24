using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingInactive : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            this.gameObject.SetActive(false);
        }
        
    }
}
