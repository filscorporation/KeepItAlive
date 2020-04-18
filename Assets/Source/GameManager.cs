using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance ?? (instance = FindObjectOfType<GameManager>());

        private Player player;
        private bool GameEnded = false;

        public void Start()
        {
            player = FindObjectOfType<Player>();
        }

        public void BaloonDead()
        {
            if (GameEnded)
                return;
            GameEnded = true;

            player.Dead();
            player.PlayerCharacterController.Freeze = true;
            Invoke(nameof(RestartGame), 3F);
        }

        private void RestartGame()
        {
            instance = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
