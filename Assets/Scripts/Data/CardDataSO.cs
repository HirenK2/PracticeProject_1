using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Scriptable/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS

    [SerializeField] private CardData[] _cardDatas;

    private int[] _allAvailableIds = new int[0];

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

    public int GetRandomCardId()
    {
        if(_allAvailableIds.Length == 0)
        {
            FetchIds();
        }
        return _allAvailableIds[Random.Range(0, _allAvailableIds.Length)];
    }

    public int[] GetAllAvailableIds()
    {
        if(_allAvailableIds.Length == 0)
        {
            FetchIds();
        }
        return _allAvailableIds;
    }

    #endregion

    #region PRIVATE_METHODS

    [ContextMenu("FetchAvailableIds")]
    private void FetchIds()
    {
        _allAvailableIds = _cardDatas.Select(x => x.cardId).ToArray();
    }

    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
