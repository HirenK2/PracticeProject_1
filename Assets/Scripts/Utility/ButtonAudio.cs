using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAudio : MonoBehaviour
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS

    [SerializeField] private AudioManager.AudioTag _audioTag = AudioManager.AudioTag.BUTTON_CLICK;
    private bool _isInitialized = false;

    #endregion

    #region UNITY_CALLBACKS

    private void Start()
    {
        if(!_isInitialized)
        {
            _isInitialized = true;
            GetComponent<Button>().onClick.AddListener(PlayButtonClickAudio);
        }
    }

    #endregion

    #region PUBLIC_METHODS
    #endregion

    #region PRIVATE_METHODS

    private void PlayButtonClickAudio()
    {
        AudioManager.Instance.PlaySFX(AudioManager.AudioTag.BUTTON_CLICK, 1f);
    }

    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
