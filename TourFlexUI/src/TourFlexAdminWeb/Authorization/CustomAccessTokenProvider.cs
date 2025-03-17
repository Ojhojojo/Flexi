//using Blazored.LocalStorage;
//using System.Text;
//using SavvyPayLibrary.Services.Authentication;

//namespace SavvyPayWeb.Authorization;
//public class CustomAccessTokenProvider(ILocalStorageService localStorage, EncryptionKeyService keyService)
//    : ICustomAccessTokenProvider
//{
//    private const string RefreshToken = "refresh_token";
//    private const string AccessToken = "access_token";

//    public async Task<string?> GetAccessTokenAsync()
//    {
//        return await localStorage.GetItemAsync<string>(AccessToken);
//        //return string.IsNullOrEmpty(encryptedToken) ? null : Decrypt(encryptedToken);
//    }

//    public async Task<string?> GetRefreshTokenAsync()
//    {
//        return await localStorage.GetItemAsync<string>(RefreshToken);
//        //return string.IsNullOrEmpty(encryptedToken) ? null : Decrypt(encryptedToken);
//    }

//    public async Task SetTokensAsync(string accessToken, string refreshToken)
//    {
//        keyService.GenerateKey(); // Fresh key per session
//        await localStorage.SetItemAsync(AccessToken, accessToken);
//        await localStorage.SetItemAsync(RefreshToken, refreshToken);
//    }

//    public Task RemoveTokensAsync()
//    {
//        localStorage.RemoveItemAsync(AccessToken);
//        localStorage.RemoveItemAsync(RefreshToken);
//        return Task.CompletedTask;
//    }

//    //private string Encrypt(string plainText)
//    //{
//    //    if (string.IsNullOrEmpty(plainText)) return string.Empty;

//    //    var plainBytes = Encoding.UTF8.GetBytes(plainText);
//    //    var keyBytes = keyService.Key;
//    //    var encryptedBytes = new byte[plainBytes.Length];

//    //    for (int i = 0; i < plainBytes.Length; i++)
//    //    {
//    //        encryptedBytes[i] = (byte)(plainBytes[i] ^ keyBytes[i % keyBytes.Length]);
//    //    }

//    //    return Convert.ToBase64String(encryptedBytes);
//    //}

//    //private string Decrypt(string encryptedText)
//    //{
//    //    if (string.IsNullOrEmpty(encryptedText)) return string.Empty;

//    //    var encryptedBytes = Convert.FromBase64String(encryptedText);
//    //    var keyBytes = keyService.Key;
//    //    var decryptedBytes = new byte[encryptedBytes.Length];

//    //    for (int i = 0; i < encryptedBytes.Length; i++)
//    //    {
//    //        decryptedBytes[i] = (byte)(encryptedBytes[i] ^ keyBytes[i % keyBytes.Length]);
//    //    }

//    //    return Encoding.UTF8.GetString(decryptedBytes);
//    //}
//}
