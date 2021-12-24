using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class volume : MonoBehaviour
{
    public Slider slide;
    private AudioSource sound;
    static private float vol=1;
    public float volumeCofficient;
    // Start is called before the first frame update
    void Awake()
    {
        sound=GetComponent<AudioSource>();
        sound.volume=vol*volumeCofficient;
        
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume=vol*volumeCofficient;
        if(slide!=null)
        {
            slide.value=vol;
        }

    }
    public void slider(float a)
    {
        vol=a;
       // Debug.Log("success");
    }
    
}
