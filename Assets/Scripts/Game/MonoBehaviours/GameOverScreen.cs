using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Asteroids.Game.MonoBehaviours
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        public void Init(int score)
        {
            _scoreText.text = score.ToString();
        }

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}