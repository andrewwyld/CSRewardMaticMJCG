using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RewardMatic_4000
{
    public class RepositoryReward
    {
        // Deserialize the JSON data into an instance of the MyData class
        public readonly static List<RewardGroup> allGroups = JsonConvert.DeserializeObject<List<RewardGroup>>(File.ReadAllText("../../../rewards.json"));
        // public readonly static List<RewardGroup> allGroups = JsonConvert.DeserializeObject<List<RewardGroup>>(File.ReadAllText("rewards.json"));
        public static Reward[] allRewards()
        {
            return RepositoryReward.allGroups.SelectMany(x => x.rewards).ToArray();
        }

        internal static RewardGroup nextGroup(RewardGroup possibleGroup)
        {
            for (int i = 0; i < allGroups.Count - 1; i++)
            {
                if (allGroups[i] == possibleGroup)
                {
                    return allGroups[i + 1];
                }
            }

            return null;
        }
    }
}