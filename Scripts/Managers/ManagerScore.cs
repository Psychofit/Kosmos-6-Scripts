using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using IG;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace Cosmos_Six
{
    public class ManagerScore : SingletonManager<ManagerScore>
    {
        public int Score;
        public Text ScoreText;
        public Action<int> OnAddScore;
        public Action<int> OnNewScore;
        public UnityEvent<int> EventAddScore;

        private void Start()
        {
            if (ScoreText == null)
            {
                Debug.LogError("ManargeScore. ScoreText null");
            }
            else
            {
                ScoreText.text = Score.ToString();
            }
        }

        private void OnEnable()
        {
            OnAddScore += IncreaseScore;
        }

        private void OnDisable()
        {
            OnAddScore -= IncreaseScore;
        }

        public void AddScore(int scoreDelta)
        {
            OnAddScore?.Invoke(scoreDelta);
        }

        public void IncreaseScore(int scoreDelta)
        {
            Score += scoreDelta;
            ScoreText.text = Score.ToString();
            OnNewScore?.Invoke(Score);
            EventAddScore.Invoke(scoreDelta);
        }

        public void ReactOnAddScore()
        {
            Debug.Log("ReactOnAddScore");
        }
    }
}