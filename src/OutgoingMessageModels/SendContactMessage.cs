using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendContactMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "contacts";

        [JsonProperty("contacts")]
        public List<SendContact> Contacts { get; set; } = [];
    }

    public class SendContact
    {
        public List<SendContactAddressObject> Addresses { get; set; } = [];

        private string _birthday = string.Empty;
        public string Birthday
        {
            get => _birthday;
            set => _birthday = DateTime.TryParse(value, out var date)
                ? date.ToString("yyyy-MM-dd")
                : value;
        }

        [JsonProperty("emails")]
        public List<SendContactEmailObject> Emails { get; set; } = [];

        [JsonProperty("name")]
        public SendContactNameObject? Name { get; set; }

        [JsonProperty("org")]
        public SendContactOrganizationObject? Org { get; set; }

        [JsonProperty("phones")]
        public List<SendContactPhoneObject> Phones { get; set; } = [];

        [JsonProperty("urls")]
        public List<SendContactUrlObject> Urls { get; set; } = [];
    }

    public class SendContactAddressObject
    {
        /// <summary>
        /// Optional - Contact's street name
        /// </summary>
        [JsonProperty("street")]
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's City
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's State
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's Zip Code
        /// </summary>
        [JsonProperty("zip")]
        public string Zip { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's Country
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's Country Codd
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's Address Type
        /// eg. Work address, Home address etc
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public class SendContactEmailObject
    {
        /// <summary>
        /// Optional - Contact's Email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's Email's type
        /// eg. Work email, Business email, Personal email
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public class SendContactNameObject
    {
        /// <summary>
        /// Required - Contact's formatted name. This will appear in the message alongside the profile arrow button
        /// </summary>
        [JsonProperty("formatted_name")]
        public required string FormattedName { get; set; }

        /// <summary>
        /// Optional - Contact's first name
        /// </summary>
        [JsonProperty("first_name")]
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's middle name
        /// </summary>
        [JsonProperty("middle_name")]
        public string Middlename { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Contact's lastname name
        /// </summary>
        [JsonProperty("last_name")]
        public string Lastname { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Prefix for the contact's name, such as Mr., Ms., Dr., etc
        /// </summary>
        [JsonProperty("prefix")]
        public string Prefix { get; set; } = string.Empty;

        /// <summary>
        /// Optional - Suffix for the contact's name, if applicable
        /// eg. Esq
        /// </summary>
        [JsonProperty("suffix")]
        public string Suffix { get; set; } = string.Empty;
    }

    public class SendContactOrganizationObject
    {
        /// <summary>
        /// Optional - Name of Company
        /// </summary>
        [JsonProperty("company")]
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Contact's Department
        /// </summary>
        [JsonProperty("department")]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Contact's Job Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
    }

    public class SendContactPhoneObject
    {
        /// <summary>
        /// Tel/Phone Number
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Type of Phone number
        /// eg. Work Phone, Personal Phone 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public class SendContactUrlObject
    {
        /// <summary>
        /// Optional - Link to online resourse (website, blog etc)
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; } = "https://gcbbank.com.gh";

        /// <summary>
        /// Optional - Describes what type Url is
        /// eg. Company site, personal blog etc
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = "website";
    }
}
