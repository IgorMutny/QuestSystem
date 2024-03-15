using System.Collections.Generic;
using UnityEngine;

public class CreateGatherQuestButton : CreateQuestButton
{
    private readonly string _initialName = "—оберите ";
    private readonly string _initialDescription = "¬ам нужно собрать предметы: ";
    private readonly string _finalDescription = ".";

    [SerializeField] private string[] _collectables;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;

    private List<string> _collectablesBag = new List<string>();

    public override void OnClick()
    {
        string collectable = GetRandomCollectable();

        int requiredAmount = Random.Range(_minValue, _maxValue + 1);

        string name = _initialName + collectable;
        string description = _initialDescription + 
            requiredAmount + " " + collectable + _finalDescription;

        Quest quest = new GatherQuest(name, description, requiredAmount);

        Mediator.OnQuestCreated(quest);
    }

    //метод, позвол€ющий избежать создани€ непрерывной серии одинаковых квестов
    private string GetRandomCollectable()
    {
        if (_collectablesBag.Count == 0)
        {
            foreach (string item in _collectables)
            {
                _collectablesBag.Add(item);
            }
        }

        int rnd = Random.Range(0, _collectablesBag.Count);
        string collectable = _collectablesBag[rnd];
        _collectablesBag.Remove(collectable);

        return collectable;
    }
}
