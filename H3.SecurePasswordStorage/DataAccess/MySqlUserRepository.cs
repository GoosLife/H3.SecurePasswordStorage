using H3.SecurePasswordStorage.Models;
using H3.SecurePasswordStorage.Security;
using MySql.Data.MySqlClient;

namespace H3.SecurePasswordStorage.DataAccess
{
	/// <summary>
	/// Represents a MySQL implementation of the user repository.
	/// </summary>
	public class MySqlUserRepository : IUserRepository
	{
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlUserRepository"/> class.
		/// </summary>
		/// <param name="configuration">The configuration object.</param>
		public MySqlUserRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="user">The user object.</param>
		public void CreateUser(User user)
		{
			string conString = _configuration.GetConnectionString("DefaultConnection");

			byte[] salt = Encrypter.GenerateSalt();
			user.Password = Encrypter.HashPassword(user.Password, salt);

			using (MySqlConnection con = new MySqlConnection(conString))
			{
				string query = "INSERT INTO users (username, password, salt) VALUES (@username, @password, @salt)";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@username", user.Username);
					cmd.Parameters.AddWithValue("@password", user.Password);
					cmd.Parameters.AddWithValue("@salt", Convert.ToBase64String(salt));

					con.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Validates the user's login credentials.
		/// </summary>
		/// <param name="user">The user object.</param>
		/// <returns><c>true</c> if the login is successful; otherwise, <c>false</c>.</returns>
		public bool Login(User user)
		{
			string conString = _configuration.GetConnectionString("DefaultConnection");

			using (MySqlConnection con = new MySqlConnection(conString))
			{
				string query = "SELECT password, salt FROM users WHERE username = @username";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@username", user.Username);

					con.Open();
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							string storedPassword = reader.GetString(0);
							string storedSalt = reader.GetString(1);

							if (storedPassword == Encrypter.HashPassword(user.Password, Convert.FromBase64String(storedSalt)))
							{
								return true;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					}
				}
			}
		}
	}
}
