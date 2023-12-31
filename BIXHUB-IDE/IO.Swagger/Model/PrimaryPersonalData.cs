/* 
 * OnBoarding
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;
namespace IO.Swagger.Model
{
    /// <summary>
    /// PrimaryPersonalData
    /// </summary>
    [DataContract]
        public partial class PrimaryPersonalData :  IEquatable<PrimaryPersonalData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryPersonalData" /> class.
        /// </summary>
        /// <param name="firstName">firstName.</param>
        /// <param name="lastName">lastName.</param>
        /// <param name="placeOfBirth">placeOfBirth.</param>
        /// <param name="dateOfBirth">dateOfBirth.</param>
        /// <param name="address">address.</param>
        /// <param name="phoneNumber">phoneNumber.</param>
        /// <param name="sex">sex.</param>
        /// <param name="email">email.</param>
        public PrimaryPersonalData(string firstName = default(string), string lastName = default(string), string placeOfBirth = default(string), DateTime? dateOfBirth = default(DateTime?), string address = default(string), string phoneNumber = default(string), Sex sex = default(Sex), string email = default(string))
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PlaceOfBirth = placeOfBirth;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.Sex = sex;
            this.Email = email;
        }
        
        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        [DataMember(Name="firstName", EmitDefaultValue=false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>
        [DataMember(Name="lastName", EmitDefaultValue=false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets PlaceOfBirth
        /// </summary>
        [DataMember(Name="placeOfBirth", EmitDefaultValue=false)]
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Gets or Sets DateOfBirth
        /// </summary>
        [DataMember(Name="dateOfBirth", EmitDefaultValue=false)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name="address", EmitDefaultValue=false)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNumber
        /// </summary>
        [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets Sex
        /// </summary>
        [DataMember(Name="sex", EmitDefaultValue=false)]
        public Sex Sex { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        [DataMember(Name="email", EmitDefaultValue=false)]
        public string Email { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PrimaryPersonalData {\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  PlaceOfBirth: ").Append(PlaceOfBirth).Append("\n");
            sb.Append("  DateOfBirth: ").Append(DateOfBirth).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
            sb.Append("  Sex: ").Append(Sex).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as PrimaryPersonalData);
        }

        /// <summary>
        /// Returns true if PrimaryPersonalData instances are equal
        /// </summary>
        /// <param name="input">Instance of PrimaryPersonalData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PrimaryPersonalData input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.FirstName == input.FirstName ||
                    (this.FirstName != null &&
                    this.FirstName.Equals(input.FirstName))
                ) && 
                (
                    this.LastName == input.LastName ||
                    (this.LastName != null &&
                    this.LastName.Equals(input.LastName))
                ) && 
                (
                    this.PlaceOfBirth == input.PlaceOfBirth ||
                    (this.PlaceOfBirth != null &&
                    this.PlaceOfBirth.Equals(input.PlaceOfBirth))
                ) && 
                (
                    this.DateOfBirth == input.DateOfBirth ||
                    (this.DateOfBirth != null &&
                    this.DateOfBirth.Equals(input.DateOfBirth))
                ) && 
                (
                    this.Address == input.Address ||
                    (this.Address != null &&
                    this.Address.Equals(input.Address))
                ) && 
                (
                    this.PhoneNumber == input.PhoneNumber ||
                    (this.PhoneNumber != null &&
                    this.PhoneNumber.Equals(input.PhoneNumber))
                ) && 
                (
                    this.Sex == input.Sex ||
                    (this.Sex != null &&
                    this.Sex.Equals(input.Sex))
                ) && 
                (
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.FirstName != null)
                    hashCode = hashCode * 59 + this.FirstName.GetHashCode();
                if (this.LastName != null)
                    hashCode = hashCode * 59 + this.LastName.GetHashCode();
                if (this.PlaceOfBirth != null)
                    hashCode = hashCode * 59 + this.PlaceOfBirth.GetHashCode();
                if (this.DateOfBirth != null)
                    hashCode = hashCode * 59 + this.DateOfBirth.GetHashCode();
                if (this.Address != null)
                    hashCode = hashCode * 59 + this.Address.GetHashCode();
                if (this.PhoneNumber != null)
                    hashCode = hashCode * 59 + this.PhoneNumber.GetHashCode();
                if (this.Sex != null)
                    hashCode = hashCode * 59 + this.Sex.GetHashCode();
                if (this.Email != null)
                    hashCode = hashCode * 59 + this.Email.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
