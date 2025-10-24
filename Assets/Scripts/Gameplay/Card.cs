using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS
    
    [SerializeField] private int _cardId;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    private bool _isRevealed = false;
    private bool _isFlipAnimationRunning = false;
    private bool _isMissmatched = false;

    private static int _cardFlipAnimHash = Animator.StringToHash(GameConstants.CARD_FLIP_ANIM);
    private static int _cardMatchAnimHash = Animator.StringToHash(GameConstants.CARD_MATCH_ANIM);
    private static int _cardMissmatchAnimHash = Animator.StringToHash(GameConstants.CARD_MISSMATCH_ANIM);

    #endregion

    #region UNITY_CALLBACKS

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Pointer Down on {_cardId}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnCardClick();
    }

    #endregion

    #region PUBLIC_METHODS

    public void Init(int id)
    {
        _cardId = id;
        SetCardColor();
    }

    public int GetCardId()
    {
        return _cardId;
    }

    public void OnCardMatched()
    {
        _animator.Play(_cardMatchAnimHash);
    }

    public void OnCardMissmatched()
    {
        _animator.Play(_cardMissmatchAnimHash);
    }

    #endregion

    #region PRIVATE_METHODS

    private void OnCardClick()
    {
        if (_isRevealed || _isFlipAnimationRunning)
        {
            return;
        }

        _isMissmatched = false;
        FlipCard();
    }

    private void SetCardColor()
    {
        if (_isRevealed)
        {
            _spriteRenderer.color = GameManager.Instance.cardDataSO.GetCardColor(_cardId);
        }
        else
        {
            _spriteRenderer.color = Color.black;
        }
    }

    private void FlipCard()
    {
        _isFlipAnimationRunning = true;
        _animator.Play(_cardFlipAnimHash);
    }

    #endregion

    #region COROUTINES
    #endregion

    #region UI_CALLBACKS
    #endregion

    #region ANIMATION_TRIGGERS

    private void OnCardFlipMidPoint()
    {
        _isRevealed = !_isRevealed;
        SetCardColor();
    }

    private void OnCardFlipAnimationComplete()
    {
        _isFlipAnimationRunning = false;
        if(!_isMissmatched)
        {
            GameManager.Instance.AddToQueue(this);
        }
    }

    private void OnCardMatchAnimationComplete()
    {
        gameObject.SetActive(false);
    }

    private void OnCardMissmatchAnimationComplete()
    {
        _isMissmatched = true;
        FlipCard();
    }

    #endregion
}
