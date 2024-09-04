namespace EgeBilgiTaskCase.Application.Abstractions.Security
{
    public interface ICryptographyService
    {
        Task<string> EncryptString(string text);
        Task<string> DecryptString(string text);
        Task<byte[]> GenerateRandomIV();
        // Task<string> GenerateRandomKey(int keySizeInBits);
        
    }
}
