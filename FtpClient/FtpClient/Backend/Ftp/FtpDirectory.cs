using System;
using System.Collections.Generic;

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
            //blokujemy duplikaty
            FtpDirectory Duplicate = GetSubdirectory(SubName);
            if (Duplicate != null) return Duplicate;

            FtpDirectory NewDir = new FtpDirectory(this, SubName);
            Subdirectories.Add(NewDir);
            return NewDir;
        }

        public FtpFile AddFile(string FileName)
        {
            //blokujemy duplikaty
            foreach (FtpFile File in Files)
                if (File.FileName.Equals(FileName)) return File;

            FtpFile NewFile = new FtpFile(this, FileName);
            Files.Add(NewFile);
            return NewFile;
        }

        public string PathString()
        {
            if (IsRoot) return "/";
            return OwnerDir.PathString() + DirName + "/";
        }

        public FtpDirectory GetSubdirectory(string SubName)
        {
            foreach (FtpDirectory Subdir in Subdirectories)
                if (Subdir.DirName.Equals(SubName)) return Subdir;
            return null;
        }

        internal FtpDirectory GetParentDir()
        {
            return OwnerDir;
        }
    }

    public class FtpFile
    {
        FtpDirectory OwnerDir;
        public string FileName;

        public FtpFile(FtpDirectory Owner, string Name)
        {
            OwnerDir = Owner ?? throw new Exception();
            FileName = Name;
        }

        internal FtpDirectory GetDirectory()
        {
            return OwnerDir;
        }
    }
}
