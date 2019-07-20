using System;
using XamarinStorage.Helper;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;

namespace XamarinStorage
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// The fire storage
        /// </summary>
        FirebaseHelper fireStorage = new FirebaseHelper();

        /// <summary>
        /// The file is instance of media file
        /// </summary>
        MediaFile file;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        /// <summary>
        /// Handles the Clicked event of the BtnPick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the BtnUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
               await fireStorage.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
        }

        private async void BtnDownload_Clicked(object sender, EventArgs e)
        {
            try
            {
                string path = await fireStorage.GetFile(txtFileName.Text);
                if (path != null)
                {
                    lblPath.Text = path;
                    await DisplayAlert("Success", path, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                await fireStorage.DeleteFile(txtFileName.Text);
                lblPath.Text = string.Empty;
                await DisplayAlert("Success", "Deleted", "OK");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
