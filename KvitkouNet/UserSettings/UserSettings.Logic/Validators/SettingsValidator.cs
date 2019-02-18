using FluentValidation;
using UserSettings.Logic.Models;

namespace UserSettings.Logic.Validators
{
	public class SettingsValidator : AbstractValidator<Settings>
	{
		public SettingsValidator()
		{
			RuleFor(x => x.SettingsId).NotNull();
		}
	}
}
