using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region PUBLIC_VARS

    public static AudioManager Instance;

    #endregion

    #region PRIVATE_VARS

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioData[] _audioData;

    #endregion

    #region UNITY_CALLBACKS

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    #endregion

    #region PUBLIC_METHODS

    public void PlaySFX(AudioTag tag, float volume = 0.6f)
    {
        for (int i = 0; i < _audioData.Length; i++)
        {
            if (_audioData[i].audioTag == tag)
            {
                _audioSource.PlayOneShot(_audioData[i].clip, volume);
                return;
            }
        }
    }

    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion

    public enum AudioTag
    {
        CARD_CLICK,
        CARD_FLIP,
        CARD_MATCH,
        CARD_MISSMATCH,
        BUTTON_CLICK
    }

    [Serializable]
    public struct AudioData
    {
        public AudioTag audioTag;
        public AudioClip clip;
    }
}
