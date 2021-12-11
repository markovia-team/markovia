using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using FluentFTP;

public static class FTPManager {
    public static async Task GetFilesName(List<string> list) {
        using FtpClient ftp = new FtpClient("192.168.10.38", new System.Net.NetworkCredential { UserName = "markovia", Password = "12345" });
        FtpListItem[] listing = await ftp.GetListingAsync();
        foreach (FtpListItem ftpListItem in listing) {
            list.Add(ftpListItem.Name);
        }
    }

    public static async Task GetFile(string fileName) {
        using FtpClient ftp = new FtpClient("192.168.10.38", new System.Net.NetworkCredential {UserName = "markovia", Password = "12345"});
        FtpListItem[] listing = await ftp.GetListingAsync();
        foreach (FtpListItem ftpListItem in listing) {
            if (ftpListItem.Name.Equals(fileName))
                await ftp.DownloadFileAsync(Application.dataPath + "/Save/tempLoad.json", ftpListItem.Name);
        }
    }
    
    public static async Task PostFile(string filePath) {
        using FtpClient ftp = new FtpClient("192.168.10.38", new System.Net.NetworkCredential { UserName = "markovia", Password = "12345" });
        var fileName = filePath.Split('/')[filePath.Split('/').Length - 1];
        await ftp.UploadFileAsync(filePath, fileName);
    }
}