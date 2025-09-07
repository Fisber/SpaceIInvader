using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UiMenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private Button ButtonLevel1;
        [SerializeField] private Button ButtonLevel2;
        [SerializeField] private Button ButtonLevel3;

        public event Action OnClickPlayLevel1;
        public event Action OnClickPlayLevel2;
        public event Action OnClickPlayLevel3;

        public void SetScore(int score)
        {
            Text.text = $"Score : {score}";
        }

        public void EnableLevel2()
        {
            ButtonLevel2.interactable = true;
        }
        
        public void EnableLevel3()
        {
            ButtonLevel3.interactable = true;
        }

        public void SubScribe()
        {
            ButtonLevel1.onClick.AddListener(() => OnClickPlayLevel1?.Invoke());
            ButtonLevel2.onClick.AddListener(() => OnClickPlayLevel2?.Invoke());
            ButtonLevel3.onClick.AddListener(() => OnClickPlayLevel3?.Invoke());
        }
    }
}