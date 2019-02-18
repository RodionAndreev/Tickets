namespace Security.Web.Models
{
    public class EditFunctionRequest
    {
        public int FunctionId { get; set; }
        public int[] RightIds { get; set; }
    }
}
