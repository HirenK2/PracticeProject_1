using UnityEngine;

public class UiManager : MonoBehaviour
{
    #region PUBLIC_VARS

    public static UiManager Instance;

    #endregion

    #region PRIVATE_VARS

    [SerializeField] private MenuView _menuPanel;
    [SerializeField] private InGameView _gameplayPanel;
    [SerializeField] private GameoverView _gameOverPanel;

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

    public void OnPlayButtonClicked()
    {
        _menuPanel.HideView();
        GameManager.Instance.OnPlayButtonClick();
    }

    public void OnNextLevelButtonClick()
    {
        _gameOverPanel.HideView();
        GameManager.Instance.OnPlayButtonClick();
    }

    public void OnGameStart()
    {
        _gameplayPanel.Init();
    }

    public void OnGameOver()
    {
        _gameOverPanel.ShowView();
    }

    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
