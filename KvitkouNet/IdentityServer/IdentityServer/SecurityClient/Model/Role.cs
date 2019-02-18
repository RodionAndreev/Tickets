using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace IdentityServer.SecurityClient.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Role {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets AccessRights
    /// </summary>
    [DataMember(Name="accessRights", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "accessRights")]
    public List<AccessRight> AccessRights { get; set; }

    /// <summary>
    /// Gets or Sets DeniedRights
    /// </summary>
    [DataMember(Name="deniedRights", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "deniedRights")]
    public List<AccessRight> DeniedRights { get; set; }

    /// <summary>
    /// Gets or Sets AccessFunctions
    /// </summary>
    [DataMember(Name="accessFunctions", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "accessFunctions")]
    public List<AccessFunction> AccessFunctions { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Role {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  AccessRights: ").Append(AccessRights).Append("\n");
      sb.Append("  DeniedRights: ").Append(DeniedRights).Append("\n");
      sb.Append("  AccessFunctions: ").Append(AccessFunctions).Append("\n");
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
