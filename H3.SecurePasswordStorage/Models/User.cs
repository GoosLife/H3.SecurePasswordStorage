namespace H3.SecurePasswordStorage.Models
{
	/// <summary>
	/// Represents a user.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Gets or sets the user ID.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }
	}
}
