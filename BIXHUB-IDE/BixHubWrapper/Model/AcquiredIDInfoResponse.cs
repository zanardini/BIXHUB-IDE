using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BixHubWrapper.Model
{
    public class AcquiredIDInfoResponse
    {
        public PersonalDataAcquiredInfoResponse PersonalData { get; set; }
        public IDCardAcquiredResponse IDCard { get; set; }
        public ResourceResponse Selfie { get; set; }
        public ResourceResponse LivenessDetection { get; set; }

        public DigitalIdentityResponse DigitalIdentity { get; set; }
    }

    public class PersonalDataAcquiredInfoResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneVerified { get; set; }
        public Sex Sex { get; set; }
        public string Email { get; set; }
    }

    public class IDCardAcquiredResponse
    {
        public string DocumentIdentifier { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DocumentType DocumentType { get; set; }
        //TODO mai valorizzato, l'errore era nascosto prima che rimuovessi automapper
        public string MimeType { get; set; }

        public byte[] FrontFileData { get; set; }

        public byte[] RearFileData { get; set; }
    }

    public class ResourceResponse
    {
        public string MimeType { get; set; }

        public byte[] FileData { get; set; }
    }

    public class DigitalIdentityResponse
    {
        public string Request { get; set; }
        public string Response { get; set; }
    }
    public enum DocumentType
    {
        IdentityCard = 1,
        DrivingLicense = 2,
        Passport = 3,
    }
}
