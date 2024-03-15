using System.Collections.Generic;
using UnityEngine;

public class CreateExploreQuestButton : CreateQuestButton
{
    private readonly string _initialName = "Посетите ";
    private readonly string _initialDescription = "Вам нужно посетить локацию ";
    private readonly string _finalDescription = ".";

    [SerializeField] private string[] _locations;

    private List<string> _locationsBag = new List<string>();

    public override void OnClick()
    {
        string location = GetRandomLocation();

        string name = _initialName + location;
        string description = _initialDescription + 
            location + _finalDescription;

        Quest quest = new ExploreQuest(name, description);

        Mediator.OnQuestCreated(quest);
    }

    //метод, позволяющий избежать создания непрерывной серии одинаковых квестов
    private string GetRandomLocation()
    {
        if (_locationsBag.Count == 0)
        {
            foreach (string item in _locations)
            { 
                _locationsBag.Add(item);
            }
        }

        int rnd = Random.Range(0, _locationsBag.Count);
        string location = _locationsBag[rnd];
        _locationsBag.Remove(location);

        return location;
    }
}
