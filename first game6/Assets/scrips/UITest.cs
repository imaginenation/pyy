using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class UITest : MonoBehaviour
{
    bool isPause = false;
    public Button pauseBtn;
    public Slider mastervalueslider;
    public AudioMixer masterMixser;
    // Start is called before the first frame update
    void Start()
    {
        pauseBtn.onClick.AddListener(PauseGame);
        mastervalueslider.onValueChanged.AddListener(VolumeChange);
    }
    public void VolumeChange(float volume)
    {
        masterMixser.SetFloat("masterVolume",volume);
    }
    public void PauseGame()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
