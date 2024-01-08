using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BixHubWrapper.Model
{
    internal class CreateSessionRequest
    {
        public Guid? InstitutionGuid { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public Dictionary<string, string> Metadata { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public PrimaryPersonalData PersonalData { get; set; }
        public SessionFlowType FlowType { get; set; }
    }


    public partial class PrimaryPersonalData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlaceOfBirth { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Sex Sex { get; set; }
        public string Email { get; set; }
    }

    public enum Sex
    {
        NotKnown = 0,
        Male = 1,
        Female = 2,
        NotApplicable = 99
    }

    public enum SessionFlowType
    {
       NotKnown= 0, 
       Default = 1, 
        Spid = 2, 
        Cie = 3,
         NoVideo = 4
    }
}