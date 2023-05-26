using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestItemSO",menuName = "ScriptableObjects/QuestItem")]
public class QuestItemSO : ScriptableObject
{
    public string Name;
    public bool isFound;
}
