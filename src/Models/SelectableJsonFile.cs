using System;

namespace Pokedex.Pokerole.Models
{
    public class SelectableJsonFile
    {
        public override bool Equals(object obj)
        {
            if (obj is SelectableJsonFile y)
            {
                return string.Equals(displayName, y.displayName, StringComparison.InvariantCultureIgnoreCase) &&
                       string.Equals(fullName, y.fullName, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        protected bool Equals(SelectableJsonFile other)
        {
            return displayName == other.displayName && fullName == other.fullName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((displayName != null ? displayName.GetHashCode() : 0) * 397) ^ (fullName != null ? fullName.GetHashCode() : 0);
            }
        }

        public string displayName { get; set; }

        public string fullName { get; set; }
    }
}