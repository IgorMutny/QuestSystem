using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestListView : MonoBehaviour
{
    [SerializeField] private QuestListViewContent _content;
    [SerializeField] private QuestModelViewDictionary _dictionary;

    private RectTransform _contentTransform;
    private Dictionary<Quest, QuestView> _quests = new Dictionary<Quest, QuestView>();

    public event Action<Quest> QuestCompleted;

    private void Awake()
    {
        _dictionary.Initialize();
        _contentTransform = _content.GetComponent<RectTransform>();
    }

    public void AddQuest(Quest quest)
    {
        GameObject prefab = _dictionary.Get(quest.GetType());
        QuestView questView = 
            Instantiate(prefab, _contentTransform).GetComponent<QuestView>();
        _content.AddElement((RectTransform)questView.transform);
        questView.Initialize(quest);
        _quests.Add(quest, questView);
        questView.Completed += OnQuestCompleted;
    }

    public void RemoveQuest(Quest quest)
    {
        QuestView questView = _quests[quest];
        questView.Completed -= OnQuestCompleted;
        questView.Disappeared += DestroyQuest;
        questView.Remove();
    }

    private void DestroyQuest(Quest quest)
    {
        QuestView questView = _quests[quest];
        questView.Disappeared -= DestroyQuest;
        _quests.Remove(quest);
        _content.RemoveElement((RectTransform)questView.transform);
        Destroy(questView.gameObject);
    }

    private void OnQuestCompleted(Quest quest)
    {
        QuestCompleted?.Invoke(quest);
    }
}
