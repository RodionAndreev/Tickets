namespace Security.Logic.Models.Responses
{
    public class AccessFunctionResponse : ActionResponse
    {
        /// <summary>
        /// Идентификатор функции
        /// </summary>
        public AccessFunction[] AccessFunctions { get; set; }

        public int TotalCount { get; set; }
    }
}
