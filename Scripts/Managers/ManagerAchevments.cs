using System.Collections;
using System.Collections.Generic;
using IG;
using UnityEngine;

namespace Cosmos_Six
{
    public class ManagerAchevments : SingletonManager<ManagerAchevments>
    {
        private void OnEnable()
        {
            ManagerScore.Instance.OnNewScore += CheckAchievements;
        }

        private void OnDisable()
        {
            ManagerScore.Instance.OnNewScore -= CheckAchievements;
        }

        private void CheckAchievements(int newScore)
        {
            if (newScore > 9)
            {
                Debug.Log("You are the CHAMPION!");
            }
        }
    }
}