using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuestView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _finishButton;
    [SerializeField] private QuestViewAnimationSettings _animationSettings;

    private Quest _quest;
    private RectTransform _rectTransform;
    private Image _image;

    public event Action<Quest> Completed;

    public TweenCallback<Quest> Disappeared;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        PlayOnAddedAnimation();
    }

    public void Initialize(Quest quest)
    {
        _quest = quest;

        _nameText.text = _quest.Name;
        _descriptionText.text = _quest.Description;
        _finishButton.onClick.AddListener(OnCompleted);

        InitializeChildProperties(quest);
    }

    public void Remove()
    {
        PlayOnRemovedAnimation();
    }

    private void OnDestroy()
    {
        _finishButton.onClick.RemoveListener(OnCompleted);
    }

    protected abstract void InitializeChildProperties(Quest quest);

    private void OnCompleted()
    {
        Completed?.Invoke(_quest);
    }

    private void PlayOnAddedAnimation()
    {
        float time = _animationSettings.AnimationTime;

        _rectTransform.localScale = Vector3.one * _animationSettings.StartScale;
        _rectTransform.DOScale(_animationSettings.EndScale, time);

        _image.color = _animationSettings.StartColor;
        _image.DOColor(_animationSettings.EndColor, time);
    }

    private void PlayOnRemovedAnimation()
    {
        float time = _animationSettings.AnimationTime;

        _image.DOColor(_animationSettings.StartColor, time);
        _rectTransform.DOScale(_animationSettings.StartScale, time)
            .OnComplete(() => Disappeared(_quest));
    }
}
