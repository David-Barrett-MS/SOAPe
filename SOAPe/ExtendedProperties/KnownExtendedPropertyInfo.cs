using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPe.ExtendedProperties
{
    public struct KnownExtendedPropertyInfo
    {
        public string CanonicalNames;
        public string AlternateNames;
        public string Areas;
        public string References;

        public KnownExtendedPropertyInfo(
            string canonicalNames,
            string alternateNames,
            string areas,
            string references)
        {
            CanonicalNames = canonicalNames;
            AlternateNames = alternateNames;
            Areas = areas;
            References = references;
        }

        public override string ToString()
        {
            return CanonicalNames;
        }
    }
}
