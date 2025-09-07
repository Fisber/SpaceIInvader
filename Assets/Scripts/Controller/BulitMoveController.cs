using System;
using UnityEngine;

namespace Controllers
{
    public class BulitMoveController : MonoBehaviour
    {
        [SerializeField] private float  TimeOut;
        public bool IsEnemy;
        public float  Speed;
        public Action OnHitEnemyAction;
        public Action OnHitPlayerAction;
        public Action OnTimeOutAction;

        private Vector3 MoveDirection;

        public void Initialize()
        {

            var scale = transform.localScale;
            if (IsEnemy)
            {
                scale.y = -1;
                MoveDirection = Vector3.down;
            }
            else
            {
                scale.y = 1;
                MoveDirection = Vector3.up;
            }

            transform.localScale = scale;
           
            
            Invoke( "OnTimeOut",TimeOut);
        }

        private void Update()
        {
            transform.Translate(MoveDirection * (Speed * Time.deltaTime));
        }

        private void OnTimeOut()
        {
            OnTimeOutAction?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsEnemy == false && collision.CompareTag("enemy"))
            {
                collision.gameObject.SetActive(false);
                CancelInvoke(nameof(OnTimeOut));
                OnHitEnemyAction?.Invoke();
                OnHitEnemyAction = null;
                
            }

            if (IsEnemy  && collision.CompareTag("player"))
            {
                CancelInvoke(nameof(OnTimeOut));
                OnHitPlayerAction?.Invoke();
                OnHitPlayerAction = null;
            }
        }
    }
}