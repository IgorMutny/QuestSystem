using System;
using System.Collections.Generic;

public class QuestList
{
    public event Action<Quest> QuestAdded;
    public event Action<Quest> QuestRemoved;

    private List<Quest> _quests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        _quests.Add(quest);
        QuestAdded?.Invoke(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        _quests.Remove(quest);
        QuestRemoved?.Invoke(quest);
    }
}
