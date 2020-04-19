﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Source
{
    /// <summary>
    /// Controlls UI
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        private const string MainMenuSceneName = "MenuScene";

        public GameObject GameEndPanel;
        public Text ScoreText;
        public Text BestScoreText;
        private Player player;

        public void Start()
        {
            BestScoreText.text = $"Best: {GameManager.Instance.BestScore}";
            player = FindObjectOfType<Player>();
        }

        public void Update()
        {
            ScoreText.text = $"Score: {GameManager.Instance.Score}";
            DrawActiveBonuses();
        }

        private void DrawActiveBonuses()
        {
            //player.Bonuses
        }

        public void ShowGameEndPanel()
        {
            GameEndPanel.SetActive(true);
        }

        public void ToMainMenuButtonClick()
        {
            GameManager.Instance.SaveScore();
            SceneManager.LoadScene(MainMenuSceneName);
        }

        public void RestartButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
