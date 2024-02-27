using System.Security.Cryptography;

namespace H3.SecurePasswordStorage.Security
{
	public static class Encrypter
	{
		/// <summary>
		/// Hashes the provided password using the specified salt.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <param name="salt">The salt to use for hashing.</param>
		/// <returns>The hashed password as a Base64 encoded string.</returns>
		public static string HashPassword(string password, byte[] salt)
		{
			int iterations = 10000; // Magic number, do not use in real life
			int keySize = 32; // 256-bit key

			// Still using SHA1 despite its obsolescence, purely because it's the option that was specified in the assignment.
			using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA1))
			{
				byte[] key = rfc2898DeriveBytes.GetBytes(keySize);

				return Convert.ToBase64String(key);
			}
		}

		/// <summary>
		/// Generates a random salt.
		/// </summary>
		/// <returns>The generated salt as a byte array.</returns>
		public static byte[] GenerateSalt()
		{
			byte[] salt = new byte[16];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			return salt;
		}
	}
}
