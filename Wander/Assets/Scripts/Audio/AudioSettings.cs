using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour {

     FMOD.Studio.EventInstance SFXVolumeTestEvent;

     FMOD.Studio.Bus Music;
     FMOD.Studio.Bus SFX;
     FMOD.Studio.Bus Master;
     [Range(0.0f, 1f)]
     public float MusicVolume = 1f;
     [Range(0.0f, 1f)]
     public float SFXVolume = 1f;
     [Range(0.0f, 1f)]
     public float MasterVolume = 1f;

     void Awake ()
     {
          DontDestroyOnLoad(this.gameObject);
          Music = FMODUnity.RuntimeManager.GetBus ("bus:/Master/Music");
          SFX = FMODUnity.RuntimeManager.GetBus ("bus:/Master/SFX");
          Master = FMODUnity.RuntimeManager.GetBus ("bus:/Master");
          SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance ("event:/UI/SFXTestEvent");
     }

     void Update () 
     {
          Music.setVolume (MusicVolume);
          SFX.setVolume (SFXVolume);
          Master.setVolume (MasterVolume);
     }

     public void MasterVolumeLevel (float newMasterVolume)
     {
          MasterVolume = newMasterVolume;
     }

     public void MusicVolumeLevel (float newMusicVolume)
     {
          MusicVolume = newMusicVolume;
     }

     public void SFXVolumeLevel (float newSFXVolume)
     {
          SFXVolume = newSFXVolume;

          FMOD.Studio.PLAYBACK_STATE PbState;
          SFXVolumeTestEvent.getPlaybackState (out PbState);
          if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
          {
               SFXVolumeTestEvent.start ();
          }
     }

     
}