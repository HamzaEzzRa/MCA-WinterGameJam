using UnityEngine;
using UnityEngine.Audio;
using Cinemachine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject followCam;
    public AudioMixer mainMixer;

    public void SetInvertedControls(bool inverted)
    {
        AudioManager.Instance.PlayUntilFinish("Toggle");
        followCam.GetComponent<CinemachineFreeLook>().m_XAxis.m_InvertInput = inverted;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        followCam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0.2f * sensitivity;
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20f);
    }
}
