using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class GameManager : MonoBehaviour
    {
        private event Action<string, object> _dataToUIManagerDelegate;

        [Header("References")]

        [Tooltip("The player gameobject")]
        [SerializeField] private Player _player = null;

        [Tooltip("The UIManager component which manages the current scene's UI")]
        [SerializeField] private UIManager _uiManager = null;

        [Tooltip("The EnemyManager component which manages the current scene's Enemy")]
        [SerializeField] private ScoreManager _scoreManager = null;

        [Tooltip("The InputManager component which manages the current scene's")]
        [SerializeField] private InputManager _inputManager = null;

        [Header("Scores")]
        [Tooltip("The player's score")]
        [SerializeField] private int _gameScore = 0;

        [Tooltip("The highest score acheived on this device")]
        [SerializeField] private int _highScore = 0;

        public int score
        {
            get
            {
                return _gameScore;
            }
            set
            {
                _gameScore = (value > 0) ? value : 0;
            }
        }


        private void Awake()
        {

        }

        void Start()
        {
            Time.timeScale = 1;
            if (PlayerPrefs.HasKey("highscore"))
            {
                _highScore = PlayerPrefs.GetInt("highscore");
            }
            if (PlayerPrefs.HasKey("score"))
            {
                score = PlayerPrefs.GetInt("score");
            }
            SubscribeUIManager();
            SubscribePlayer();
            SubscribeInputManager();
            SubscribeScoreManager();
            _dataToUIManagerDelegate?.Invoke("score", score);
            _dataToUIManagerDelegate?.Invoke("highscore", _highScore);
        }

        private void SubscribeScoreManager()
        {
            if (_scoreManager != null)
            {
                _scoreManager.SubscribeAddScore(AddScore);
            }
        }

        private void UnsubscribeScoreManager()
        {
            if (_scoreManager != null)
            {
                _scoreManager.UnsubscribeAddScore(AddScore);
            }
        }

        private void SubscribeInputManager()
        {
            if (_inputManager != null)
            {
                _inputManager.SubscribePause(PauseGame);
                _inputManager.SubscribeUnPause(UnPauseGame);
            }
        }

        private void UnsubscribeInputManager()
        {
            if (_inputManager != null)
            {
                _inputManager.UnsubscribePause(PauseGame);
                _inputManager.UnsubscribeUnPause(UnPauseGame);
            }
        }

        private void SubscribeUIManager()
        {
            if (_uiManager != null)
            {
                _dataToUIManagerDelegate += _uiManager.DataProcess;
            }
        }

        private void UnsubscribeUIManager()
        {
            if (_uiManager != null)
            {
                _dataToUIManagerDelegate -= _uiManager.DataProcess;
            }
        }

        private void SubscribePlayer()
        {
            if (_player != null)
            {
                _player.SubscribeUpdateLives(UpdateLives);
                _player.SubscribeUpdateHealth(UpdateHealth);
                _player.SubscribeLevelCompleted(LevelCompleted);
                _player.SubscribeGameOver(GameOver);
                _player.GetCurrentLivesAndHealth();

            }
        }

        private void UnsubscribePlayer()
        {
            if (_player != null)
            {
                _player.UnsubscribeUpdateLives(UpdateLives);
                _player.UnsubscribeUpdateHealth(UpdateHealth);
                _player.UnsubscribeLevelCompleted(LevelCompleted);
                _player.UnsubscribeGameOver(GameOver);
                _player.GetCurrentLivesAndHealth();

            }
        }

        private void OnApplicationQuit()
        {
            SaveHighScore();
            ResetScore();
        }

        private void LevelCompleted()
        {
            SaveScore();
            Time.timeScale = 0;
            _dataToUIManagerDelegate?.Invoke("levelCompleted", true);
            UnsubscribeUIManager();
            UnsubscribePlayer();
            UnsubscribeInputManager();
            UnsubscribeScoreManager();


        }

        private void GameOver()
        {
            ResetScore();
            Time.timeScale = 0;
            _dataToUIManagerDelegate?.Invoke("gameOver", true);
            UnsubscribeUIManager();
            UnsubscribePlayer();
            UnsubscribeInputManager();
            UnsubscribeScoreManager();
        }

        private void SaveHighScore()
        {
            PlayerPrefs.SetInt("highscore", _highScore);
            _dataToUIManagerDelegate?.Invoke("highscore", _highScore);
        }

        private void ResetScore()
        {
            score = 0;
            SaveScore();
            _dataToUIManagerDelegate?.Invoke("score", score);
        }

        private void SaveScore()
        {
            PlayerPrefs.SetInt("score", score);
        }

        private void AddScore(int scoreAmount)
        {
            score += scoreAmount;
            _dataToUIManagerDelegate?.Invoke("score", score);
            if(score > _highScore)
            {
                _highScore = score;
                SaveHighScore();
            }
        }

        private void UpdateLives(int currentLives)
        {
            _dataToUIManagerDelegate?.Invoke("lives", currentLives);
        }

        private void UpdateHealth(int currentHealth)
        {
            _dataToUIManagerDelegate?.Invoke("health", currentHealth);
        }

        private void PauseGame()
        {
            _dataToUIManagerDelegate?.Invoke("pause", true);
            Time.timeScale = 0;
        }

        public void UnPauseGame()
        {
            _dataToUIManagerDelegate?.Invoke("unpause", false);
            Time.timeScale = 1;
        }
    }
}