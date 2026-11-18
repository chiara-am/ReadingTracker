namespace Viola_Eliana_ReadingTracker
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        // Runs when the Search button is clicked or Enter is pressed
        private async void OnSearchClicked(object? sender, EventArgs e)
        {
            // TODO: call the search service
            await DisplayAlertAsync("Search", $"You searched for: {SearchEntry.Text}", "OK");
        }

        // Runs when a result is tapped in the list
        private void OnResultSelected(object? sender, SelectionChangedEventArgs e)
        {
            // TODO: open the detail page
        }

        // Runs when the Library button in the top bar is clicked
        private async void Library_Clicked(object? sender, EventArgs e)
        {
            await DisplayAlertAsync("Library", "Library page coming soon.", "OK");
        }

    }
}
