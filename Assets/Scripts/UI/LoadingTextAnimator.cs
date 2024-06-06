using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Core.UI
{
    public class LoadingTextAnimator : MonoBehaviour
    {
        public TMP_Text loadingText;
        public float dotSpeed = 0.5f; // Speed of dot animation
        private string baseText = "Loading";
    
        void Start()
        {
            if (loadingText == null)
            {
                Debug.LogError("LoadingText is not assigned in the inspector.");
                return;
            }
    
            StartCoroutine(AnimateLoadingText());
        }
    
        IEnumerator AnimateLoadingText()
        {
            int dotCount = 0;
    
            while (true)
            {
                string dots = new string('.', dotCount);
                loadingText.text = $"{baseText}{dots}";

                dotCount++;
                if (dotCount > 5)
                {
                    dotCount = 0;
                }

                yield return new WaitForSeconds(dotSpeed);
            }
        }
    }
}
