using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartScrollText : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        string name = "";
        if (CharacterSelect.CHAR_SINGLETON.luke)
        {
            name = "Luke";
        }
        else if (CharacterSelect.CHAR_SINGLETON.solo)
        {
            name = "Han";
        }
        SceneManager.GetActiveScene().GetRootGameObjects()[2].GetComponent<VideoPlayer>().clip = Resources.Load("Video/" + name) as VideoClip;
        Invoke("NextScene", 12);
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
