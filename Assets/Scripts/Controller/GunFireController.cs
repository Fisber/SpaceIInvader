using System;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class GunFireController : MonoBehaviour
    {
        public float RepeatTime;

        public event Action<Vector2> OnRequestFireEvent;
        
        public event Action OnEnemyReachPlayer;
        public void Initialize()
        {
            InvokeRepeating(nameof(RequestFire),1,RepeatTime);
        }

        public void CancelInvoke()
        {
            CancelInvoke(nameof(RequestFire));
        }

        private void RequestFire()
        {
            OnRequestFireEvent?.Invoke(transform.position);
        }

      
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(Constants.TagEnemy))
            {
                OnEnemyReachPlayer?.Invoke();
            }
        }
        
    }
}