using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace BookStore.Services
{
    public class FirebaseStorageService
    {
        private readonly string _bucketName ;
        private readonly StorageClient _storageClient;
        public FirebaseStorageService(){
            string credentialPath = "bookstore-59884-firebase-adminsdk-p59pi-12966435ab.json";
            if (!System.IO.File.Exists(credentialPath))
            {
                throw new InvalidOperationException($"Tệp thông tin xác thực không tồn tại tại: {credentialPath}");
            }
            GoogleCredential credential = GoogleCredential.FromFile(credentialPath);
            _storageClient = StorageClient.Create(credential);
            _bucketName = "bookstore-59884.appspot.com";
        }
        public async Task<string>UploadImageAsync(Stream fileStream, string fileName, string contentType)
        {
            try
            {
                var objectName  = $"images/{fileName}";
                var imageObject = await _storageClient.UploadObjectAsync(_bucketName,objectName,contentType,fileStream);

                var objMetadata = _storageClient.GetObject(_bucketName,objectName);
                string downloadToken = "";
                if(objMetadata.Metadata != null && objMetadata.Metadata.ContainsKey("firebaseStorageDownloadTokens")){
                    downloadToken = objMetadata.Metadata["firebaseStorageDownloadTokens"];
                }else{
                    downloadToken = Guid.NewGuid().ToString();

                    var updateMetadata = _storageClient.PatchObject(new Google.Apis.Storage.v1.Data.Object()
                    {
                        Bucket = _bucketName,
                        Name = objectName,
                        Metadata = new System.Collections.Generic.Dictionary<string, string>
                        {
                            { "firebaseStorageDownloadTokens", downloadToken }
                        }
                    });
                }
                
                var firebaseUrl = $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{Uri.EscapeDataString(objectName)}?alt=media&token={downloadToken}";
                
                return firebaseUrl;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Tải lên Firebase Storage thất bại: {ex.Message}");

            }
        }
    }
}