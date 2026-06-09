using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void StartSound()
    {
        if (audioManager == null)
        {
            Debug.LogError("AudioManager este NULL!");
            return;
        }

        if (audioManager.buttonClick == null)
        {
            Debug.LogError("buttonClick este NULL!");
            return;
        }

        audioManager.PlaySFX(audioManager.buttonClick);
    }
}
