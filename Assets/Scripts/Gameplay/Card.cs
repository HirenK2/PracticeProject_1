using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler
{
    #region PUBLIC_VARS
    #endregion

    #region PRIVATE_VARS
    
    [SerializeField] private int _cardId;
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;
    private bool _isRevealed = false;
    private bool _isFlipAnimationRunning = false;

    private static int _cardFlipAnimHash = Animator.StringToHash(GameConstants.CARD_FLIP_ANIM);
    private static int _cardMatchAnimHash = Animator.StringToHash(GameConstants.CARD_MATCH_ANIM);
    private static int _cardMissmatchAnimHash = Animator.StringToHash(GameConstants.CARD_MISSMATCH_ANIM);

    #endregion

    #region UNITY_CALLBACKS

    public void OnPointerDown(PointerEventData eventData)
    {
        OnCardClick();
    }

    #endregion

    #region PUBLIC_METHODS

    public void Init(int id)
    {
        ResetCard();
        _cardId = id;
        SetCardColor();
        SetActiveVisuals(true);
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

    private void ResetCard()
    {
        _cardId = -1;
        _isRevealed = false;
        _isFlipAnimationRunning = false;
        SetCardColor();
        _image.transform.localRotation = Quaternion.identity;
        _image.transform.localScale = Vector3.one;
    }

    private void OnCardClick()
    {
        if (_isRevealed || _isFlipAnimationRunning)
        {
            return;
        }

        FlipCard();
    }

    private void SetCardColor()
    {
        if (_isRevealed)
        {
            _image.color = GameManager.Instance.cardDataSO.GetCardColor(_cardId);
        }
        else
        {
            _image.color = Color.black;
        }
    }

    private void FlipCard()
    {
        _isFlipAnimationRunning = true;
        _animator.Play(_cardFlipAnimHash);
    }

    private void SetActiveVisuals(bool value)
    {
        _image.gameObject.SetActive(value);
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
        if(_isRevealed)
        {
            GameManager.Instance.AddToQueue(this);
        }
    }

    private void OnCardMatchAnimationComplete()
    {
        SetActiveVisuals(false);
    }

    private void OnCardMissmatchAnimationComplete()
    {
        FlipCard();
    }

    #endregion
}
