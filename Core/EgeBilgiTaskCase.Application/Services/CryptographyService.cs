using EgeBilgiTaskCase.Application.Abstractions.Security;

namespace EgeBilgiTaskCase.Application.Services
{
    public class CryptographyService : ICryptographyService
    {
        string encryptionKey = GenerateRandomKey(256).Result;

        public async Task<string> DecryptString(string text)
        {
            byte[] cipherBytes = Convert.FromBase64String(text);
            string EncryptionKey = await GenerateRandomKey(256);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(encryptionKey);
                aesAlg.IV = cipherBytes.Take(16).ToArray();

                aesAlg.Padding = PaddingMode.PKCS7; // Set the padding mode to PKCS7

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes, 16, cipherBytes.Length - 16))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public async Task<string> EncryptString(string text)
        {
            string EncryptionKey = await GenerateRandomKey(256);
            byte[] iv = await GenerateRandomIV();

            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(encryptionKey);
                aesAlgorithm.IV = iv;

                aesAlgorithm.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                    }
                    byte[] encryptedBytes = msEncrypt.ToArray();
                    byte[] resultBytes = new byte[iv.Length + encryptedBytes.Length];
                    Array.Copy(iv, 0, resultBytes, 0, iv.Length);
                    Array.Copy(encryptedBytes, 0, resultBytes, iv.Length, encryptedBytes.Length);
                    return Convert.ToBase64String(resultBytes);
                }
            }
        }

        public async Task<byte[]> GenerateRandomIV()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                return aesAlg.IV;
            }
        }

        public static Task<string> GenerateRandomKey(int keySizeInBits)
        {
            int keySizeInBytes = keySizeInBits / 8;

            byte[] keyBytes = new byte[keySizeInBytes];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }
            return Task.FromResult(Convert.ToBase64String(keyBytes));
        }
    }
}
