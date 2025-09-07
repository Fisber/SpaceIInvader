using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Score;
        [SerializeField] private TextMeshProUGUI YouWonText;
        [SerializeField] private Button Button;
        [SerializeField] private Button Next;
        [SerializeField] private Button RestartOnWinButton;
        [SerializeField] private Button RestartOnLoseButton;
        [SerializeField] private GameObject WinPopup;
        [SerializeField] private GameObject LosePopup;

        public event Action OnClick;
        public event Action OnRestart;
        public event Action OnNext;

        public void Initialize()
        {
            Button.onClick.AddListener(() => OnClick?.Invoke());
            Next.onClick.AddListener(() => OnNext?.Invoke());
            RestartOnWinButton.onClick.AddListener(() => OnRestart?.Invoke());
            RestartOnLoseButton.onClick.AddListener(() => OnRestart?.Invoke());
        }

        public void SetYouWonText(int level)
        {
            YouWonText.text = $"you have passed level {level + 1}";
        }

        public void ShowWinPopup()
        {
            WinPopup.SetActive(true);
        }

        public void ShowLosePopup()
        {
            LosePopup.SetActive(true);
        }


        public void SetScore(int score)
        {
            Score.text = $"Score : {score}";
        }

      
    }
}