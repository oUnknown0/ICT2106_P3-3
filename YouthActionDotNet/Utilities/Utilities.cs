using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.StaticFiles;

public static class Utils{

    public static string hashpassword(string password){
        
        SHA256 sha256 = SHA256.Create();
        var secretPw = Convert.ToHexString(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        sha256.Dispose();
        return secretPw;
    }

    public static string GetFileExt(string fileName){
        var fileExtention = Path.GetExtension(fileName);
        return fileExtention;
    }

    public static string GetMime(string fileExt){
        var mime = new FileExtensionContentTypeProvider();
        mime.TryGetContentType(fileExt, out string contentType);
        return contentType;
    }

}