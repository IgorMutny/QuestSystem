using UnityEngine;

[CreateAssetMenu(menuName = "Quest View Animation Settings")]
public class QuestViewAnimationSettings : ScriptableObject
{
    [field: SerializeField] public float AnimationTime { get; private set; }
    [field: SerializeField] public float StartScale { get; private set; }
    [field: SerializeField] public float EndScale { get; private set; }
    [field: SerializeField] public Color StartColor { get; private set; }
    [field: SerializeField] public Color EndColor { get; private set; }
}
