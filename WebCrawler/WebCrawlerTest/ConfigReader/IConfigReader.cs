using System.Collections.Generic;

namespace WebCrawlerTest.ConfigReader
{
    internal interface IConfigReader
    {
        void LoadFile(string fileName);

        int GetDepth();
        List<string> GetUrls();
    }
}