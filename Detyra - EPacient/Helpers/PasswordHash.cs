using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Detyra___EPacient.Helpers {
    public class PasswordHash {
        private const int SALT_SIZE = 16;
        private const int HASH_SIZE = 20;
        private const int HASH_ITER = 10000;

        private readonly byte[] salt, hash;

        public PasswordHash(string password) {
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SALT_SIZE]);
            hash = new Rfc2898DeriveBytes(password, salt, HASH_ITER).GetBytes(HASH_SIZE);
        }

        public string toString() {
            byte[] hashBytes = new byte[SALT_SIZE + HASH_SIZE];
            Array.Copy(salt, 0, hashBytes, 0, SALT_SIZE);
            Array.Copy(hash, 0, hashBytes, SALT_SIZE, HASH_SIZE);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        public bool verify(string savedPasswordHash, string enteredPassword) {
            // Extract the bytes
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Get the salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute the hash on the password the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Compare the results
            for (int i = 0; i < 20; i++) {
                if (hashBytes[i + 16] != hash[i]) {
                    return false;
                }
            }

            return true;
        }
    }
}
