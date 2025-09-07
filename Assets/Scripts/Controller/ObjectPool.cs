using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform DeadPool;
        [SerializeField] private BulitMoveController Bulet;
        [SerializeField] private int InitialAmmount;

        private List<BulitMoveController> Builts = new List<BulitMoveController>();

        public void Initialize()
        {
            for (int i = 0; i < InitialAmmount; i++)
            {
                var built = Instantiate(Bulet, DeadPool.position, quaternion.identity);
                built.transform.SetParent(DeadPool);
                built.transform.localPosition = Vector3.zero;
                Builts.Add(built);
            }
        }

        public BulitMoveController GetBuilit()
        {
            var tempBuilit = Builts.First();
            Builts.RemoveAt(0);
            Builts.Insert(Builts.Count - 1, tempBuilit);
            tempBuilit.transform.parent = null;
            return tempBuilit;
        }

        public void ReturnToPool(BulitMoveController bulit)
        {
            Builts.Add(bulit);
            bulit.transform.SetParent(DeadPool);
            bulit.transform.localPosition = Vector3.zero;
        }
        
        
        
        
    }
}