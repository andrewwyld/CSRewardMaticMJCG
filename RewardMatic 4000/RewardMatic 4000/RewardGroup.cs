#nullable enable
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RewardMatic_4000
{
    public class RewardGroup
    {
        private readonly List<Reward> _groupRewards;

        public List<Reward> rewards { get { return this._groupRewards; } }

        [JsonProperty("name")]
        public string Name { get; }

        public RewardGroup(string name, List<Reward> groupRewards)
        {
            Name = name;
            _groupRewards = groupRewards ?? new List<Reward>();
        }

        public bool ContainsReward(Reward reward)
        {
            foreach (var itemReward in this._groupRewards)
            {
                if (itemReward == reward)
                {
                    return true;
                }
            }
            return false;
        }

        public Reward? GetRewardByIndex(int i)
        {
            if (i < _groupRewards.Count)
            {
                return _groupRewards[i];
            }
            else
            {
                return null;
            }
        }
    }
}