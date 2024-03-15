public class QuestListMediator
{
    private QuestList _questList;
    private QuestListView _questListView;

    public QuestListMediator(QuestList questList, QuestListView questListView)
    {
        _questList = questList;
        _questList.QuestAdded += OnQuestAdded;
        _questList.QuestRemoved += OnQuestRemoved;

        _questListView = questListView;
        _questListView.QuestCompleted += OnQuestCompleted;
    }

    public void Destroy()
    {
        _questList.QuestAdded -= OnQuestAdded;
        _questList.QuestRemoved -= OnQuestRemoved;
        _questListView.QuestCompleted -= OnQuestCompleted;
    }

    public void OnQuestCreated(Quest quest)
    {
        _questList.AddQuest(quest);
    }

    public void OnQuestAdded(Quest quest)
    {
        _questListView.AddQuest(quest);
    }
    public void OnQuestCompleted(Quest quest)
    {
        _questList.RemoveQuest(quest);
    }

    public void OnQuestRemoved(Quest quest)
    {
        _questListView.RemoveQuest(quest);
    }
}
