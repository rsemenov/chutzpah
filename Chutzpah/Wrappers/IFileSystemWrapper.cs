﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chutzpah.Wrappers
{
    public interface IFileSystemWrapper
    {
        string GetTemporaryFolder(string leaf);
        void MoveFile(string sourceFilename, string destFilename);
        void CopyFile(string sourceFilename, string destFilename, bool overwrite=true);
        void MoveDirectory(string sourceDirectory, string destDirectory);
        void SetFileAttributes(string fileName, FileAttributes attributes);
        DateTime GetCreationTime(string path);
        DateTime GetLastAccessTime(string path);
        bool FileExists(string path);
        bool FolderExists(string path);
        void DeleteFile(string path);
        void DeleteDirectory(string path, bool recursive);
        string GetDirectoryName(string path);
        void CreateDirectory(string path);
        string GetFullPath(string path);
        IEnumerable<string> GetDirectories(string directory);
        string[] GetFiles(string path, string searchPattern, SearchOption searchOption);
        string GetFileName(string path);
        Stream Open(string path);
        Stream Open(string path, FileMode mode, FileAccess access);
        void Save(string path, Stream stream);
        byte[] GetContent(Stream stream);
        void Save(string path, string contents);
        string GetText(string path);
        string[] GetLines(string path);
        string GetRandomFileName();
        DateTime GetLastWriteTime(string path);
        void WriteAllText(string path, string text);
        void WriteAllText(string path, string text,Encoding encoding);
        bool IsPathRooted(string path);
        string CombinePath(params string[] paths);
        string GetFileNameWithoutExtension(string path);

    }
}