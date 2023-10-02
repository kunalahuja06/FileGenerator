using FileGenerator.Core.Contracts;
using System.Net.Http;
using Octokit;
using System.Threading.Tasks;

namespace FileGenerator.Core.Services
{
    public class FolderService: IFolderService
    {
        private readonly GitHubClient _gitHubClient;
        private readonly Credentials _credentials;

        public FolderService(HttpClient httpClient)
        {
            _gitHubClient = new GitHubClient(new ProductHeaderValue("FileGenerator"));
            _credentials = new Credentials("test", "test");
            _gitHubClient.Credentials = _credentials;
        }
        public async Task<bool> GetPatternFiles(string pattern, string folder)
        {

            var contents = await _gitHubClient.Repository.Content.GetAllContents("kunalahuja06", "FileGenerator", $"FileGenerator/Code/{pattern}");
            if(contents!=null)
            {
                foreach (var content in contents)
                {
                    var file = await _gitHubClient.Repository.Content.GetRawContent("kunalahuja06", "FileGenerator", content.Path);
                    System.IO.File.WriteAllBytes($"{folder}\\{content.Name}", file);
                }
                return true;
            }
            return false;
        }
    }
}
