using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source
{
    /// <summary>
    /// Controls main game operations
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private Player player;
        private bool GameEnded = false;

        public const string BestScorePref = "BestScore";
        private DateTime gameStartTime;
        private int additionalScore = 0;
        public int Score = 0;
        public int BestScore = 0;

        public void Awake()
        {
            Instance = FindObjectOfType<GameManager>();
            player = FindObjectOfType<Player>();
            BestScore = PlayerPrefs.GetInt(BestScorePref);
            gameStartTime = DateTime.Now;
        }

        public void Update()
        {
            Score = (int)Math.Round((DateTime.Now - gameStartTime).TotalSeconds) + additionalScore;
        }

        public void BaloonDead()
        {
            if (GameEnded)
                return;
            GameEnded = true;

            player.Dead();
            player.PlayerCharacterController.Freeze = true;

            FindObjectOfType<UIManager>().ShowGameEndPanel();
        }

        /// <summary>
        /// Adds value to currentr score
        /// </summary>
        /// <param name="s"></param>
        public void AddScore(int s)
        {
            additionalScore += s;
        }

        public void SaveScore()
        {
            if (Score > BestScore)
                PlayerPrefs.SetInt(BestScorePref, Score);
        }

        public void OnApplicationQuit()
        {
            SaveScore();
        }
    }
}
