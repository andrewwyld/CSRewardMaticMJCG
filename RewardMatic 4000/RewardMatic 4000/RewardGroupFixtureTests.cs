using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace RewardMatic_4000
{
    public class RewardGroupFixtureTests
    {
        protected string _json;

        public RewardGroupFixtureTests()
        {
            // Load the JSON file and initialize the _json field
            string jsonFilePath = "../../../rewards.json";
            _json = File.ReadAllText(jsonFilePath);
        }

        [Test]
        public void TestDeserializationGroupRewards()
        {
            var aList = RepositoryReward.allGroups[0].rewards;
            var aGroup = RepositoryReward.allGroups[0];
            Assert.AreEqual(aGroup.Name, "Getting Started");
            Assert.NotNull(aGroup.rewards);

            Assert.NotNull(aGroup);
            Assert.AreEqual("Starting strong!", aList[0].Message);
            Assert.AreEqual(200, aList[0].ScoreDifferential);
            Assert.AreEqual("Nice job!", aList[1].Message);
            Assert.AreEqual(300, aList[1].ScoreDifferential);
            Assert.Greater(aList.Count, 0);

            foreach (var group in RepositoryReward.allGroups)
            {
                foreach (var reward in group.rewards)
                {
                    Assert.Greater(reward.ScoreDifferential, 0);
                }
            }
        }


        [Test]
        public void TestEveryRewardBelongsToItsGroup()
        {
            foreach (var group in RepositoryReward.allGroups)
            {
                foreach (var reward in group.rewards)
                {
                    Assert.IsTrue(group.ContainsReward(reward));
                }
            }
        }

        [Test]
        public void TestGroupLatestRewardReceive()
        {
            var firstGroup = RepositoryReward.allGroups[0];
            var scoredForCompleteGroup = firstGroup.rewards.Select(r => r.ScoreDifferential).Aggregate((accumulator, current) => accumulator + current);
            var scoredForThreeRewards = firstGroup.rewards[0].ScoreDifferential + firstGroup.rewards[1].ScoreDifferential + firstGroup.rewards[2].ScoreDifferential;

            var userInFirstGroup = new User();
            userInFirstGroup.UpdateScore(scoredForThreeRewards);
            Assert.AreEqual(userInFirstGroup.GetRewardGroupForLatestRewardReceived(), firstGroup);

            var userStillInFirstGroup = new User();
            userStillInFirstGroup.UpdateScore(scoredForCompleteGroup);
            Assert.AreEqual(userStillInFirstGroup.GetRewardGroupForLatestRewardReceived(), firstGroup);

        }

        [Test]
        public void TestGroupRewardInProgress()
        {
            var firstGroup = RepositoryReward.allGroups[0];
            var secondGroup = RepositoryReward.allGroups[1];
            var scoredForCompleteGroup = firstGroup.rewards.Select(r => r.ScoreDifferential).Aggregate((accumulator, current) => accumulator + current);
            var scoredForThreeRewards = firstGroup.rewards[0].ScoreDifferential + firstGroup.rewards[1].ScoreDifferential + firstGroup.rewards[2].ScoreDifferential;

            var userInFirstGroup = new User();
            userInFirstGroup.UpdateScore(scoredForThreeRewards);
            Assert.AreEqual(userInFirstGroup.GetRewardGroupForRewardInProgress(), firstGroup);

            var useInSecondGroup = new User();
            useInSecondGroup.UpdateScore(scoredForCompleteGroup);
            Assert.AreEqual(useInSecondGroup.GetRewardGroupForRewardInProgress(), secondGroup);
        }

    }
}