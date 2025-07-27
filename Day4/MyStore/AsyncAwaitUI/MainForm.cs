namespace AsyncAwaitUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Awaitable  ->  Task, Task<>, ValueTask, ValueTask
        // Async -> void, Task, Task<>, ValueTask, ValueTask

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            //_ = StartDownloadAll();
            await StartDownloadAll();
            btnStart.Enabled = true;
        }

        async Task StartDownloadAll()
        {
            var urls = new string[]
            {
                    "https://example.com",
                    "https://example.org",
                    "https://example.net",
                    "https://example.edu",
            };

            foreach (var url in urls)
            {
                try
                {
                    var downloadBytesCount = await StartDownload(url);
                    listBoxLogs.Items.Add($"Download from {url}. Bytes: {downloadBytesCount}");
                }
                catch (Exception ex)
                {
                    listBoxLogs.Items.Add($"Error downloading from {url}: {ex.Message}");
                }
            }
        }

        private Task<int> StartDownload(string url)
        {
            var downloadTask = Task.Run<int>(() =>
            {
                Thread.Sleep(5000);
                return new Random().Next(1000, 5000); // Simulate download by returning a random byte count
            });

            return downloadTask;
        }
    }
}
