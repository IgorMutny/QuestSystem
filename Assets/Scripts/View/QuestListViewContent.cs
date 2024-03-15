using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListViewContent : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private float _animationTime;

    private RectTransform _rectTransform;
    private List<RectTransform> _elements = new List<RectTransform>();
    private Vector2[] _anchoredPositions;
    private Vector2 _startSize;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startSize = _rectTransform.sizeDelta;

        _scrollBar.onValueChanged.AddListener(OnScrollbarMoved);
    }

    private void OnDestroy()
    {
        _scrollBar.onValueChanged.RemoveListener(OnScrollbarMoved);
    }

    public void AddElement(RectTransform element)
    {
        _elements.Add(element);
        DefineAnchoredPositions();
        int lastIndex = _elements.Count - 1;
        _elements[lastIndex].anchoredPosition = _anchoredPositions[lastIndex];
        ResizeRectTransform();
        MoveToEnd();
    }

    public void RemoveElement(RectTransform element)
    {
        _elements.Remove(element);
        DefineAnchoredPositions();
        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i].DOAnchorPos(_anchoredPositions[i], _animationTime);
        }
        ResizeRectTransform();
    }

    private void DefineAnchoredPositions()
    {
        _anchoredPositions = new Vector2[_elements.Count];

        if (_elements.Count > 0)
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                _anchoredPositions[i] = GetRequiredAnchoredPosition(i);
                _elements[i].DOAnchorPos(_anchoredPositions[i], _animationTime);
            }
        }
    }

    private Vector2 GetRequiredAnchoredPosition(int index)
    {
        Vector2 anchoredPosition;

        if (index == 0)
        {
            anchoredPosition = GetFirstRequiredAnchoredPosition();
        }
        else
        {
            anchoredPosition = GetNextRequiredAnchoredPosition(index);
        }

        return anchoredPosition;
    }

    private Vector2 GetFirstRequiredAnchoredPosition()
    {
        float availableSpace = _startSize.y;
        float requiredSpace = _elements[0].sizeDelta.y;

        Vector2 anchoredPosition = new Vector2(0, -(requiredSpace - availableSpace) / 2);
        return anchoredPosition;
    }

    private Vector2 GetNextRequiredAnchoredPosition(int index)
    {
        Vector2 prevAnchoredPosition = _anchoredPositions[index - 1];
        float prevHalfHeight = _elements[index - 1].sizeDelta.y / 2;
        float currentHalfHeight = _elements[index].sizeDelta.y / 2;
        Vector2 offset = new Vector2(0, prevHalfHeight + currentHalfHeight);
        Vector2 anchoredPosition = prevAnchoredPosition - offset;
        return anchoredPosition;
    }

    private void ResizeRectTransform()
    {
        float height = GetTotalHeight();
        _rectTransform.sizeDelta = new Vector2(_startSize.x, height);

        if (_rectTransform.anchoredPosition.y > GetTotalHeight() - _startSize.y)
        {
            MoveY(GetTotalHeight() - _startSize.y);
        }

        ResizeScrollbarHandle();
    }

    private void OnScrollbarMoved(float value)
    {
        float offset = (_rectTransform.sizeDelta.y - _startSize.y) * value;
        MoveY(offset);
    }

    private void MoveToEnd()
    {
        float endY = _rectTransform.sizeDelta.y - _startSize.y;
        MoveY(endY);
        _scrollBar.value = 1;
    }

    private void MoveY(float endY)
    {
        Vector2 endPosition = new Vector2(0, endY);
        _rectTransform.DOAnchorPos(endPosition, _animationTime);
    }

    private void ResizeScrollbarHandle()
    {
        _scrollBar.size = _startSize.y / _rectTransform.sizeDelta.y;
    }

    private float GetTotalHeight()
    {
        float height = 0;
        foreach (var element in _elements)
        {
            height += element.sizeDelta.y;
        }
        height = Mathf.Max(height, _startSize.y);
        return height;
    }
}
