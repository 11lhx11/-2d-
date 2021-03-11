using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource bgm;
    public AudioClip GoldClip;
    public AudioClip RewardClip;
    public AudioClip FireClip;
    public AudioClip ChangeClip;
    public AudioClip LevelupClip;
    private bool isMute;
    public Toggle muteConfig;
    public static AudioManager Instance { get => instance; set => instance = value; }
    void Awake()
    {
        Instance = this;
        GetMuteState();
        PlayBackgroundMusic();
    }
  
    public void ChangeMuteState(bool _isMute)
    {
        isMute = _isMute;
        PlayBackgroundMusic();
    }
    private void PlayBackgroundMusic()
    {
        if (isMute == true)
        {
            bgm.Pause();
        }
        else if(!bgm.isPlaying) bgm.Play();
    }
    public void SaveMuteState()
    {
        PlayerPrefs.SetInt("MuteState", (isMute==false)?0:1);
    }
    public void GetMuteState()
    {
        int temp=PlayerPrefs.GetInt("MuteState", 0);
        isMute = (temp==0)?false:true;
        muteConfig.isOn = isMute;
    }
    public void PlayAudioClip(AudioClip clip)
    {
        if (isMute) return;
        AudioSource.PlayClipAtPoint(clip,new Vector3(0,0,0));
    }
}
