using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Video;
using UnityEngine.UI;


public class GameSceneManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public AudioMixer otheraudioMixer;
    public GameObject dashIcon;
    public Toggle Open;
    //public VideoPlayer openvp;


   

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //DontDestroyOnLoad(this);
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void PanseMenu() {
        pauseMenu.SetActive(true);
        //时间静止
        Time.timeScale = 0f;
    }
    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetDashIcon() {
      
        dashIcon.SetActive(!Open.isOn);
    }
    //public void QuitGame()
    //{
    //   SceneManager.LoadScene(SceneManager.GetSceneByName(""),LoadSceneMode.Single)
    //}
    public void SetVolume(float value) {
        audioMixer.SetFloat("MainVolume", value);
        
    }
    public void SetOtherVolume(float value)
    {
        otheraudioMixer.SetFloat("OtherAudio", value);

    }
    public void UIEnable() { GameObject.Find("Canvas/MainPanel/UI").SetActive(true); }
}
