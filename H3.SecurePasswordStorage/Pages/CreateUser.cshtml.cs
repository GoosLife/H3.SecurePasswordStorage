using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using H3.SecurePasswordStorage.Models;
using MySql.Data.MySqlClient;
using System.Text;
using System.Security.Cryptography;
using H3.SecurePasswordStorage.DataAccess;

namespace H3.SecurePasswordStorage.Pages
{
	/// <summary>
	/// Represents the model for creating a new user.
	/// </summary>
	public class CreateUserModel : PageModel
	{
		[BindProperty]
		public new User User { get; set; } = new User();
		public string DebugOutput = "";

		private readonly IUserRepository _userRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateUserModel"/> class.
		/// </summary>
		/// <param name="userRepository">The user repository.</param>
		public CreateUserModel(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public void OnGet()
		{
		}

		public void OnPost()
		{
			_userRepository.CreateUser(User);

			DebugOutput = "User created: " + User.Username;
			DebugOutput += " with password: " + User.Password;
			DebugOutput += ".<br>This is to demonstrate in the GUI that the password is being hashed and salted. Try creating a new user with the same password and compare the outputs.";
		}
	}
}
