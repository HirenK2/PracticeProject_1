using UnityEngine;
using UnityEngine.UI;

public class InGameView : BaseView
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS

    [SerializeField] private Text _pairScoreText;
    [SerializeField] private Text _turnCountText;
    [SerializeField] private Text _comboCountText;
    [SerializeField] private Text _highestComboCountText;

    #endregion

    #region UNITY_CALLBACKS

    private void OnEnable()
    {
        GameManager.OnTurnIncr += SetTurnText;
        GameManager.OnPairMatch += SetPairText;
        GameManager.OnComboIncr += SetComboText;
        GameManager.OnHighestComboIncr += SetHighestComboText;
    }

    private void OnDisable()
    {
        GameManager.OnTurnIncr -= SetTurnText;
        GameManager.OnPairMatch -= SetPairText;
        GameManager.OnComboIncr -= SetComboText;
        GameManager.OnHighestComboIncr -= SetHighestComboText;
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
        _pairScoreText.text = $"{pairCount} / {GameManager.Instance.LevelGenerator.totalPairCount}";
    }

    public void SetTurnText(int turnCount)
    {
        _turnCountText.text = turnCount.ToString();
    }

    public void SetComboText(int comboCounter)
    {
        _comboCountText.text = $"x{comboCounter}";
    }

    public void SetHighestComboText(int highestComboCount)
    {
        _highestComboCountText.text = $"x{highestComboCount}";
    }

    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
