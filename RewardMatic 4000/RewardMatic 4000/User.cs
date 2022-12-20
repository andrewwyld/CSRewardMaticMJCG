#nullable enable
using System;
using System.Collections.Generic;

namespace RewardMatic_4000
{
    public class User
    {
        private int _score = 0;

        public User()
        {
        }

        public int Score
        {
            get { return _score; }
        }

        public void UpdateScore(int update)
        {
            _score += update;
        }

        public Reward? GetRewardInProgress()
        {
            var accScoreDifferential = Reward.AvailableRewards[0].ScoreDifferential;
            foreach (var currentReward in Reward.AvailableRewards)
            {
                if (this.Score < accScoreDifferential)
                {
                    return currentReward;
                }
                accScoreDifferential += currentReward.ScoreDifferential;
            }
            return null;
        }

        public Reward? GetLatestRewardReceived()
        {
            throw new NotImplementedException();
        }
    }
}