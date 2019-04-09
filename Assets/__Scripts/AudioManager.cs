using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Private variables

    //Singleton instance
    private static AudioManager _MANAGE_AUDIO;

    //General audio clips for the main menu + a couple of sound effects
    private AudioClip[] _audioClips;

    //Darth Vader sound effects that are common to both characters
    private AudioClip[] _darthVaderCommon;

    //Darth Vader defeat sound effects that are common to both characters
    private AudioClip[] _darthVaderDefeatCommon;

    //Audio source that will play the music
    private AudioSource _audioSource;

    //Clips that are unique to Luke facing the boss
    private AudioClip[] _darthVaderLuke;

    //Default variable which means that Luke is the default variable
    private bool lukeChosen = true;

    //When created, initialize the variables
    public void Awake()
    {
        //Singleton structure
        if (_MANAGE_AUDIO == null)
        {
            _MANAGE_AUDIO = this;
            InitializeVars();
        }
    }


    //Initialize all the Audio Clips
    public void InitializeVars()
    {
        //General sound effects
        _audioClips = new AudioClip[] {(AudioClip)Resources.Load("Audio/menu_selection_click"),
            (AudioClip)Resources.Load("Audio/menu_selection_hover"),
            (AudioClip) Resources.Load("Audio/StarWarsMainTheme"),
            (AudioClip)Resources.Load("Audio/TheImperialMarch"),
            (AudioClip)Resources.Load("Audio/Blaster-Solo"),
            (AudioClip)Resources.Load("Audio/hansolo_gorgeousguy"),
            (AudioClip)Resources.Load("Audio/luke_greetings"),
            (AudioClip)Resources.Load("Audio/blaster_multiple")};


        //Boss sound effects
        _darthVaderCommon = new AudioClip[] { (AudioClip)Resources.Load("Audio/darthvader_dontmakeme"),
        (AudioClip)Resources.Load("Audio/darthvader_expectingyou"),
        (AudioClip)Resources.Load("Audio/darthvader_giveyourself"),
        (AudioClip)Resources.Load("Audio/darthvader_lackoffaith") };

        //Boss defeat sound effects
        _darthVaderDefeatCommon = new AudioClip[] { (AudioClip)Resources.Load("Audio/darthvader_technological"),
        (AudioClip)Resources.Load("Audio/darthvader_honored") };


        //Sound effects unique to Luke
        _darthVaderLuke = new AudioClip[] { (AudioClip)Resources.Load("Audio/darthvader_pointless"), (AudioClip)Resources.Load("Audio/darthvader_taughtyouwell")};

        //If an AudioSource has not been created, create a New One
        if (_audioSource == null)
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _audioSource.loop = true;
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<AudioListener>();
        }

    }

    //When the scene is switched change the music
    public void SwitchScene()
    {
        //Get the name of the current scene
        string scene_name = SceneManager.GetActiveScene().name;


        switch (scene_name)
        {
            //If its the main menu, play the Star Wars theme song if already not playing
            case "_MainMenu":
                if (_audioSource.clip == null || !_audioSource.clip.name.Equals(_audioClips[2].name)){
                    FadeOutAudio(1);
                    _audioSource.clip = _audioClips[2];
                    _audioSource.PlayDelayed(1);
                }
                break;
            //If its the first level, play the Darth Vader theme song if already not playing
            case "_Level_1":
                if(_audioSource.clip == null || !_audioSource.clip.name.Equals(_audioClips[3].name))
                {
                    FadeOutAudio(3);
                    _audioSource.clip = _audioClips[3];
                    _audioSource.PlayDelayed(1);
                }
                break;
        }
    }

    //Play the hover sound effect
    public void PlayHover()
    {
        _audioSource.PlayOneShot(_audioClips[1]);
    }

    //Play the click sound effect
    public void PlayClick()
    {
        _audioSource.PlayOneShot(_audioClips[0]);
    }

    //Fade out the audio that is currently playing
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

    //Play the character sound bites
    public void CharacterSelect(string name)
    {
        switch (name)
        {
            //If han is chosen, then play his sound bit
            case "han":
                _audioSource.PlayOneShot(_audioClips[5]);
                lukeChosen = false;
                break;
            //If luke is chosen then play his sound bite
            case "luke":
                _audioSource.PlayOneShot(_audioClips[6]);
                lukeChosen = true;
                break;
        }
    }

    //Mute or Unmute the audio
    public int Mute()
    {
        //If the volume is 0 (it is muted)
        if(_audioSource.volume == 0.0f)
        {
            //Unmute
            _audioSource.volume = 0.7f;
            return 1;
        }
        else
        {
            //If it is not muted, then mute
            _audioSource.volume = 0;
            return 0;
        }
    }

    //Get the private singleton
    public static AudioManager AUDIO_MANAGER
    {
        get
        {
            return _MANAGE_AUDIO;
        }
    }

    //Play the shooting sound
    public void Shoot(string type)
    {
        switch (type)
        {
            //If its the normal gun, play its sound
            case "normal":
                _audioSource.PlayOneShot(_audioClips[4]);
                break;

            //Play the spread gun sound if its that gun
            case "spread":
                _audioSource.PlayOneShot(_audioClips[7]);
                break;
        }
    }

    //If the boss is going to appear, then play a quote from Darth Vader
    public void PlayDarthVaderQuote()
    {
        if(Random.Range(0, 100) < 20 && lukeChosen)
        {
            _audioSource.PlayOneShot(_darthVaderLuke[0]);
        }
        else
        {
            _audioSource.PlayOneShot(_darthVaderCommon[((int)Random.Range(0, 100)) % _darthVaderCommon.Length]);
        }
    }

    //If the boss is defeated, then play a defeat quote
    public void PlayDarthVaderDefeatQuote()
    {
        if (Random.Range(0, 100) < 20 && lukeChosen)
        {
            _audioSource.PlayOneShot(_darthVaderLuke[1]);
        }     
        else
        {
            _audioSource.PlayOneShot(_darthVaderDefeatCommon[((int)Random.Range(0, 100)) % _darthVaderDefeatCommon.Length]);
        }
    }

    public float Volume()
    {
        return _audioSource.volume;
    }
}