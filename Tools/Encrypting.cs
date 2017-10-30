using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace WalletSimulator.Tools
{
    public static class Encrypting
    {
        public static string EncryptText(string text, string publicKey)
        {
            text = GetMd5HashForString(text);
            text = EncryptString(text, publicKey);
            return text;
        }

        public static string GetMd5HashForString(string text)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            var hashValue = md5Hasher.ComputeHash(ConvertStringToByteArray(text));
            var hashData = BitConverter.ToString(hashValue);
            hashData = hashData.Replace("-", "");
            var result = hashData;
            return result;
        }

        private static string EncryptString(string inputString, string xmlString)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(1024);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int keySize = 128;
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                var tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
                Array.Reverse(encryptedBytes);
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        private static byte[] ConvertStringToByteArray(string data)
        {
            return new UnicodeEncoding().GetBytes(data);
        }

        //private static System.IO.FileStream GetFileStream(string pathName)
        //{
        //    return (new System.IO.FileStream(pathName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
        //}

        //public static string GetSHA1Hash(string pathName)
        //{
        //    string strResult = "";
        //    string strHashData = "";

        //    byte[] arrbytHashValue;
        //    System.IO.FileStream oFileStream = null;

        //    System.Security.Cryptography.SHA1CryptoServiceProvider oSHA1Hasher = new System.Security.Cryptography.SHA1CryptoServiceProvider();

        //    try
        //    {
        //        oFileStream = GetFileStream(pathName);
        //        arrbytHashValue = oSHA1Hasher.ComputeHash(oFileStream);
        //        oFileStream.Close();

        //        strHashData = System.BitConverter.ToString(arrbytHashValue);
        //        strHashData = strHashData.Replace("-", "");
        //        strResult = strHashData;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw;//System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
        //    }

        //    return (strResult);
        //}

        //public static string GetMD5Hash(string pathName)
        //{
        //    string strResult = "";
        //    string strHashData = "";

        //    byte[] arrbytHashValue;
        //    System.IO.FileStream oFileStream = null;

        //    System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();

        //    try
        //    {
        //        oFileStream = GetFileStream(pathName);
        //        arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
        //        oFileStream.Close();

        //        strHashData = System.BitConverter.ToString(arrbytHashValue);
        //        strHashData = strHashData.Replace("-", "");
        //        strResult = strHashData;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw;//System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
        //    }

        //    return (strResult);
        //}



        //public static string GetSHA1HashForString(string s)
        //{
        //    string strResult = "";
        //    string strHashData = "";

        //    byte[] arrbytHashValue;


        //    System.Security.Cryptography.SHA1CryptoServiceProvider oSHA1Hasher = new System.Security.Cryptography.SHA1CryptoServiceProvider();

        //    try
        //    {

        //        arrbytHashValue = oSHA1Hasher.ComputeHash(ConvertStringToByteArray(s));


        //        strHashData = System.BitConverter.ToString(arrbytHashValue);
        //        strHashData = strHashData.Replace("-", "");
        //        strResult = strHashData;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw;//System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
        //    }

        //    return (strResult);
        //}

        //public static string GetSign(string SourcePath, int prefix)
        //{
        //    string sign = "";
        //    string BasicPath = SourcePath + "//" + prefix.ToString();
        //    string OriginExt;
        //    string SCExt = "tif";
        //    string RecogExt;
        //    foreach (PictureFormat pf in Enum.GetValues(typeof(PictureFormat)))
        //    {
        //        StationManager.GetExtentionsByBatchId(pf, out OriginExt, out SCExt, out RecogExt);
        //        if (Directory.Exists(BasicPath + "_Originals"))
        //        {
        //            DirectoryInfo dirinfo = new DirectoryInfo(BasicPath + "_Originals");
        //            FileInfo[] files = dirinfo.GetFiles("*." + OriginExt, SearchOption.AllDirectories);
        //            for (int j = 0; j < files.Length; j++)
        //            {
        //                sign = sign + FileSigner.GetMD5Hash(files[j].FullName);
        //            }
        //        }
        //        if (pf == PictureFormat.tiff)
        //        {
        //            if (Directory.Exists(BasicPath + "_SC"))
        //            {
        //                DirectoryInfo dirinfo = new DirectoryInfo(BasicPath + "_SC");
        //                FileInfo[] files = dirinfo.GetFiles("*." + SCExt, SearchOption.AllDirectories);
        //                for (int j = 0; j < files.Length; j++)
        //                {
        //                    sign = sign + FileSigner.GetMD5Hash(files[j].FullName);
        //                }
        //            }
        //        }
        //        if (Directory.Exists(BasicPath))
        //        {
        //            DirectoryInfo dirinfo = new DirectoryInfo(BasicPath);
        //            FileInfo[] files = dirinfo.GetFiles("*." + RecogExt, SearchOption.AllDirectories);
        //            for (int j = 0; j < files.Length; j++)
        //            {
        //                sign = sign + FileSigner.GetMD5Hash(files[j].FullName);
        //            }
        //        }

        //    }
        //    sign = FileSigner.GetSHA1HashForString(sign);
        //    return sign;
        //}


        //public static string Encrypt(RSAParameters rsaParams, int keySize, string stringdata)
        //{
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);
        //    rsa.ImportParameters(rsaParams);

        //    int realKeySize = rsa.KeySize / 8;
        //    byte[] bytes = Encoding.UTF32.GetBytes(stringdata);
        //    int maxLength = realKeySize - 42;
        //    int dataLength = bytes.Length;
        //    int iterations = dataLength / maxLength;
        //    StringBuilder stringBuilder = new StringBuilder();
        //    for (int i = 0; i <= iterations; i++)
        //    {
        //        byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
        //        Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
        //        byte[] encryptedBytes = rsa.Encrypt(tempBytes, true);
        //        Array.Reverse(encryptedBytes);
        //        stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
        //    }
        //    return stringBuilder.ToString();
        //}

        //public static string Decrypt(RSAParameters rsaParams, int keySize, string stringdata)
        //{
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);
        //    rsa.ImportParameters(rsaParams);

        //    int base64BlockSize = ((rsa.KeySize / 8) % 3 != 0) ? (((rsa.KeySize / 8) / 3) * 4) + 4 : ((rsa.KeySize / 8) / 3) * 4;
        //    int iterations = stringdata.Length / base64BlockSize;
        //    ArrayList arrayList = new ArrayList();
        //    for (int i = 0; i < iterations; i++)
        //    {
        //        byte[] encryptedBytes = Convert.FromBase64String(stringdata.Substring(base64BlockSize * i, base64BlockSize));
        //        Array.Reverse(encryptedBytes);
        //        arrayList.AddRange(rsa.Decrypt(encryptedBytes, true));
        //    }
        //    return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        //}

        //#endregion

        //public Encrypting()
        //{
        //}


        public static string DecryptString(string inputString, string xmlString)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(1024);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int base64BlockSize = (128 % 3 != 0) ? ((128 / 3) * 4) + 4 : (128 / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }

        //public string EncryptArray(byte[] bytes, int dwKeySize, string xmlString)
        //{
        //    // TODO: Add Proper Exception Handlers
        //    RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
        //    rsaCryptoServiceProvider.FromXmlString(xmlString);
        //    int keySize = dwKeySize / 8;
        //    //byte[] bytes = Encoding.UTF32.GetBytes(inputString);
        //    // The hash function in use by the .NET RSACryptoServiceProvider here is SHA1
        //    // int maxLength = ( keySize ) - 2 - ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
        //    int maxLength = keySize - 42;
        //    int dataLength = bytes.Length;
        //    int iterations = dataLength / maxLength;
        //    StringBuilder stringBuilder = new StringBuilder();
        //    for (int i = 0; i <= iterations; i++)
        //    {
        //        byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
        //        Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
        //        byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
        //        // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
        //        // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
        //        // Comment out the next line and the corresponding one in the DecryptString function.
        //        Array.Reverse(encryptedBytes);
        //        // Why convert to base 64?
        //        // Because it is the largest power-of-two base printable using only ASCII characters
        //        stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
        //    }
        //    return stringBuilder.ToString();
        //}
        //public byte[] DecryptArray(string inputString, int dwKeySize, string xmlString)
        //{
        //    // TODO: Add Proper Exception Handlers
        //    RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
        //    rsaCryptoServiceProvider.FromXmlString(xmlString);
        //    int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
        //    int iterations = inputString.Length / base64BlockSize;
        //    ArrayList arrayList = new ArrayList();
        //    for (int i = 0; i < iterations; i++)
        //    {
        //        byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
        //        // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
        //        // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
        //        // Comment out the next line and the corresponding one in the EncryptString function.
        //        Array.Reverse(encryptedBytes);
        //        arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
        //    }
        //    return arrayList.ToArray(Type.GetType("System.Byte")) as byte[];
        //}

        //public static string GetMd5FileHash(byte[] input)
        //{
        //    MD5 md5Hash = MD5.Create();
        //    StringBuilder sBuilder = new StringBuilder();

        //    byte[] bs = new byte[] { 0x0066, 0x0049, 0x006B, 0x0048, 0x004B, 0x0052, 0x0036, 0x0037, 0x0072, 0x0075, 0x0066, 0x0052, 0x0066, 0x0046,
        //        0x0049, 0x0055, 0x0072, 0x0046, 0x0049, 0x0075, 0x0052, 0x0072, 0x0046, 0x0049, 0x0052, 0x0049, 0x0052 };

        //    byte[] arg = new byte[bs.Length + input.Length];
        //    Array.Copy(bs, 0, arg, 0, bs.Length);
        //    Array.Copy(input, 0, arg, bs.Length, input.Length);

        //    foreach (byte b in md5Hash.ComputeHash(arg))
        //        sBuilder.Append(b.ToString("x2"));

        //    return sBuilder.ToString();
        //}
        //public static bool VerifyMd5FileHash(byte[] input, string hash)
        //{
        //    StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        //    if (comparer.Compare(GetMd5FileHash(input), hash) == 0)
        //        return true;
        //    else
        //        return false;
        //}
        //public static bool VerifyMd5FileHash(string hash1, string hash2)
        //{
        //    StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        //    if (comparer.Compare(hash1, hash2) == 0)
        //        return true;
        //    return false;
        //}
    }
}
