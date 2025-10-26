using UnityEngine;
using UnityEngine.UI;

public class InGameView : BaseView
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS

    [SerializeField] private Text _pairScoreText;
    [SerializeField] private Text _turnCountText;

    #endregion

    #region UNITY_CALLBACKS

    private void OnEnable()
    {
        GameManager.OnTurnIncr += SetTurnText;
        GameManager.OnPairMatch += SetPairText;
    }

    private void OnDisable()
    {
        GameManager.OnTurnIncr -= SetTurnText;
        GameManager.OnPairMatch -= SetPairText;
    }

    #endregion

    #region PUBLIC_METHODS

    public void Init()
    {
        SetPairText(0);
        SetTurnText(0);
        ShowView();
    }

    public void SetPairText(int pairCount)
    {
        _pairScoreText.text = $"{pairCount} out of {GameManager.Instance.LevelGenerator.totalPairCount}";
    }

    public void SetTurnText(int turnCount)
    {
        _turnCountText.text = turnCount.ToString();
    }

    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
