using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source
{
    /// <summary>
    /// Makes scene transitions
    /// </summary>
    public class SceneTransitionManager : MonoBehaviour
    {
        public GameObject Panel;
        private float panelAlpha = 1F;
        public const float TransitionSpeed = 1.5F;
        private bool isInTransitionFrom = true;
        private bool isInTransitionTo = false;

        public void Start()
        {
            Panel.SetActive(true);
        }

        public void Update()
        {
            if (isInTransitionFrom)
            {
                panelAlpha -= TransitionSpeed * Time.deltaTime * TransitionSpeed;
                Panel.GetComponent<Image>().color = new Color(0, 0, 0, panelAlpha);
                if (panelAlpha <= 0)
                    isInTransitionFrom = false;
            }
            if (isInTransitionTo)
            {
                panelAlpha += TransitionSpeed * Time.deltaTime * TransitionSpeed;
                Panel.GetComponent<Image>().color = new Color(0, 0, 0, panelAlpha);
                if (panelAlpha >= 1)
                    isInTransitionTo = false;
            }
        }

        public void DoTransition()
        {
            panelAlpha = 0;
            isInTransitionTo = true;
        }
    }
}
