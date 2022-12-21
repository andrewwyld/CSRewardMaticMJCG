#nullable enable
using Newtonsoft.Json;

namespace RewardMatic_4000
{
    public class Reward
    {
        public static Reward[] AvailableRewards = RepositoryReward.allRewards();

        public Reward(int scoreDifferential, string message)
        {
            ScoreDifferential = scoreDifferential;
            Message = message;
        }

        [JsonProperty("scoredifferential")]
        public int ScoreDifferential { get; }

        [JsonProperty("name")]
        public string Message { get; }

        public override bool Equals(object? otherReward)
        {
            if (otherReward == null || !(otherReward is Reward))
            {
                return false;
            }

            Reward castedReward = (Reward)otherReward;
            return castedReward == this;
        }

        public static bool operator ==(Reward? lhs, Reward? rhs)
        {
            // Check if lhs or rhs is null
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return false;
            }

            // Compare the fields of lhs and rhs
            return lhs.Message == rhs.Message && lhs.ScoreDifferential == rhs.ScoreDifferential;
        }

        public static bool operator !=(Reward lhs, Reward rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            // Combine the hash codes of the fields using the XOR operator
            return this.Message.GetHashCode() ^ this.ScoreDifferential.GetHashCode();
        }

    }
}