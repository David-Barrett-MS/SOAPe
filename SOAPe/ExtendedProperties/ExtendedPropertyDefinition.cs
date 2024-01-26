using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPe.ExtendedProperties
{
    public class ExtendedPropertyDefinition
    {
        // <ExtendedFieldURI DistinguishedPropertySetId="" PropertySetId="" PropertyTag="" PropertyName="" PropertyId="" PropertyType="" />
        private string _xml="";
        const string _xmlns = "http://schemas.microsoft.com/exchange/services/2006/types";

        public ExtendedPropertyDefinition(DefaultExtendedPropertySet defaultExtendedPropertySet, int Id, MapiPropertyType mapiPropertyType)
        {
            _xml = $"<ExtendedFieldURI DistinguishedPropertySetId=\"{defaultExtendedPropertySet}\" PropertyId=\"{Id}\" PropertyType=\"{mapiPropertyType}\" xmlns=\"{_xmlns}\" />";
        }

        public ExtendedPropertyDefinition(DefaultExtendedPropertySet defaultExtendedPropertySet, string name, MapiPropertyType mapiPropertyType)
        {
            _xml = $"<ExtendedFieldURI DistinguishedPropertySetId=\"{defaultExtendedPropertySet}\" PropertyName=\"{name}\" PropertyType=\"{mapiPropertyType}\" xmlns=\"{_xmlns}\" />";
        }

        public ExtendedPropertyDefinition(Guid propertySetId, int Id, MapiPropertyType mapiPropertyType)
        {
            _xml = $"<ExtendedFieldURI PropertySetId=\"{propertySetId}\" PropertyId=\"{Id}\" PropertyType=\"{mapiPropertyType}\" xmlns=\"{_xmlns}\" />";
        }

        public ExtendedPropertyDefinition(Guid propertySetId, string name, MapiPropertyType mapiPropertyType)
        {
            _xml = $"<ExtendedFieldURI PropertySetId=\"{propertySetId}\" PropertyName=\"{name}\" PropertyType=\"{mapiPropertyType}\" xmlns=\"{_xmlns}\" />";
        }

        public ExtendedPropertyDefinition(int Id, MapiPropertyType mapiPropertyType)
        {
            _xml = $"<ExtendedFieldURI PropertyTag=\"{Id}\" PropertyType=\"{mapiPropertyType}\" xmlns=\"{_xmlns}\" />";
        }

        public string Xml
        {
            get { return _xml; }
        }
    }
}
