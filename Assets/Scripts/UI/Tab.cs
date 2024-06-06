using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class Tab : MonoBehaviour
    {
        [SerializeField] private TabName _tabName;
        public TabName Name => _tabName;

        public void ShowTab()
        {
            this.gameObject.SetActive(true);
        }
        
        public void HideTab()
        {
            this.gameObject.SetActive(false);
        }

    }

}
