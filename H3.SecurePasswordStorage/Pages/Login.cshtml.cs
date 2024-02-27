using H3.SecurePasswordStorage.DataAccess;
using H3.SecurePasswordStorage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;

namespace H3.SecurePasswordStorage.Pages
{
	/// <summary>
	/// Represents the login page model.
	/// </summary>
	public class LoginModel : PageModel
	{
		private IUserRepository _userRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoginModel"/> class.
		/// </summary>
		/// <param name="userRepository">The user repository.</param>
		public LoginModel(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		[BindProperty]
		public new User User { get; set; } = new User();

		/// <summary>
		/// Gets or sets the debug output.
		/// </summary>
		public string DebugOutput { get; set; } = "";


		public void OnGet()
		{
		}

		public void OnPost()
		{
			if (_userRepository.Login(User))
			{
				DebugOutput = "Login successful. This means the supplied password successfully matched the stored hash. Nothing else will happen because this is a demo application.";
			}
			else
			{
				DebugOutput = "Login failed.";
			}
		}
	}
}
