using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Scriptable/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS

    [SerializeField] private CardData[] _cardDatas;
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region PUBLIC_METHODS

    public Color GetCardColor(int id)
    {
        for (int i = 0; i < _cardDatas.Length; i++)
        {
            if (_cardDatas[i].cardId == id)
            {
                return _cardDatas[i].cardColor;
            }
        }
        return Color.white;
    }

    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
