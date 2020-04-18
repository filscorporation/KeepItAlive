using UnityEngine;

namespace Assets.Source
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance ?? (instance = FindObjectOfType<GameManager>());
    }
}
