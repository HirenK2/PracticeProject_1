using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region PUBLIC_VARS

    public static GameManager Instance;
    public CardDataSO cardDataSO;

    #endregion

    #region PRIVATE_VARS

    private Queue<Card> _flippedCards = new Queue<Card>();

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

    #endregion

    #region PRIVATE_METHODS

    private void CheckForMatch(Card card)
    {
        if(_flippedCards.Peek().GetCardId() == card.GetCardId())
        {
            _flippedCards.Dequeue().OnCardMatched();
            card.OnCardMatched();
        }
        else
        {
            _flippedCards.Dequeue().OnCardMissmatched();
            card.OnCardMissmatched();
        }

    }

    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion
}
