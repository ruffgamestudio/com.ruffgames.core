using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
    public class LevelConfig
    {
        [PreviewField(150, ObjectFieldAlignment.Center)]
        public List<GameObject> Levels;

        public int RestartLevelIndex;
    }
