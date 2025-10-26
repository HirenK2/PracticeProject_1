using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    #region PUBLIC_VARS

    public int totalPairCount { get; private set; }

    #endregion

    #region PRIVATE_VARS

    [SerializeField] private Card _cardPrefab;
    [SerializeField] private Vector2Int _gridRangeX;
    [SerializeField] private Vector2Int _gridRangeY;
    [SerializeField] private GridLayoutGroup _grid;

    private LevelData _currentLevelData;
    private List<Card> _spawnedCardList = new List<Card>();
    private List<int> _cardIdCurrentPool = new List<int>();

    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region PUBLIC_METHODS

    [ContextMenu("GenerateRandomLevel")]
    public void GenerateRandomLevel()
    {
        _currentLevelData.gridSizeX = Random.Range(_gridRangeX.x, _gridRangeX.y + 1);
        _currentLevelData.gridSizeY = Random.Range(_gridRangeY.x, _gridRangeY.y + 1);

        int totalRequiredCardCount = _currentLevelData.gridSizeX * _currentLevelData.gridSizeY;
        if (totalRequiredCardCount % 2 != 0)
        {
            if (_currentLevelData.gridSizeY < _gridRangeY.y)
            {
                _currentLevelData.gridSizeY++;
            }
            else
            {
                _currentLevelData.gridSizeY--;
            }
            totalRequiredCardCount = _currentLevelData.gridSizeX * _currentLevelData.gridSizeY;
        }
        totalPairCount = totalRequiredCardCount / 2;
        _grid.constraintCount = Mathf.Max(_currentLevelData.gridSizeX, _currentLevelData.gridSizeY);

        GenerateRandomIdPool(totalPairCount);

        if (totalRequiredCardCount >= _spawnedCardList.Count)
        {
            for (int i = 0; i < totalRequiredCardCount; i++)
            {
                if (_spawnedCardList.Count <= i)
                {
                    _spawnedCardList.Add(Instantiate(_cardPrefab, _grid.transform));
                }

                InitCard(_spawnedCardList[i], PopRandomCardIdFromPool());
            }
        }
        else
        {
            for (int i = _spawnedCardList.Count - 1; i >= 0; i--)
            {
                if (i < totalRequiredCardCount)
                {
                    InitCard(_spawnedCardList[i], PopRandomCardIdFromPool());
                }
                else
                {
                    Destroy(_spawnedCardList[i].gameObject);
                    _spawnedCardList.RemoveAt(i);
                }
            }
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private int GetRandomCardId()
    {
        return GameManager.Instance.cardDataSO.GetRandomCardId();
    }

    private void GenerateRandomIdPool(int uniqueIdCount)
    {
        _cardIdCurrentPool.Clear();
        for (int i = 0; i < uniqueIdCount; i++)
        {
            int randomId = GetRandomCardId();
            _cardIdCurrentPool.Add(randomId);
            _cardIdCurrentPool.Add(randomId);
        }
    }

    private int PopRandomCardIdFromPool()
    {
        int selectedIndex = Random.Range(0, _cardIdCurrentPool.Count);
        int cardId = _cardIdCurrentPool[selectedIndex];
        _cardIdCurrentPool.RemoveAt(selectedIndex);
        return cardId;
    }

    private void InitCard(Card card, int cardId)
    {
        card.Init(cardId);
    }


    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
