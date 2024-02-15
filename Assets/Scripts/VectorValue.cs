using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("�������� ��� ������������ ������")]
    public Vector2 initialValue;
    [Header("�������� � ������ ����")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize() { initialValue = defaultValue; }

    public void OnBeforeSerialize() { }
}
