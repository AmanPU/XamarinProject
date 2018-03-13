using Plugin.Media;
using Xamarin.Forms;

namespace Media
{
    public partial class MediaPage : ContentPage
    {
        public MediaPage()
        {

            InitializeComponent();

            StackLayout stackLayout = new StackLayout();

            Image image = new Image();
            Label titleLabel = new Label
            {
                Text = "Xamarin Project",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Label welcomeLabel = new Label
            {
                Text = "Welcome",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

           

            Button googleButton = new Button
            {
                Text = "Open Google",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Button cameraButton = new Button
            {
                Text = "Open Camera",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };


            googleButton.Clicked += async (sender, args) =>
            {
                Device.OpenUri(new System.Uri("http://google.com"));
            };

            cameraButton.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");
                image.Source = ImageSource.FromStream(() =>
                 {
                    var stream = file.GetStream();
                    return stream;
                    }); 
               
            };

            stackLayout.Padding = 30;
            stackLayout.Spacing = 50;


            stackLayout.Children.Add(titleLabel);
            stackLayout.Children.Add(welcomeLabel);
            stackLayout.Children.Add(googleButton);
            stackLayout.Children.Add(cameraButton);
            stackLayout.Children.Add(image);
         

            Content = stackLayout;

        }


       


    }
}
