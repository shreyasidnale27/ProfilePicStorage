using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;

namespace XamarinStorage.Helper
{
   public class FirebaseHelper
    {
        FirebaseStorage firebaseStorage= new FirebaseStorage("xamfirebase-910d9.appspot.com");

       /* // Get a reference to the storage service, using the default Firebase App
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        Firebase.Storage.StorageReference storage_ref =
          storage.GetReferenceFromUrl("gs://imagesstorage-5338e.appspot.com");*/

       /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var imageUrl = await firebaseStorage
                .Child("Images")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child("Images")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        public async Task DeleteFile(string fileName)
        {
            await firebaseStorage
                 .Child("Images")
                 .Child(fileName)
                 .DeleteAsync();
        }
    }
}
