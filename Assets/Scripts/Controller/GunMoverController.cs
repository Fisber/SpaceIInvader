
using System;
using System.Linq;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class GunMoverController : MonoBehaviour
    {
        

        private bool IsInitialized;

        public event Action OnGameEne;

        public void Initialized(bool initialize)
        {
            IsInitialized = initialize;
        }

        private void Update()
        {
            if (IsInitialized == false)
            {
                return;
            }
            
            if (Input.touches.Any() == false)
            {
                return;
            }

            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    Move(Camera.main.ScreenToWorldPoint(Input.touches[0].position), 15);
                    break;
                case TouchPhase.Stationary:
                    Move(Camera.main.ScreenToWorldPoint(Input.touches[0].position), 15);
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
            }
        }

        private void Move(Vector2 pos, float speed)
        {

            pos.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        }

      
    }
}