using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private QuestListView _questListView;
    [SerializeField] private CreateQuestButton _exploreQuestButton;
    [SerializeField] private CreateQuestButton _gatherQuestButton;

    private void Awake()
    {
        QuestList questList = new QuestList();

        QuestListMediator mediator = new QuestListMediator(questList, _questListView);

        _exploreQuestButton.SetQuestSystemMediator(mediator);
        _gatherQuestButton.SetQuestSystemMediator(mediator);
    }
}
