using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region PUBLIC_VARS

    public static GameManager Instance;
    public CardDataSO cardDataSO;
    public float initialFlipWaitTimeRatioByPair = 3;

    public static Action<int> OnTurnIncr;
    public static Action<int> OnPairMatch;
    public static Action<int> OnComboIncr;
    public static Action<int> OnHighestComboIncr;

    public LevelGenerator LevelGenerator { get { return _levelGenerator; } }

    #endregion

    #region PRIVATE_VARS

    [SerializeField] private LevelGenerator _levelGenerator;

    private Queue<Card> _flippedCards = new Queue<Card>();

    private int CurrentPairCount
    {
        get { return _currentPairCount; }
        set
        {
            _currentPairCount = value;
            OnPairMatch?.Invoke(value);
        }
    }
    private int _currentPairCount;
    
    private int CurrentTurnCount
    {
        get { return _currentTurnCount; }
        set
        {
            _currentTurnCount = value;
            OnTurnIncr?.Invoke(value);
        }
    }
    private int _currentTurnCount;

    private int CurrentComboCounter
    {
        get { return _currentcomboCounter; }
        set 
        {
            _currentcomboCounter = value;
            OnComboIncr?.Invoke(value);
        }
    }
    private int _currentcomboCounter;

    private int HighestComboScore
    {
        get
        {
            if (_highestComboScore == -1)
            {
                _highestComboScore = PlayerPrefs.GetInt(HIGHEST_COMBO_SCORE, 0);
            }
            return _highestComboScore;
        }
        set
        {
            _highestComboScore = value;
            PlayerPrefs.SetInt(HIGHEST_COMBO_SCORE, _highestComboScore);
            OnHighestComboIncr?.Invoke(value);
        }
    }
    private int _highestComboScore = -1;
    private const string HIGHEST_COMBO_SCORE = "HighestComboScoreKey";

    #endregion

    #region UNITY_CALLBACKS

    private void Awake()
    {
        if (Instance == null)
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
        if (_flippedCards.Count % 2 == 0)
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
        CurrentComboCounter = 0;
        Invoke(nameof(CardFlipBackOnGameStart), _levelGenerator.currentInitialCardFlipWaitTime);
    }

    #endregion

    #region PRIVATE_METHODS

    private void CardFlipBackOnGameStart()
    {
        OnHighestComboIncr?.Invoke(HighestComboScore);
        _levelGenerator.FlipAllCards();
    }

    private void CheckForMatch(Card card)
    {
        CurrentTurnCount++;

        if (_flippedCards.Peek().GetCardId() == card.GetCardId())
        {
            _flippedCards.Dequeue().OnCardMatched();
            card.OnCardMatched();

            CurrentPairCount++;
            HandleComboScore();

            CheckForGameOver();
        }
        else
        {
            CurrentComboCounter = 0;

            _flippedCards.Dequeue().OnCardMissmatched();
            card.OnCardMissmatched();
        }
    }

    private void HandleComboScore()
    {
        _currentcomboCounter++;
        if(_currentcomboCounter > 1)
        {
            OnComboIncr?.Invoke(_currentcomboCounter);
            if(_currentcomboCounter > HighestComboScore)
            {
                HighestComboScore = _currentcomboCounter;
            }
        }
    }

    private void CheckForGameOver()
    {
        if (_currentPairCount == _levelGenerator.totalPairCount)
        {
            ResetStats();
            UiManager.Instance.OnGameOver();
        }
    }

    private void ResetStats()
    {
        _currentPairCount = 0;
        _currentTurnCount = 0;
        _currentcomboCounter = 0;
        _flippedCards.Clear();
    }

    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
