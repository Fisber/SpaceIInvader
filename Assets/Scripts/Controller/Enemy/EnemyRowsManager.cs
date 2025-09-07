using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class EnemyRowsManager : MonoBehaviour
    {
        [SerializeField] private Transform SpawPoint;
        [SerializeField] private Transform Parent;
        [SerializeField] private EnemyRowController RowGameObject;
        [SerializeField] private List<EnemyRowController> InitialRows;

        private Action<EnemyController> RequestAction;
        
        public void InitializeRowManager()
        {
            foreach (var row in InitialRows)
            {
                row.InitializeRow();
                
            }

            InvokeRepeating(nameof(GenerateRow), 5, Constants.EnemyRowSpawnTime);
        }

        public void SubscribeToFireEvent(Action<EnemyController> action)
        {
            RequestAction = action;
            foreach (var row in InitialRows)
            {
                row.OnFireRequested += action;
            }
        }

        public void OnGameEnd()
        {
            foreach (var row in InitialRows)
            {
                row.gameObject.SetActive(false);
            }

            CancelInvoke(nameof(GenerateRow));
        }

        private void GenerateRow()
        {
            var newRow = Instantiate(RowGameObject, SpawPoint.position, quaternion.identity,Parent);
            InitialRows.Add(newRow);
            newRow.InitializeRow();
            newRow.OnFireRequested += RequestAction;
        }
    }
}