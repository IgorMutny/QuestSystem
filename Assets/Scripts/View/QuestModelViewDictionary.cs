using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest Model-View Dictionary")]
public class QuestModelViewDictionary : ScriptableObject
{
    [SerializeField] private string[] _types;
    [SerializeField] private GameObject[] _views;

    private Dictionary<Type, GameObject> _viewsDictionary = new Dictionary<Type, GameObject>();

    public void Initialize()
    {
        if (_types.Length != _views.Length)
        {
            throw new Exception("Amount of types should be equal to amount of views");
        }

        FillDictionary();
    }

    private void FillDictionary()
    {
        for (int i = 0; i < _types.Length; i++)
        {
            Type type = Type.GetType(_types[i]);
            if (type == null)
            {
                throw new Exception($"Type {_types[i]} does not exist");
            }

            if (type.BaseType != typeof(Quest))
            {
                throw new Exception($"Type {_types[i]} is not a quest type");
            }

            if (_viewsDictionary.ContainsKey(type))
            {
                throw new Exception($"Type {_types[i]} is already added");
            }

            _viewsDictionary.Add(type, _views[i]);
        }
    }

    public GameObject Get(Type type)
    {
        if (_viewsDictionary.ContainsKey(type) == true)
        {
            return _viewsDictionary[type];
        }
        else
        {
            throw new Exception($"Dictionary does not contain type {type}");
        }
    }
}
