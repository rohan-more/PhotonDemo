using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    
    public enum TabName { LOADING, LOBBY, CREATE, ROOM, FIND_ROOM}
    public class TabManager : MonoBehaviour
    {
        [SerializeField] private List<Tab> tabs;

        private void Awake()
        {
            SwitchTab(TabName.LOADING);
        }

        public void OnEnable()
        {
            Events.ShowTab += SwitchTab;
        }

        public void OnDisable()
        {
            Events.ShowTab -= SwitchTab;
        }


        private void SwitchTab(TabName tab)
        {
            foreach (var item in tabs)
            {
                if (tab == item.Name)
                {
                    item.ShowTab();
                }
                else
                {
                    item.HideTab();
                }
            }
        }
    }
}

