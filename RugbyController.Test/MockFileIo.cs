using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RugbyController.Test
{
    public class MockFileIo : RugbyModel.IFileIo
    {
        public List<string> _readFileLines;
        public List<string> _writtenFileLines;

        public string[] ReadAllLines(string path, Encoding encoding)
        {
            if (_readFileLines == null)
                return null;
            return _readFileLines.ToArray();
        }

        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            _writtenFileLines = contents.ToList();
        }
    }
}
