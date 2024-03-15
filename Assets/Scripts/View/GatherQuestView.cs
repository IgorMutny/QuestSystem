using TMPro;
using UnityEngine;

public class GatherQuestView : QuestView
{
    private readonly string _initialRemained = "Осталось: ";

    [SerializeField] private TextMeshProUGUI _remainedText;
    [SerializeField] private RectTransform _progressBar;
    [SerializeField] private RectTransform _progressBarFilled;

    protected override void InitializeChildProperties(Quest quest)
    {
        SetProgressView(quest);
    }

    private void SetProgressView(Quest quest)
    {
        GatherQuest gatherQuest = (GatherQuest)quest;
        SetRemainedText(gatherQuest);
        FillProgressBar(gatherQuest);
    }

    private void SetRemainedText(GatherQuest gatherQuest)
    {
        int remained = gatherQuest.RequiredAmount - gatherQuest.CurrentAmount;
        _remainedText.text = _initialRemained + remained;
    }

    private void FillProgressBar(GatherQuest gatherQuest)
    {
        float percentage = (float)gatherQuest.CurrentAmount / (float)gatherQuest.RequiredAmount;
        float fullWidth = _progressBar.sizeDelta.x;
        float width = fullWidth * percentage;
        float height = _progressBarFilled.sizeDelta.y;
        _progressBarFilled.sizeDelta = new Vector2(width, height);
    }
}
