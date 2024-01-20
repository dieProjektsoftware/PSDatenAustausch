using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;

namespace PSDatenAustausch;
class FTP
{

    //private string _host;
    //private NetworkCredential _credentials;
    //private WebClient _wc;

    //public FTP(string host, string username, string password)
    //{
    //    _host = host;
    //    _credentials = new(username, password);
    //    _wc = new()
    //    {
    //        BaseAddress = _host,
    //        Credentials = _credentials
    //    }
    //}

    public string? _url { get; set; }
    
    public string? _user {get; set; }

    public string? _password
    {
        get ; 
        set ;
    }

    public string? _localDestinDir
    {
        get; set;
    }






    public bool FtpUpload(string TargetFileName, ref String InfoMsg )
    {
        try { 
            //string PureFileName = new FileInfo(TargetFileName).Name;      //webseiten/g-technologie/files
            //string uploadUrl = String.Format("{0}/{1}/{2}", "ftp://ftp.risikobeurteilung.at-web.biz", "kunden/492073_92637/webseiten/g-technologie/files", PureFileName);
            string uploadUrl = _url +"/"+ TargetFileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
            request.Method = WebRequestMethods.Ftp.UploadFile;


                // This example assumes the FTP site uses anonymous logon.  
                request.Credentials = new NetworkCredential(_user, _password);
                request.Proxy = null;
                request.KeepAlive = true;
                request.UseBinary = true;
                request.UsePassive = true;
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // Copy the contents of the file to the request stream.  
                //StreamReader sourceStream = new StreamReader(@"C:\temp\Test_24.pdf");
            byte[] fileContents = File.ReadAllBytes(_localDestinDir + "\\"+TargetFileName); //Encoding.ASCII.GetBytes(sourceStream.ReadToEnd());
               //sourceStream.Close();

            //Hier Verschlüsseln



            request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            InfoMsg = "Upload File Complete, status " + response.StatusDescription;
            return  true ;
        }
        catch (Exception e)
        {
            InfoMsg=e.Message;
            return false ;
        }
    }


    public string[] GetFileList()
    {
        string[] downloadFiles;
        StringBuilder result = new StringBuilder();
        WebResponse response = null;
        StreamReader reader = null;

        try
        {
         
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_url);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(_user, _password);
            request.KeepAlive = false;
            request.UsePassive = false;
            response = request.GetResponse();
            reader = new StreamReader(response.GetResponseStream());
            string line = reader.ReadLine();
            while (line != null)
            {
                result.Append(line);
                result.Append("\n");
                line = reader.ReadLine();
            }
            result.Remove(result.ToString().LastIndexOf('\n'), 1);
            return result.ToString().Split('\n');
        }
        catch (Exception ex)
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (response != null)
            {
                response.Close();
            }
            downloadFiles = null;
            return downloadFiles;
        }
    }

    public bool Download_(string file,ref string InfoErr)
    {
        try
        {
      

            //string FileName = "23-342635-ECS-DE-APU.pdf";
            //file = FileName;
            //string PureFileName = new FileInfo(FileName).Name;
            ////string url = String.Format("{0}/{1}/{2}", "ftp://ftp.risikobeurteilung.at-web.biz", "kunden/492073_92637/webseiten/g-technologie/files", FileName);
            //string url="ftp.risikobeurteilung.at-web.biz/"+FileName;
            ////url = "http://www.g-technologie.de/files/23-342635-ECS-DE-APU.pdf";
            

            string uri = _url + "/" + file;
            Uri serverUri = new Uri(uri);
            if (serverUri.Scheme != Uri.UriSchemeFtp)
            {
                return false;
            }

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("492073-PSfiles", "gkXbs9tcVs<u");
            request.KeepAlive = false;
            request.UsePassive = false;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            //localDestnDir = @"C:\Users\mg\OneDrive\Desktop\PS";
            FileStream writeStream = new FileStream(_localDestinDir + "\\" + file, FileMode.Create);
            int Length = 2048;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }
            writeStream.Close();
            response.Close();
            return true;
        }
        catch (WebException wEx)
        {
            InfoErr= wEx.Message + "\n"+ "Download Error";
            return false;
        }
        catch (Exception ex)
        {
            InfoErr = ex.Message + "\n" + "Download Error";
            return false;
        }
    }

    public bool DelFile(string fileName, ref string infoErr)
    {
        try
        {
            string uri = _url + "/" + fileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            //request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(_user, _password);
            //request.KeepAlive = false;
            //request.UsePassive = false;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                infoErr=response.StatusDescription;
            }
           
           
            return true;
        }
        catch (WebException wEx)
        {
            infoErr=(wEx.Message + "Del Error");
            return false;
        }
        catch (Exception ex)
        {
            infoErr = ex.Message + "Del Error";
            return false;
        }



        return true;
    }

    private Byte[] EncryptByteArray(Byte[] ByteQuellArray)
    {
        var collection = new X509Certificate2Collection();

        //collection.Import(
        //    encryptPublicKeyFilePath,
        //    null, 
        //    X509KeyStorageFlags.PersistKeySet);
        byte[] _salt= new byte[] { 13, 45, 23, 123, 78,90, 99,88 };

        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes("SackGesicht", _salt);

        

        var certificate = collection[0];

        var data = ByteQuellArray;  //  File.ReadAllBytes(sourceFilePath);

        var contentInfo = new ContentInfo(data);
        var envelopedCms = new EnvelopedCms(contentInfo);
        envelopedCms.Encrypt(new CmsRecipient(certificate));

        var encryptedData = envelopedCms.Encode();

        return encryptedData;
    
    }









}



















