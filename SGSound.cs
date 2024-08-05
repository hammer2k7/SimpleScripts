using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGSound : MonoBehaviour
{
    public static SGSound instance;


    public AudioSource jumpAudio;
    public AudioSource landAudio;
    public AudioSource leftStepAudio;
    public AudioSource rightStepAudio;

    private void Awake()
    {
        instance = this;
    }
    
    public void PlayjumpAudio()
    {
        jumpAudio.Play();
    }

    public void PlaylandAudio()
    {
        landAudio.Play();
    }

    public void PlayleftStepAudio()
    {
        leftStepAudio.Play();
    }

    public void PlayrightStepAudio()
    {
        rightStepAudio.Play();
    }


}
