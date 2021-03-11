using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainUIManager : MonoBehaviour
{
    private static MainUIManager instance;

    public AudioSource bgm;
    private bool isMute;
    public static MainUIManager Instance { get => instance; set => instance = value; }

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    { 
        
        int temp = PlayerPrefs.GetInt("MuteState", 0);
        isMute = (temp == 0) ? false : true;
        //Debug.Log("Main MuteState" + isMute);
        if (isMute == false)
        {
            if(!bgm.isPlaying)bgm.Play();
        }
        else bgm.Pause();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void StartNewGame()
    {
        PlayerPrefs.DeleteKey("Gold");
        PlayerPrefs.DeleteKey("Exp");
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("SmallCountDown");
        PlayerPrefs.DeleteKey("BigCountDown");
        SceneManager.LoadScene(1);
    }
    public void ResumeGame()
    {
        SceneManager.LoadScene(1);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
