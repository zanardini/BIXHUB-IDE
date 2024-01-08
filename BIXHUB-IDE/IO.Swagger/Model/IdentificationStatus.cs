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
    /// Defines IdentificationStatus
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
        public enum IdentificationStatus
    {
        /// <summary>
        /// Enum InProgress for value: InProgress
        /// </summary>
        [EnumMember(Value = "InProgress")]
        InProgress = 1,
        /// <summary>
        /// Enum Identified for value: Identified
        /// </summary>
        [EnumMember(Value = "Identified")]
        Identified = 2,
        /// <summary>
        /// Enum NotRecognized for value: NotRecognized
        /// </summary>
        [EnumMember(Value = "NotRecognized")]
        NotRecognized = 3    }
}