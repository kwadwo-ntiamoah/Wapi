using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src.Utilities
{
    public class EncryptionHelper(ILogger<EncryptionHelper> logger)
    {
        private readonly ILogger<EncryptionHelper> _logger = logger;
        const int TAG_LENGTH = 16;

        public (DecryptedFlowMessage decryptedBody, byte[] aesKeyBytes, byte[] initialVectorBytes) DecryptRequest(EncryptedFlowMessage payload, string privatePemPath, string passphrase)
        {
            using var rsa = RSA.Create();
            // Load the private key from PEM
            string pemContent = File.ReadAllText(privatePemPath);
            var pemReader = new PemReader(new StringReader(pemContent), new PasswordFinder(passphrase));
            var pemObject = pemReader.ReadObject();

            if (pemObject is AsymmetricCipherKeyPair keyPair)
            {
                if (keyPair.Private is not RsaPrivateCrtKeyParameters privateKey)
                    throw new CryptographicException("The provided PEM does not contain a valid RSA private key.");

                var rsaParams = DotNetUtilities.ToRSAParameters(privateKey);
                rsa.ImportParameters(rsaParams);
            }
            else if (pemObject is RsaPrivateCrtKeyParameters privateKey)
            {
                var rsaParams = DotNetUtilities.ToRSAParameters(privateKey);
                rsa.ImportParameters(rsaParams);
            }
            else
            {
                throw new CryptographicException("The provided PEM does not contain a valid RSA private key.");
            }

            // Decrypt the AES key created by the client
            byte[] encryptedAesKeyBytes = Convert.FromBase64String(payload.EncryptedAesKey);
            byte[] aesKeyBytes = rsa.Decrypt(encryptedAesKeyBytes, RSAEncryptionPadding.OaepSHA256);

            // Decrypt the Flow data
            byte[] initialVectorBytes = Convert.FromBase64String(payload.InitialVector);
            byte[] flowDataBytes = Convert.FromBase64String(payload.EncryptedFlowData);
            byte[] plainTextBytes = new byte[flowDataBytes.Length - TAG_LENGTH];

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(aesKeyBytes), TAG_LENGTH * 8, initialVectorBytes);
            cipher.Init(false, parameters);
            var offset = cipher.ProcessBytes(flowDataBytes, 0, flowDataBytes.Length, plainTextBytes, 0);
            cipher.DoFinal(plainTextBytes, offset);

            string decryptedJsonString = Encoding.UTF8.GetString(plainTextBytes);

            _logger.LogInformation("================== Received Data =====================");
            _logger.LogInformation("Data:{decryptedJsonString}", decryptedJsonString);
            _logger.LogInformation("================== Received Data =====================");

            var decryptedBody = JsonConvert.DeserializeObject<DecryptedFlowMessage>(decryptedJsonString);
            return (decryptedBody, aesKeyBytes, initialVectorBytes);
        }

        public string EncryptResponse(dynamic response, byte[] aesKeyBytes, byte[] initialVectorBytes)
        {
            // Flip the initialization vector
            byte[] flippedIV = initialVectorBytes.Select(b => (byte)~b).ToArray();

            // Encrypt the response data
            string jsonResponse = JsonConvert.SerializeObject(response);
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(jsonResponse);

            var cipher = new GcmBlockCipher(new AesEngine());
            var cipherParameters = new AeadParameters(new KeyParameter(aesKeyBytes), TAG_LENGTH * 8, flippedIV);

            // Encrypt the data
            cipher.Init(true, cipherParameters);
            byte[] encryptedDataBytes = new byte[cipher.GetOutputSize(dataToEncrypt.Length)];
            var offset = cipher.ProcessBytes(dataToEncrypt, 0, dataToEncrypt.Length, encryptedDataBytes, 0);
            cipher.DoFinal(encryptedDataBytes, offset);

            // Get the authentication tag
            byte[] authTag = new byte[TAG_LENGTH];
            Array.Copy(encryptedDataBytes, encryptedDataBytes.Length - TAG_LENGTH, authTag, 0, TAG_LENGTH);

            // Concatenate encrypted data and auth tag, then return as base64
            byte[] encryptedResponse = new byte[encryptedDataBytes.Length - TAG_LENGTH + TAG_LENGTH];
            Array.Copy(encryptedDataBytes, 0, encryptedResponse, 0, encryptedDataBytes.Length - TAG_LENGTH);
            Array.Copy(authTag, 0, encryptedResponse, encryptedDataBytes.Length - TAG_LENGTH, TAG_LENGTH);

            _logger.LogInformation("Encryption Completed");

            return Convert.ToBase64String(encryptedResponse);
        }
    }

    public class PasswordFinder : IPasswordFinder
    {
        private readonly char[] _password;

        public PasswordFinder(string password)
        {
            _password = password.ToCharArray();
        }

        public char[] GetPassword()
        {
            return _password;
        }
    }

}
