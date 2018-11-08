using System;
using System.Collections.Generic;
using System.IO;

namespace FtpClient
{
    public class FtpDirectory
    {
        FtpDirectory OwnerDir;
        public List<FtpDirectory> Subdirectories = new List<FtpDirectory>();
        public List<FtpFile> Files = new List<FtpFile>();
        public string DirName;
        public bool IsRoot = false;

        public FtpDirectory()
        {
            IsRoot = true;
        }

        public FtpDirectory(FtpDirectory TopDirectory, string Name)
        {
            OwnerDir = TopDirectory ?? throw new Exception();
            DirName = Name;
        }

        public FtpDirectory AddSubdirectory(string SubName)
        {
            FtpDirectory NewDir = new FtpDirectory(this, SubName);
            Subdirectories.Add(NewDir);
            return NewDir;
        }

        public string PathString()
        {
            if (IsRoot) return "/";
            return OwnerDir.ToString() + DirName + "/";
        }
    }

    public class FtpFile
    {
        FtpDirectory OwnerDir;
        string FileName;

        public FtpFile(FtpDirectory Owner, string Name)
        {
            OwnerDir = Owner ?? throw new Exception();
            FileName = Name;
        }
    }
}
