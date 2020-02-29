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

        public string displayName { get; set; }

        public string fullName { get; set; }
    }
}