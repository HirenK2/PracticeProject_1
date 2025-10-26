using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS

    [SerializeField] private Card _cardPrefab;
    [SerializeField][Range(2, 4)] private int _minGridRange, _maxGridRange;
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
        _currentLevelData.gridSizeX = Random.Range(_minGridRange, _maxGridRange + 1);
        _currentLevelData.gridSizeY = Random.Range(_minGridRange, _maxGridRange + 1);

        int totalRequiredCardCount = _currentLevelData.gridSizeX * _currentLevelData.gridSizeY;
        if(totalRequiredCardCount % 2 != 0)
        {
            if (_currentLevelData.gridSizeX < _maxGridRange)
            {
                _currentLevelData.gridSizeX++;
            }
            else
            {
                _currentLevelData.gridSizeX--;
            }
            totalRequiredCardCount = _currentLevelData.gridSizeX * _currentLevelData.gridSizeY;
        }

        _grid.constraintCount = Mathf.Max(_currentLevelData.gridSizeX, _currentLevelData.gridSizeY);

        GenerateRandomIdPool(totalRequiredCardCount / 2);

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
