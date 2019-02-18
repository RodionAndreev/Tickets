using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace IdentityServer.SecurityClient.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class UserRightsResponse : ActionResponse {
    /// <summary>
    /// Gets or Sets UserRights
    /// </summary>
    [DataMember(Name="userRights", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "userRights")]
    public UserRights UserRights { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UserRightsResponse {\n");
      sb.Append("  UserRights: ").Append(UserRights).Append("\n");
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
