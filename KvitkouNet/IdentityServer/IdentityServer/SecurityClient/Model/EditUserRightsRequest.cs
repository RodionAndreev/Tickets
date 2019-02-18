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
  public class EditUserRightsRequest {
    /// <summary>
    /// Gets or Sets UserId
    /// </summary>
    [DataMember(Name="userId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "userId")]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or Sets RoleIds
    /// </summary>
    [DataMember(Name="roleIds", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "roleIds")]
    public List<int?> RoleIds { get; set; }

    /// <summary>
    /// Gets or Sets FunctionIds
    /// </summary>
    [DataMember(Name="functionIds", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "functionIds")]
    public List<int?> FunctionIds { get; set; }

    /// <summary>
    /// Gets or Sets AccessedRightsIds
    /// </summary>
    [DataMember(Name="accessedRightsIds", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "accessedRightsIds")]
    public List<int?> AccessedRightsIds { get; set; }

    /// <summary>
    /// Gets or Sets DeniedRightsIds
    /// </summary>
    [DataMember(Name="deniedRightsIds", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "deniedRightsIds")]
    public List<int?> DeniedRightsIds { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class EditUserRightsRequest {\n");
      sb.Append("  UserId: ").Append(UserId).Append("\n");
      sb.Append("  RoleIds: ").Append(RoleIds).Append("\n");
      sb.Append("  FunctionIds: ").Append(FunctionIds).Append("\n");
      sb.Append("  AccessedRightsIds: ").Append(AccessedRightsIds).Append("\n");
      sb.Append("  DeniedRightsIds: ").Append(DeniedRightsIds).Append("\n");
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
