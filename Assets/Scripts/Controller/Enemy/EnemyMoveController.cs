using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class EnemyMoveController : MonoBehaviour
    {
        [SerializeField] float Speed;
        [SerializeField] float Tolerance;
        [SerializeField] float MoveDirection = 1;
        [SerializeField] private Vector2 ScreenSize;

        public bool IsLife;
        private List<Vector2> Directions = new List<Vector2>();
        private float CurrentVerticalPos;
        private bool IsHarezontal = true;

        public void UpdateScreenSize(Vector2 screenSize)
        {
            ScreenSize = screenSize;
        }

        public void Initialize()
        {
            Directions.Add(new Vector2(ScreenSize.x * MoveDirection, transform.position.y));
            Directions.Add(new Vector2(0, transform.position.y - 1));
        }


        private void Update()
        {
            if ( Directions.Count == 0)
            {
                return;
            }

            MoveObject();
        }

        private void MoveObject()
        {
            var position = transform.position;
            var step = Speed * Time.deltaTime;
            position = Vector3.MoveTowards(position, Directions.First(), step);
            transform.position = position;
            Swap();
        }

        private void Swap()
        {
            if (!(Vector2.Distance(transform.position, Directions.First()) < Tolerance))
            {
                return;
            }

            if (IsHarezontal)
            {
                IsHarezontal = !IsHarezontal;
                var temp = Directions[1];
                Directions.RemoveAt(1);
                Directions.Insert(0, temp);
                var newHorizontalPos = Directions[0];
                newHorizontalPos.x = transform.position.x;
                Directions[0] = newHorizontalPos;
                Directions[1] = new Vector2(Directions[1].x * -1, Directions[0].y);
            }
            else
            {
                IsHarezontal = !IsHarezontal;
                var temp = Directions[1];
                Directions.RemoveAt(1);
                Directions.Insert(0, temp);
                Directions[1] = new Vector2(Directions[0].x, transform.position.y - 1);
            }
        }
    }
}