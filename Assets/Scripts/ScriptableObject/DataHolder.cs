using System;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO", fileName = "EnemyHolder")]
    public class DataHolder : ScriptableObject
    {
        [SerializeField] public Data UserData;
       

        [Serializable]
        public class Data
        {
            public int Score;
            public int LevelPaseed;
        }
        
        
    }
}