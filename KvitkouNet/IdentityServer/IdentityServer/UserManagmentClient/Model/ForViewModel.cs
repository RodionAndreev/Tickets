using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace IdentityServer.UserManagmentClient.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ForViewModel {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or Sets Login
    /// </summary>
    [DataMember(Name="login", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "login")]
    public string Login { get; set; }

    /// <summary>
    /// Gets or Sets FirstName
    /// </summary>
    [DataMember(Name="firstName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstName")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or Sets LastName
    /// </summary>
    [DataMember(Name="lastName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastName")]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or Sets Sex
    /// </summary>
    [DataMember(Name="sex", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "sex")]
    public Sex Sex { get; set; }

    /// <summary>
    /// Gets or Sets Birthday
    /// </summary>
    [DataMember(Name="birthday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "birthday")]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// Gets or Sets RegistrationDate
    /// </summary>
    [DataMember(Name="registrationDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "registrationDate")]
    public DateTime? RegistrationDate { get; set; }

    /// <summary>
    /// Gets or Sets Rating
    /// </summary>
    [DataMember(Name="rating", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "rating")]
    public double? Rating { get; set; }

    /// <summary>
    /// Gets or Sets Email
    /// </summary>
    [DataMember(Name="email", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or Sets PhoneNumber
    /// </summary>
    [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or Sets EmailConfirmed
    /// </summary>
    [DataMember(Name="emailConfirmed", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "emailConfirmed")]
    public bool? EmailConfirmed { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ForViewModel {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Login: ").Append(Login).Append("\n");
      sb.Append("  FirstName: ").Append(FirstName).Append("\n");
      sb.Append("  LastName: ").Append(LastName).Append("\n");
      sb.Append("  Sex: ").Append(Sex).Append("\n");
      sb.Append("  Birthday: ").Append(Birthday).Append("\n");
      sb.Append("  RegistrationDate: ").Append(RegistrationDate).Append("\n");
      sb.Append("  Rating: ").Append(Rating).Append("\n");
      sb.Append("  Email: ").Append(Email).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  EmailConfirmed: ").Append(EmailConfirmed).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
