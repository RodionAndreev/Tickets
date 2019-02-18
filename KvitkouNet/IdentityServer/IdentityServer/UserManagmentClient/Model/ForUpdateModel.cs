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
  public class ForUpdateModel {
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
    /// Gets or Sets Adress
    /// </summary>
    [DataMember(Name="adress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "adress")]
    public List<string> Adress { get; set; }

    /// <summary>
    /// Gets or Sets Phones
    /// </summary>
    [DataMember(Name="phones", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phones")]
    public List<string> Phones { get; set; }

    /// <summary>
    /// Gets or Sets Sex
    /// </summary>
    [DataMember(Name="sex", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "sex")]
    public string Sex { get; set; }

    /// <summary>
    /// Gets or Sets Birthday
    /// </summary>
    [DataMember(Name="birthday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "birthday")]
    public DateTime? Birthday { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ForUpdateModel {\n");
      sb.Append("  FirstName: ").Append(FirstName).Append("\n");
      sb.Append("  LastName: ").Append(LastName).Append("\n");
      sb.Append("  Adress: ").Append(Adress).Append("\n");
      sb.Append("  Phones: ").Append(Phones).Append("\n");
      sb.Append("  Sex: ").Append(Sex).Append("\n");
      sb.Append("  Birthday: ").Append(Birthday).Append("\n");
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
