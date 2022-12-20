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
            var accScoreDifferential = Reward.AvailableRewards[0].ScoreDifferential;
            if (this.Score <= accScoreDifferential)
            {
                return null;
            }

            for (int index = 0; index < Reward.AvailableRewards.Length; index++)
            {
                var currentReward = Reward.AvailableRewards[index];
                if (this.Score <= accScoreDifferential)
                {
                    return Reward.AvailableRewards[index - 1];
                }
                accScoreDifferential += currentReward.ScoreDifferential;
            }

            return Reward.AvailableRewards[Reward.AvailableRewards.Length - 1];

        }
    }
}