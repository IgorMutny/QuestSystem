using UnityEngine;

public abstract class CreateQuestButton : MonoBehaviour
{
    private QuestListMediator _mediator;

    protected QuestListMediator Mediator => _mediator;

    public void SetQuestSystemMediator(QuestListMediator mediator)
    { 
        _mediator = mediator;
    }

    public abstract void OnClick();
}
