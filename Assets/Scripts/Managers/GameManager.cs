using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region PUBLIC_VARS

    public static GameManager Instance;
    public CardDataSO cardDataSO;

    public static Action<int> OnTurnIncr;
    public static Action<int> OnPairMatch;

    public LevelGenerator LevelGenerator { get {  return _levelGenerator; } }

    #endregion

    #region PRIVATE_VARS

    [SerializeField] private LevelGenerator _levelGenerator;

    private Queue<Card> _flippedCards = new Queue<Card>();

    private int _currentMatchCount;
    private int _currentTurnCount;

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

    public void AddToQueue(Card card)
    {
        if(_flippedCards.Count % 2 == 0)
        {
            _flippedCards.Enqueue(card);
        }
        else
        {
            CheckForMatch(card);
        }
    }

    public void OnPlayButtonClick()
    {
        _levelGenerator.GenerateRandomLevel();
        UiManager.Instance.OnGameStart();
    }

    #endregion

    #region PRIVATE_METHODS

    private void CheckForMatch(Card card)
    {
        _currentTurnCount++;
        OnTurnIncr?.Invoke(_currentTurnCount);
        if (_flippedCards.Peek().GetCardId() == card.GetCardId())
        {
            _flippedCards.Dequeue().OnCardMatched();
            card.OnCardMatched();

            _currentMatchCount++;
            OnPairMatch?.Invoke(_currentMatchCount);
            CheckForGameOver();
        }
        else
        {
            _flippedCards.Dequeue().OnCardMissmatched();
            card.OnCardMissmatched();
        }
    }

    private void CheckForGameOver()
    {
        if(_currentMatchCount == _levelGenerator.totalPairCount)
        {
            ResetStats();
            UiManager.Instance.OnGameOver();
        }
    }

    private void ResetStats()
    {
        _currentMatchCount = 0;
        _currentTurnCount = 0;
        _flippedCards.Clear();
    }

    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
