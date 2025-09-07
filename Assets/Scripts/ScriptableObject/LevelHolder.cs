using System;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "Level", fileName = "LevelHolder")]
    public class LevelHolder : ScriptableObject
    {
        public Level LevelIds;
        [Serializable]
        public class Level
        {
            public int LevelId;
        }
    }
}