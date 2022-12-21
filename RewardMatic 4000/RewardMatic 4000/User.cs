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

        protected int GetRewardInProgressIndex()
        {
            var accScoreDifferential = Reward.AvailableRewards[0].ScoreDifferential;
            if (this.Score <= accScoreDifferential)
            {
                return 0;
            }

            for (int index = 0; index < Reward.AvailableRewards.Length; index++)
            {
                var currentReward = Reward.AvailableRewards[index];
                if (this.Score <= accScoreDifferential)
                {
                    return index;
                }
                accScoreDifferential += currentReward.ScoreDifferential;
            }

            return Reward.AvailableRewards.Length;
        }

        public Reward? GetRewardInProgress()
        {
            var possibleRewardInProgressIndex = this.GetRewardInProgressIndex();
            if (possibleRewardInProgressIndex >= Reward.AvailableRewards.Length)
            {
                return null;
            }
            return Reward.AvailableRewards[possibleRewardInProgressIndex];
        }

        protected RewardGroup? GetRewardGroup(Reward? rewardToCheck)
        {
            if (rewardToCheck == null)
            {
                return null;
            }
            foreach (var group in RepositoryReward.allGroups)
            {
                if (group.ContainsReward(rewardToCheck))
                {
                    return group;
                }
            }
            return null;
        }

        public RewardGroup? GetRewardGroupForRewardInProgress()
        {
            var rewardInProgress = this.GetRewardInProgress();
            var possibleGroup = this.GetRewardGroup(rewardInProgress);
            if (possibleGroup == null || rewardInProgress == null)
            {
                return null;
            }

            var lastReward = possibleGroup.rewards[possibleGroup.rewards.Count - 1];
            if (lastReward != rewardInProgress)
            {
                return possibleGroup;
            }

            return RepositoryReward.nextGroup(possibleGroup);

        }

        public RewardGroup? GetRewardGroupForLatestRewardReceived()
        {
            return this.GetRewardGroup(this.GetLatestRewardReceived());
        }

        public Reward? GetLatestRewardReceived()
        {
            var possibleRewardInProgressIndex = this.GetRewardInProgressIndex();
            if (possibleRewardInProgressIndex == 0)
            {
                return null;
            }
            return Reward.AvailableRewards[possibleRewardInProgressIndex - 1];
        }
    }
}