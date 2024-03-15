using Random = UnityEngine.Random;

public class GatherQuest : Quest
{
    private int _requiredAmount;
    private int _currentAmount;

    public int RequiredAmount => _requiredAmount;
    public int CurrentAmount => _currentAmount;

    public GatherQuest(string name, string description, int requiredAmount):
        base(name, description)
    {
        _requiredAmount = requiredAmount;
        _currentAmount = Random.Range(0, _requiredAmount);
    }
}
