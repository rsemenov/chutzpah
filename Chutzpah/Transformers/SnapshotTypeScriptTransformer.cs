using System;
using Chutzpah.Models;
using Chutzpah.Wrappers;

namespace Chutzpah.Transformers
{
    public class SnapshotTypeScriptTransformer : SummaryTransformer
    {
        private readonly IJsonSerializer jsonSerializer;

        public SnapshotTypeScriptTransformer(IFileSystemWrapper fileSystem) : base(fileSystem)
        {
            jsonSerializer = new JsonSerializer();
        }

        public override string Description
        {
            get
            {
                return "Genereates typescript file with test snapshots";
            }
        }

        public override string Name
        {
            get
            {
                return Constants.SnapshotTypeScriptTransform;
            }
        }

        public override void Transform(TestCaseSummary testFileSummary, string outFile)
        {
            if (testFileSummary == null)
            {
                ChutzpahTracer.TraceWarning("Tests summary is empy");
                return;
            }

            foreach(var fileSummary in testFileSummary.TestFileSummaries)
            {
                if (fileSummary.TestSnapshots != null && fileSummary.TestSnapshots.Count > 0)
                {
                    if (!string.IsNullOrEmpty(fileSummary.Path))
                    {
                        var snapshotPath = GenerateSnapshotFilePath(fileSummary.Path);

                        if (FileSystem.FileExists(snapshotPath))
                        {
                            ChutzpahTracer.TraceError("Snapshot file {0} already exist. Remove existing snapshot file and try again.", snapshotPath);
                            return;
                        }

                        var snapshotContent = GenerateSnapshotContent(fileSummary);

                        FileSystem.WriteAllText(snapshotPath, snapshotContent);
                    }
                }
            }
        }

        public string GenerateSnapshotFilePath(string testFilePath)
        {
            var nameWithoutExtension = FileSystem.GetFileNameWithoutExtension(testFilePath);
            var snapshotFileName = nameWithoutExtension + ".snap.ts";
            var path = FileSystem.GetDirectoryName(testFilePath);
            return FileSystem.CombinePath(path, snapshotFileName);
        }

        public string GenerateSnapshotContent(TestFileSummary fileSummary)
        {
            string json = "{ ";
            foreach(var snapshot in fileSummary.TestSnapshots)
            {
                json += $"{Environment.NewLine}'{snapshot.Key}': `{snapshot.Value}`,";
            }
            json = json.TrimEnd(',');
            json += $"{Environment.NewLine}}}";
            return $"export const snapshots = {json}";
        }

        public override string Transform(TestCaseSummary testcaseSummary)
        {
            throw new NotImplementedException();
        }
    }
}
