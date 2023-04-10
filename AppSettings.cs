using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TRM_Api_Viewer
{
    internal class AppSettings
    {
        public static void EncryptAndSave(string key, string value)
        {
            // Check if the key already exists in AppSettings
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement keyConfig = config.AppSettings.Settings[key];

            // If the key is not found, add it to AppSettings
            if (keyConfig == null)
            {
                config.AppSettings.Settings.Add(key, "");
                config.Save(ConfigurationSaveMode.Modified);
            }

            // Check if the encryptedKey and encryptedIV settings already exist in AppSettings
            KeyValueConfigurationElement keyConfigEncryptedKey = config.AppSettings.Settings[key + "_key"];
            KeyValueConfigurationElement keyConfigEncryptedIV = config.AppSettings.Settings[key + "_iv"];

            // If either setting is not found, add it to AppSettings
            if (keyConfigEncryptedKey == null)
            {
                config.AppSettings.Settings.Add(key + "_key", "");
                config.Save(ConfigurationSaveMode.Modified);
            }

            if (keyConfigEncryptedIV == null)
            {
                config.AppSettings.Settings.Add(key + "_iv", "");
                config.Save(ConfigurationSaveMode.Modified);
            }

            // Create a new instance of the AesManaged class
            using (AesManaged aes = new AesManaged())
            {
                // Generate a new encryption key and initialization vector (IV)
                aes.GenerateKey();
                aes.GenerateIV();

                // Convert the plaintext value to a byte array
                byte[] plaintext = Encoding.UTF8.GetBytes(value);

                // Encrypt the plaintext using AES encryption
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] ciphertext = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);

                    // Convert the encryption key and IV to base64 strings for storage in app settings
                    string encryptedKey = Convert.ToBase64String(aes.Key);
                    string encryptedIV = Convert.ToBase64String(aes.IV);

                    // Save the encrypted value, encrypted key, and encrypted IV to app settings
                    config.AppSettings.Settings[key].Value = Convert.ToBase64String(ciphertext);
                    config.AppSettings.Settings[key + "_key"].Value = encryptedKey;
                    config.AppSettings.Settings[key + "_iv"].Value = encryptedIV;
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
        }

        public static string LoadAndDecrypt(string key)
        {
            // Load the encrypted value, encrypted key, and encrypted IV from app settings
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Check if the encryptedKey and encryptedIV settings already exist in AppSettings
            KeyValueConfigurationElement keyConfigEncryptedKey = config.AppSettings.Settings[key + "_key"];
            KeyValueConfigurationElement keyConfigEncryptedIV = config.AppSettings.Settings[key + "_iv"];

            // If either setting is not found return ""
            if (keyConfigEncryptedKey == null)
            {
                return "";
            }

            if (keyConfigEncryptedIV == null)
            {
                return "";
            }

            string encryptedValue = config.AppSettings.Settings[key].Value;
            string encryptedKey = config.AppSettings.Settings[key + "_key"].Value;
            string encryptedIV = config.AppSettings.Settings[key + "_iv"].Value;

            // Convert the encrypted key and IV from base64 strings to byte arrays
            byte[] keyBytes = Convert.FromBase64String(encryptedKey);
            byte[] ivBytes = Convert.FromBase64String(encryptedIV);

            // Convert the encrypted value from a base64 string to a byte array
            byte[] encryptedBytes = Convert.FromBase64String(encryptedValue);

            // Decrypt the encrypted value using AES decryption
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    // Convert the decrypted byte array to a string and return it
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
