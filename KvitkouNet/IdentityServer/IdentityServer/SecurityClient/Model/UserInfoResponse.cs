using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace IdentityServer.SecurityClient.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class UserInfoResponse : ActionResponse {
    /// <summary>
    /// Gets or Sets UsersInfo
    /// </summary>
    [DataMember(Name="usersInfo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "usersInfo")]
    public List<UserInfo> UsersInfo { get; set; }

    /// <summary>
    /// Gets or Sets TotalCount
    /// </summary>
    [DataMember(Name="totalCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "totalCount")]
    public int? TotalCount { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UserInfoResponse {\n");
      sb.Append("  UsersInfo: ").Append(UsersInfo).Append("\n");
      sb.Append("  TotalCount: ").Append(TotalCount).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public new string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
