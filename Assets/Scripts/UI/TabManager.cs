using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Core.UI
{
    
    public enum TabName { LOADING, LOBBY, CREATE, ROOM, FIND_ROOM}
    public class TabManager : MonoBehaviour
    {
        [SerializeField] private List<Tab> tabs;
        [SerializeField] private CanvasGroup panelCanvasGroup;
        [SerializeField] private float fadeDuration = 1.0f;
        private void Awake()
        {
            if (panelCanvasGroup == null)
            {
                panelCanvasGroup = GetComponent<CanvasGroup>();
            }
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
        
        private void FadeIn()
        {
            panelCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.InOutQuad);
        }

        private void FadeOut()
        {
            panelCanvasGroup.DOFade(0, fadeDuration).SetEase(Ease.InOutQuad);
        }


        private void SwitchTab(TabName tab)
        {
            foreach (var item in tabs)
            {
                if (tab == item.Name)
                {
                    FadeOut();
                    item.ShowTab();
                }
                else
                {
                    FadeIn();
                    item.HideTab();
                }
            }
        }
    }
}

