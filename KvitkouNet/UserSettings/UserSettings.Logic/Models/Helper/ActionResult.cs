namespace UserSettings.Logic.Models.Helper
{
	/// <summary>
	/// Описание результата
	/// </summary>
	public class MyActionResult
	{
		/// <summary>
		/// Результат.
		/// </summary>
		public ResultEnum UpdateResult { get; set; }

		/// <summary>
		/// Дополнительная информация.
		/// </summary>
		public string Message { get; set; }
	}
}
