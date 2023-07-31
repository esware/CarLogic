using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        #region Inspector Properties
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private GameObject winObject;
        [SerializeField] private GameObject loseObject;
        #endregion

        #region Unity Methods
        private void Start()
        {
            InitializeCanvas();
            SignUpEvents();
        }
        #endregion

        #region Initialization
        private void InitializeCanvas()
        {
            DisableCanvasObjects();

            var level = PlayerPrefs.GetInt("PlayerLevel") + 1;
            levelText.text = "Level " + level;
        }
        #endregion

        #region Event Handling
        private void SignUpEvents()
        {
            SignUpGameOverEvents();
        }

        private void SignUpGameOverEvents()
        {
            GameEvents.LoseEvent += OnLoseEvent;
            GameEvents.WinEvent += OnWinEvent;
        }

        private void OnWinEvent()
        {
            winObject.SetActive(true);
        }

        private void OnLoseEvent()
        {
            loseObject.SetActive(true);
        }
        #endregion

        #region Helper Methods
        private void DisableCanvasObjects()
        {
            winObject.SetActive(false);
            loseObject.SetActive(false);
        }
        #endregion
    }
}
