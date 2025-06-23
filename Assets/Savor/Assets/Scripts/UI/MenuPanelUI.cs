using Lean.Transition;
using TMPro;
using Undercooked.Data;
using Undercooked.Managers;
using Undercooked.Utils; // Supaya Singleton<T> dikenali
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Undercooked.UI
{
    public class MenuPanelUI : Singleton<MenuPanelUI>
    {
        [Header("InitialMenu")]
        [SerializeField] private GameObject initialMenu;
        private CanvasGroup _initialMenuCanvasGroup;

        [Header("GameOverMenu")]
        [SerializeField] private GameObject gameOverMenu;
        private CanvasGroup _gameOverMenuCanvasGroup;
        [SerializeField] private GameObject firstSelectedGameOverMenu;
        [SerializeField] private AudioClip successClip;

        [Header("Buttons")]
        [SerializeField] private Button restartButton_GameOver;
        [SerializeField] private Button quitButton_GameOver;

        [Header("GameOver Stars")]
        [SerializeField] private Image star1;
        [SerializeField] private Image star2;
        [SerializeField] private Image star3;
        [SerializeField] private TextMeshProUGUI scoreStar1Text;
        [SerializeField] private TextMeshProUGUI scoreStar2Text;
        [SerializeField] private TextMeshProUGUI scoreStar3Text;
        [SerializeField] private TextMeshProUGUI scoreText;

        public delegate void ButtonPressed();
        public static ButtonPressed OnRestartButton;
        public static ButtonPressed OnQuitButton;

        private void Awake()
        {
            _initialMenuCanvasGroup = initialMenu.GetComponent<CanvasGroup>();
            _gameOverMenuCanvasGroup = gameOverMenu.GetComponent<CanvasGroup>();

#if UNITY_EDITOR
            Assert.IsNotNull(initialMenu);
            Assert.IsNotNull(gameOverMenu);
            Assert.IsNotNull(_initialMenuCanvasGroup);
            Assert.IsNotNull(_gameOverMenuCanvasGroup);

            Assert.IsNotNull(restartButton_GameOver);
            Assert.IsNotNull(quitButton_GameOver);

            Assert.IsNotNull(star1);
            Assert.IsNotNull(star2);
            Assert.IsNotNull(star3);
            Assert.IsNotNull(scoreStar1Text);
            Assert.IsNotNull(scoreStar2Text);
            Assert.IsNotNull(scoreStar3Text);
            Assert.IsNotNull(scoreText);
#endif

            initialMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            _initialMenuCanvasGroup.alpha = 0f;
            _gameOverMenuCanvasGroup.alpha = 0f;
        }

        private void OnEnable() => AddButtonListeners();
        private void OnDisable() => RemoveButtonListeners();

        private void AddButtonListeners()
        {
            restartButton_GameOver.onClick.AddListener(HandleRestartButton);
            quitButton_GameOver.onClick.AddListener(HandleQuitButton);
        }

        private void RemoveButtonListeners()
        {
            restartButton_GameOver.onClick.RemoveAllListeners();
            quitButton_GameOver.onClick.RemoveAllListeners();
        }

        private static void HandleRestartButton()
        {
            GameOverMenu(false); // Tutup game over menu saat restart
            OnRestartButton?.Invoke();
        }

        private static void HandleQuitButton() => OnQuitButton?.Invoke();

        public static void InitialMenuSetActive(bool active)
        {
            Instance.initialMenu.SetActive(active);
            Instance._initialMenuCanvasGroup.alphaTransition(active ? 1f : 0f, 2f);

            // Jika menu ditutup, paksa semua PlayerInput switch ke PlayerControls
            if (!active)
            {
                foreach (var player in UnityEngine.Object.FindObjectsOfType<UnityEngine.InputSystem.PlayerInput>())
                {
                    player.SwitchCurrentActionMap("PlayerControls");
                }
            }
        }

        public static void GameOverMenu(bool show = true)
        {
            if (show)
            {
                Instance.gameOverMenu.SetActive(true);
                Time.timeScale = 0;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(Instance.firstSelectedGameOverMenu);
                Instance._gameOverMenuCanvasGroup.alphaTransition(1f, .5f);
                UpdateStars();
            }
            else
            {
                Instance._gameOverMenuCanvasGroup
                    .alphaTransition(0f, .5f)
                    .JoinTransition()
                    .EventTransition(() => Instance.gameOverMenu.SetActive(false))
                    .EventTransition(() => Time.timeScale = 1);
            }
        }

        private static void UpdateStars()
        {
            int score = GameManager.Score;
            LevelData levelData = GameManager.LevelData;
            int star1Score = levelData.star1Score;
            int star2Score = levelData.star2Score;
            int star3Score = levelData.star3Score;

            Instance.scoreStar1Text.text = star1Score.ToString();
            Instance.scoreStar2Text.text = star2Score.ToString();
            Instance.scoreStar3Text.text = star3Score.ToString();
            Instance.scoreText.text = $"Score {score}";

            Instance.star1.transform.localScale = Vector3.zero;
            Instance.star2.transform.localScale = Vector3.zero;
            Instance.star3.transform.localScale = Vector3.zero;

            if (score >= star1Score)
            {
                Instance.star1.transform.localScaleTransition(Vector3.one, 1f, LeanEase.Bounce);
            }

            if (score >= star2Score)
            {
                Instance.star2.transform.localScaleTransition(Vector3.one, 1f, LeanEase.Bounce)
                        .JoinTransition();
            }

            if (score >= star3Score)
            {
                Instance.star3.transform.localScaleTransition(Vector3.one, 1f, LeanEase.Bounce)
                        .JoinTransition();
            }
        }

        private async Task StartMainMenuAsync()
        {
            await Task.Delay(1000);
            MenuPanelUI.InitialMenuSetActive(true);

            // ... kode deteksi start ...

            MenuPanelUI.InitialMenuSetActive(false);

            // Tambahkan ini untuk memastikan action map berpindah ke PlayerControls
            foreach (var player in UnityEngine.Object.FindObjectsOfType<UnityEngine.InputSystem.PlayerInput>())
            {
                player.SwitchCurrentActionMap("PlayerControls");
            }
        }
    }
}
