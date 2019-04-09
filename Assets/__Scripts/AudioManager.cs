using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _MANAGE_AUDIO;
    private AudioClip[] _audioClips;
    private AudioClip[] _darthVaderCommon;
    private AudioClip[] _darthVaderDefeatCommon;
    private static AudioSource _audioSource;
    private AudioClip _darthVaderLuke;
    private AudioClip _darthVaderLukeDefeat;

    private bool lukeChosen = true;

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
    }

    public void InitializeVars()
    {
        _audioClips = new AudioClip[] {(AudioClip)Resources.Load("Audio/menu_selection_click"),
            (AudioClip)Resources.Load("Audio/menu_selection_hover"),
            (AudioClip) Resources.Load("Audio/StarWarsMainTheme"),
            (AudioClip)Resources.Load("Audio/TheImperialMarch"),
            (AudioClip)Resources.Load("Audio/Blaster-Solo"),
            (AudioClip)Resources.Load("Audio/hansolo_gorgeousguy"),
            (AudioClip)Resources.Load("Audio/luke_greetings"),
            (AudioClip)Resources.Load("Audio/blaster_multiple")};

        _darthVaderCommon = new AudioClip[] { (AudioClip)Resources.Load("Audio/darthvader_dontmakeme"),
        (AudioClip)Resources.Load("Audio/darthvader_expectingyou"),
        (AudioClip)Resources.Load("Audio/darthvader_giveyourself"),
        (AudioClip)Resources.Load("Audio/darthvader_lackoffaith") };

        _darthVaderDefeatCommon = new AudioClip[] { (AudioClip)Resources.Load("Audio/darthvader_technological"),
        (AudioClip)Resources.Load("Audio/darthvader_honored") };

        _darthVaderLuke = Resources.Load("darthvader_pointless") as AudioClip;
        _darthVaderLukeDefeat = Resources.Load("darthvader_taughtyouwell") as AudioClip;

        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = true;
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<AudioListener>();
        }

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
                lukeChosen = false;
                break;
            case "luke":
                _audioSource.PlayOneShot(_audioClips[6]);
                lukeChosen = true;
                break;
        }
    }

    public int Mute()
    {
        if(_audioSource.volume == 0.0f)
        {
            _audioSource.volume = 0.7f;
            return 1;
        }
        else
        {
            _audioSource.volume = 0;
            return 0;
        }
    }

    public static AudioManager AUDIO_MANAGER
    {
        get
        {
            return _MANAGE_AUDIO;
        }
    }

    public void Shoot(string type)
    {
        switch (type)
        {
            case "normal":
                _audioSource.PlayOneShot(_audioClips[4]);
                break;
            case "spread":
                _audioSource.PlayOneShot(_audioClips[7]);
                break;
        }
    }

    public void PlayDarthVaderQuote()
    {
        if(Random.Range(0, 100) < 20 && lukeChosen)
        {
            _audioSource.PlayOneShot(_darthVaderLuke);
        }
        else
        {
            _audioSource.PlayOneShot(_darthVaderCommon[((int)Random.Range(0, 100)) % _darthVaderCommon.Length]);
        }
    }

    public void PlayDarthVaderDefeatQuote()
    {
        if (Random.Range(0, 100) < 20 && lukeChosen)
        {
            _audioSource.PlayOneShot(_darthVaderLukeDefeat);
        }
        else
        {
            _audioSource.PlayOneShot(_darthVaderDefeatCommon[((int)Random.Range(0, 100)) % _darthVaderDefeatCommon.Length]);
        }
    }
}