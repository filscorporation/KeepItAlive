using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Source
{
    /// <summary>
    /// Controlls main menu
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject AboutText;
        public Text BestScoreText;

        private const string GameSceneName = "GameScene";

        public void Start()
        {
            // Load highscore
            int best = PlayerPrefs.GetInt(GameManager.BestScorePref);
            BestScoreText.text = $"Best: {best}";
        }

        public void PlayButtonClick()
        {
            SceneManager.LoadScene(GameSceneName);
        }

        public void AboutButtonClick()
        {
            AboutText.SetActive(!AboutText.activeInHierarchy);
        }

        public void ExitButtonClick()
        {
            Application.Quit();
        }
    }
}
