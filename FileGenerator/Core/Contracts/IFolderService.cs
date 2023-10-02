using System.Threading.Tasks;

namespace FileGenerator.Core.Contracts
{
    public interface IFolderService
    {
       Task<bool> GetPatternFiles(string pattern, string folder);
    }
}
