﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _MANAGE_AUDIO;
    private AudioClip[] _audioClips;
    private static AudioSource _audioSource;

    public void Awake()
    {
        InitializeVars();
    }
    public AudioManager()
    {
        if (_MANAGE_AUDIO == null)
        {
            _MANAGE_AUDIO = this;
        }
        else
        {
            print("Audio Manager already instantiated");
        }
    }

    public void InitializeVars()
    {
        _audioClips = new AudioClip[] {(AudioClip)Resources.Load("Audio/menu_selection_click"),
            (AudioClip)Resources.Load("Audio/menu_selection_hover"),
            (AudioClip) Resources.Load("Audio/StarWarsMainTheme"),
            (AudioClip)Resources.Load("Audio/TheImperialMarch"),
            (AudioClip)Resources.Load("Audio/Blaster-Solo"),
            (AudioClip)Resources.Load("Audio/hansolo_badfeeling"),
            (AudioClip)Resources.Load("Audio/luke_greetings")};
        _audioSource = gameObject.AddComponent<AudioSource>();
        print(_audioSource);
        _audioSource.loop = true;
        DontDestroyOnLoad(gameObject);
    }

    public void SwitchScene()
    {
        string scene_name = SceneManager.GetActiveScene().name;
        switch (scene_name)
        {
            case "_MainMenu":
                if (_audioSource.clip == null || !_audioSource.clip.name.Equals(_audioClips[2].name)){
                    FadeOutAudio(1);
                    _audioSource.clip = _audioClips[2];
                    _audioSource.PlayDelayed(1);
                }
                break;
            case "_Scene_0":
                if(_audioSource.clip == null || !_audioSource.clip.name.Equals(_audioClips[3].name))
                {
                    FadeOutAudio(3);
                    _audioSource.clip = _audioClips[3];
                    _audioSource.PlayDelayed(1);
                }
                break;
        }
    }

    public void PlayHover()
    {
        _audioSource.PlayOneShot(_audioClips[1]);
    }

    public void PlayClick()
    {
        _audioSource.PlayOneShot(_audioClips[0]);
    }

    public void FadeOutAudio(int fadeTime)
    {
        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
        }
        _audioSource.Stop();
        _audioSource.volume = startVolume;
    }

    public void CharacterSelect(string name)
    {
        switch (name)
        {
            case "han":
                _audioSource.PlayOneShot(_audioClips[5]);
                break;
            case "luke":
                _audioSource.PlayOneShot(_audioClips[6]);
                break;
        }
    }

    public static AudioManager AUDIO_MANAGER
    {
        get
        {
            return _MANAGE_AUDIO;
        }
    }

    public void Shoot()
    {
        _audioSource.PlayOneShot(_audioClips[4]);
    }
}