using DG.Tweening;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Transform _animationTarget;

    private float _animTime = 0.5f;
    private Vector3 _animCloseScale = Vector3.one * 0.3f;

    private void Awake()
    {
        _confirmButton?.onClick.AddListener(OnConfirmButton);
        _cancelButton?.onClick.AddListener(OnCancelButton);
    }

    protected virtual void OnEnable()
    {
        if (_animationTarget)
        {
            _animationTarget.localScale = _animCloseScale;
            _animationTarget.DOScale(Vector3.one, _animTime)
                            .SetEase(Ease.OutBack);
        }

    }

    protected virtual void OnDisable()
    {
        
    }


    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        _animationTarget.DOScale(_animCloseScale, _animTime)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => gameObject.SetActive(false));
        
    }

    protected virtual void OnConfirmButton()
    {
        Close();
    }

    protected virtual void OnCancelButton()
    {
        Close();
    }
}

