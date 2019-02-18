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
  public class UserRights {
    /// <summary>
    /// Gets or Sets UserId
    /// </summary>
    [DataMember(Name="userId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "userId")]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or Sets UserLogin
    /// </summary>
    [DataMember(Name="userLogin", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "userLogin")]
    public string UserLogin { get; set; }

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
    /// Gets or Sets Roles
    /// </summary>
    [DataMember(Name="roles", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "roles")]
    public List<Role> Roles { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UserRights {\n");
      sb.Append("  UserId: ").Append(UserId).Append("\n");
      sb.Append("  UserLogin: ").Append(UserLogin).Append("\n");
      sb.Append("  AccessRights: ").Append(AccessRights).Append("\n");
      sb.Append("  DeniedRights: ").Append(DeniedRights).Append("\n");
      sb.Append("  AccessFunctions: ").Append(AccessFunctions).Append("\n");
      sb.Append("  Roles: ").Append(Roles).Append("\n");
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
