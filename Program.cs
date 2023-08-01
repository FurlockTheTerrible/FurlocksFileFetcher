string[] urlList = File.ReadAllLines(@"Z:\test.txt"); // Replace this with the target file. This should be a .txt with a list of links that are separated by line breaks.
string downloadDirectory = @"Z:\test\"; // Replace this with the path to your desired download folder. The folder must already exist.
await DownloadFilesAsync(urlList, downloadDirectory);

async Task DownloadFilesAsync(string[] urlList, string downloadDirectory)
{
    using (HttpClient client = new HttpClient())
    {
        foreach (string url in urlList)
        {
            string fileName = Path.GetFileName(url);
            string downloadPath = Path.Combine(downloadDirectory, fileName);

            try
            {
                byte[] fileData = await client.GetByteArrayAsync(url);

                using (FileStream fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
                {
                    await fileStream.WriteAsync(fileData, 0, fileData.Length);
                }

                Console.WriteLine($"Downloaded: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading {fileName}: {ex.Message}");
            }
        }
    }
}