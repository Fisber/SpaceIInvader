using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controllers
{
    public class EnemyRowController : MonoBehaviour
    {
        [SerializeField] private List<EnemyController> EnemyController;
        
        public event Action<EnemyController> OnFireRequested;

        private List<int> Indexes = new();
        
        public void InitializeRow()
        {
            var screenSize =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                    Camera.main.transform.position.z));
            int index = 0;
            foreach (var enemyController in EnemyController)
            {
                Indexes.Add(index++);
                enemyController.EnemyMoveController.UpdateScreenSize(screenSize);
                enemyController.EnemyMoveController.Initialize();
            }

            RequestFire();
        }

        private void RequestFire()
        {
            if (Indexes.Count == 0)
            {
                StopAllCoroutines();
                gameObject.SetActive(false);
                return;
            }
            
            float ran = UnityEngine.Random.Range(3f, 10f);
            
            int ranEnemy = UnityEngine.Random.Range(0, Indexes.Count);
            
            StartCoroutine(IEFireTrigger(ran, Indexes[ranEnemy]));
        }

        IEnumerator IEFireTrigger(float delay, int index)
        {
            if (EnemyController[index].gameObject.activeInHierarchy == false)
            {
                Indexes.Remove(index);
                RequestFire();
                yield break;
               
            }
            
            OnFireRequested?.Invoke(EnemyController[index]);
            
            yield return new WaitForSeconds(delay);
            
            RequestFire();
            
            yield return null;
        }
    }
}