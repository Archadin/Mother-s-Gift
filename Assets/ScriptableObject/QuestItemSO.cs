using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestItemSO",menuName = "QuestItems/QuestItem")]
public class QuestItemSO : ScriptableObject
{
    public string Name;
    public bool isFound;
}
