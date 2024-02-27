using H3.SecurePasswordStorage.Models;

namespace H3.SecurePasswordStorage.DataAccess
{
	/// <summary>
	/// Represents a repository for managing user data.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="user">The user object to create.</param>
		void CreateUser(User user);

		/// <summary>
		/// Validates the user's credentials for login.
		/// </summary>
		/// <param name="user">The user object containing the login credentials.</param>
		/// <returns>True if the login is successful, otherwise false.</returns>
		bool Login(User user);
	}
}
