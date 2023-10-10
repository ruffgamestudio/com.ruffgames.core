using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable,BoxGroup(group:"ID",CenterLabel = true),HideLabel]
public class ID
{
    [ReadOnly, HorizontalGroup("ID"), SerializeField, HideLabel]
    private string id = Guid.NewGuid().ToString();

    [HorizontalGroup("ID", Width = 18), SerializeField, HideLabel]
    private bool idLock;

    public string Id => id;

    [Button(ButtonSizes.Small), HorizontalGroup("ID", Width = 100)]
    private void GenerateID()
    {
        if (!idLock) return;
        id = Guid.NewGuid().ToString();
        idLock = false;
    }
}