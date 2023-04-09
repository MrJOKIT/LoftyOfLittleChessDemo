using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueSystem/Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    [TextArea(5,20)]
    [SerializeField] private string message;
}
