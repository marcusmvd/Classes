using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace Tvirtual
{
    //DONT WORKING
    public class Folders
    {
        //Create folder
        public void CreateModify(string path)
        {
            List<string> logger = new List<string>();
            try
            {
                DirectoryInfo createFolder = new DirectoryInfo(path);
                createFolder.Create();
            }
            catch (IOException ex)
            {
                logger.Add(ex.ToString());
            }
            foreach (string log in logger)
            {
                //  MessageBox.Show(log);
            }

        }
        //Folder exists True or False
        public bool GetShow(string path)
        {
            bool Exists = false;
            if (Directory.Exists(path))
            {
                Exists = true;
            }
            return Exists;
        }
        //Get current path
        public string GetShow()
        {
            string currenPath = Environment.CurrentDirectory.ToString();
            return currenPath;
        }
        //return if IsEmpty or not
        public bool GetShow(string path, int nul)
        {
            bool IsEmpty = false;
            Dictionary<DirectoryInfo, List<string>> DirectoryTree = new Dictionary<DirectoryInfo, List<string>>();
            List<string> logger = new List<string>();
            try
            {
                string[] files = null;
                string[] subDirs = null;
                try
                {
                    files = Directory.EnumerateFiles(path).ToArray();
                    foreach (string f in files)
                    {
                        // MessageBox.Show(f);
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    logger.Add(e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    logger.Add(e.Message);
                }
                if (files != null)
                {
                    DirectoryTree.Add(new DirectoryInfo(path), files.ToList());
                    subDirs = Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories);
                    foreach (string dir in subDirs)
                    {
                        if (dir.ToString() != null)
                        {
                            // MessageBox.Show(dir);
                            IsEmpty = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Add(e.Message);
            }
            return IsEmpty;

        }
        //Delete folder empty or full
        public void Destroy(string path)
        {
            if (GetShow(path) != false)
            {
                List<string> logger = new List<string>();
                try
                {
                    DirectoryInfo DestroyFolder = new DirectoryInfo(path);
                    DestroyFolder.Delete(true);
                }
                catch (UnauthorizedAccessException ex)
                {
                    logger.Add(ex.ToString());
                }
                catch (DirectoryNotFoundException ex)
                {
                    logger.Add(ex.ToString());
                }
                catch (IOException ex)
                {
                    logger.Add(ex.ToString());
                }
                foreach (string log in logger)
                {
                    //
                }
            }
            else
            {
                MessageBox.Show("Pasta inexistente!");
            }

        }
    }
    public class Files
    {
        //Check if file exist
        public bool GetShow(string path)
        {
            bool exist = false;
            FileInfo files = new FileInfo(path);
            if (File.Exists(path))
            {
                exist = true;
            }
            return exist;
        }
        //Get FileSize
        public String GetShow(string path, string nul)
        {
            String measures = string.Empty;
            FileInfo files = new FileInfo(path);
            if (File.Exists(path))
            {
                string[] suf = { "B ", "KB ", "MB ", "GB ", "TB ", "PB ", "EB " }; //Longs run out around EB
                if (files.Length == 0)
                    return "0" + suf[0];
                long bytes = Math.Abs(files.Length);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                measures = (Math.Sign(files.Length) * num).ToString() + suf[place];
            }
            return measures;
        }
        public void Destroy(string path)
        {
            Files files = new Files();

            if (files.GetShow(path))
            {
                try
                {
                    FileInfo rename = new FileInfo(path);
                    File.Delete(rename.FullName);
                }
                catch
                {

                }




            }


        }
    }
    public class FolderOrFileGetShowInfo
    {
        #region Class Instance
        Folders folders = new Folders();
        Files files = new Files();
        #endregion
        //
        #region ReadOnly GetShow Attribute and Properties
        //Return Creation time
        public string CreationTime(string path)
        {
            string returns = string.Empty;
            if (folders.GetShow(path))
            {
                //Return folder creation time
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                returns = "Criado em: " + dirInfo.CreationTime.ToString();
            }
            if (files.GetShow(path))
            {
                //Return file creation time
                FileInfo fileinfo = new FileInfo(path);
                returns = "Criado em:" + fileinfo.CreationTime.ToString();
            }

            return returns;
        }
        //Return Creation time UTC (Coordinated Universal Time)
        public string CreationTimeUtc(string path)
        {
            string returns = string.Empty;

            if (folders.GetShow(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                returns = "Pasta (Formato (UTC)) Criado em: " + dirInfo.CreationTimeUtc.ToString();
            }
            if (files.GetShow(path))
            {
                //Return file creation time
                FileInfo fileinfo = new FileInfo(path);
                returns = "Arquivo (Formato (UTC)) Criado em: " + fileinfo.CreationTimeUtc.ToString();
            }
            return returns;
        }
        //Return LastAccess time
        public string LastAccessTime(string path)
        {


            string returns = string.Empty;
            if (folders.GetShow(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                returns = "Ultimo acesso em: " + dirInfo.LastAccessTime;
            }
            if (files.GetShow(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                returns = "Ultimo acesso em: " + fileInfo.LastAccessTime;
            }

            return returns;




        }
        //Return LastAccess time UTC (Coordinated Universal Time)
        public string LastAccessTimeUtc(string path)
        {
            string returns = string.Empty;
            if (folders.GetShow(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                returns = "(Formato (UTC)) Ultimo acesso em: " + dirInfo.LastAccessTime;
            }
            if (files.GetShow(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                returns = "(Formato (UTC)) Ultimo acesso em: " + fileInfo.LastAccessTime;
            }
            return returns;
        }
        //Return Last Write time
        public string LastWriteTime(string path)
        {
            string returns = string.Empty;
            if (folders.GetShow(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                return "Ultima Mofificação em: " + dirInfo.LastWriteTime;
            }
            if (files.GetShow(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                returns = "(Formato (UTC)) Ultimo acesso em: " + fileInfo.LastWriteTime;
            }
            return returns;
        }
        //Return Last Write time UTC (Coordinated Universal Time)
        public string LastWriteTimeUtc(string path)
        {

            string returns = string.Empty;
            if (folders.GetShow(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                returns = "(Formato (UTC)) Ultima Mofificação em: " + dirInfo.LastWriteTimeUtc;
            }
            if (files.GetShow(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                returns = "(Formato (UTC)) Ultima Mofificação em: " + fileInfo.LastWriteTimeUtc;
            }
            return returns;

        }
        //Return All Attributes file or folder
        public string[] Attribuiltes(string path)
        {
            //storage all atributes
            List<string> Bools = new List<string>();

            //check folder exist
            if (folders.GetShow(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Archive).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Archive).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Compressed).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Device).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Directory).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Encrypted).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Hidden).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.IntegrityStream).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Normal).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.NoScrubData).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.NotContentIndexed).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Offline).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.ReparsePoint).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.System).ToString());
                Bools.Add(dirInfo.Attributes.HasFlag(FileAttributes.Temporary).ToString());
            }
            if (files.GetShow(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Archive).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Archive).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Compressed).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Device).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Directory).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Encrypted).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Hidden).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.IntegrityStream).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Normal).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.NoScrubData).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.NotContentIndexed).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Offline).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.System).ToString());
                Bools.Add(fileInfo.Attributes.HasFlag(FileAttributes.Temporary).ToString());

            }
            return Bools.ToArray();


        }
        //Return specific attibute folder or file
        public bool Attribuiltes(string path, string attrib)
        {

            bool returns = false;

            if (folders.GetShow(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                switch (attrib.ToLower())
                {
                    case "archive":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Archive));
                        break;

                    case "readOnly":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly));
                        break;

                    case "compressed":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Compressed));
                        break;

                    case "device":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Device));
                        break;
                    case "directory":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Directory));
                        break;
                    case "encrypted":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Encrypted));
                        break;

                    case "hidden":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Hidden));
                        break;

                    case "integrityStream":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.IntegrityStream));
                        break;

                    case "normal":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Normal));
                        break;

                    case "noScrubDat":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.NoScrubData));
                        break;


                    case "notcontenindexed":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.NotContentIndexed));
                        break;

                    case "offline":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Offline));
                        break;

                    case "reparsepoint":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.ReparsePoint));
                        break;

                    case "system":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Normal));
                        break;

                    case "temporary":
                        returns = (dirInfo.Attributes.HasFlag(FileAttributes.Normal));
                        break;
                }

            }
            if (files.GetShow(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                switch (attrib)
                {
                    case "Archive":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Archive));
                        break;

                    case "ReadOnly":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly));
                        break;

                    case "Compressed":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Compressed));
                        break;

                    case "Device":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Device));
                        break;

                    case "Encrypted":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Encrypted));
                        break;

                    case "Hidden":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Hidden));
                        break;

                    case "IntegrityStream":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.IntegrityStream));
                        break;

                    case "Normal":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Normal));
                        break;

                    case "NoScrubDat":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.NoScrubData));
                        break;

                    case "NotContenIndexed":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.NotContentIndexed));
                        break;

                    case "Offline":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Offline));
                        break;

                    case "ReparsePoint":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint));
                        break;

                    case "System":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Normal));
                        break;

                    case "Temporary":
                        returns = (fileInfo.Attributes.HasFlag(FileAttributes.Normal));
                        break;
                }
            }
            return returns;


        }

        #endregion
        //
        #region ReadOnly GetShow Permissions
        //Get Owner
        public string GetShow(string path)
        {
            string Owner = string.Empty;
            //check if is an folder
            if (folders.GetShow(path))
            {
                DirectorySecurity directorySecurity = Directory.GetAccessControl(path);
                IdentityReference IdentityReference = directorySecurity.GetOwner(typeof(SecurityIdentifier));
                NTAccount nTAccount = IdentityReference.Translate(typeof(NTAccount)) as NTAccount;
                Owner = nTAccount.Value;
            }

            if (files.GetShow(path))
            {
                FileSecurity fileSecurity = File.GetAccessControl(path);
                IdentityReference IdentityReference = fileSecurity.GetOwner(typeof(SecurityIdentifier));
                NTAccount ntAccount = IdentityReference.Translate(typeof(NTAccount)) as NTAccount;
                Owner = ntAccount.Value;
            }

            return Owner;
        }
        //Returns all users with permissions at the folder or file
        public List<string> GetShow(string path, bool nul)
        {
            List<string> returnsAccounts = new List<string>();
            //check if is an folder
            if (folders.GetShow(path))
            {

                DirectoryInfo filePath = new DirectoryInfo(path);
                DirectorySecurity DirectorySecurity = filePath.GetAccessControl();
                AuthorizationRuleCollection acl = DirectorySecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (FileSystemAccessRule rules in acl)
                {
                    returnsAccounts.Add(rules.IdentityReference.Value);
                }
                return returnsAccounts;

            }

            if (files.GetShow(path))
            {
                FileInfo filePath = new FileInfo(path);
                FileSecurity DirectorySecurity = filePath.GetAccessControl();
                AuthorizationRuleCollection acl = DirectorySecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (FileSystemAccessRule rules in acl)
                {
                    returnsAccounts.Add(rules.IdentityReference.Value);
                }
                return returnsAccounts;
            }
            return returnsAccounts;
        }


        #endregion
        //
        #region Attribuiltes
        public bool AttribuiltesSet(string path, string attrib, bool tf)
        {
            bool returns = false;
            //check if is an folder
            if (folders.GetShow(path))
            {
                bool isHidden = (File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isArchive = (File.GetAttributes(path) & FileAttributes.Archive) == FileAttributes.Archive;
                bool isReadOnly = (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                bool isCompressed = (File.GetAttributes(path) & FileAttributes.Compressed) == FileAttributes.Compressed;
                bool isDevice = (File.GetAttributes(path) & FileAttributes.Device) == FileAttributes.Device;
                bool isEncrypted = (File.GetAttributes(path) & FileAttributes.Encrypted) == FileAttributes.Encrypted;
                bool isIntegrityStream = (File.GetAttributes(path) & FileAttributes.IntegrityStream) == FileAttributes.IntegrityStream;
                bool isNormal = (File.GetAttributes(path) & FileAttributes.Normal) == FileAttributes.Normal;
                bool isNoScrubDat = (File.GetAttributes(path) & FileAttributes.NoScrubData) == FileAttributes.NoScrubData;
                bool isNotContenIndexed = (File.GetAttributes(path) & FileAttributes.NotContentIndexed) == FileAttributes.NotContentIndexed;
                bool isOffline = (File.GetAttributes(path) & FileAttributes.Offline) == FileAttributes.Offline;
                bool isReparsePoint = (File.GetAttributes(path) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
                bool isSystem = (File.GetAttributes(path) & FileAttributes.System) == FileAttributes.System;
                bool isTemporary = (File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary;
                //Archive
                if (attrib.Contains("Archive"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Archive);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Archive);
                        returns = false;
                    }
                }
                //Hidden
                if (attrib.Contains("Hidden"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Hidden);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Hidden);
                        returns = false;
                    }
                }
                //ReadyOnly
                if (attrib.Contains("ReadOnly"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.ReadOnly);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
                        returns = false;
                    }
                }
                //Compressed             
                if (attrib.Contains("Compressed"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Compressed);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Compressed);
                        returns = false;
                    }
                }
                //Devince
                if (attrib.Contains("Device"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Device);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Device);
                        returns = false;
                    }
                }
                //Encrypted
                if (attrib.Contains("Encrypted"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Encrypted);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Encrypted);
                        returns = false;
                    }
                }
                //Integritestream
                if (attrib.Contains("IntegrityStream"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.IntegrityStream);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.IntegrityStream);
                        returns = false;
                    }
                }
                //Normal
                if (attrib.Contains("Normal"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Normal);
                        returns = false;
                    }
                }
                //NoScrubData
                if (attrib.Contains("NoScrubData"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.NoScrubData);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.NoScrubData);
                        returns = false;
                    }
                }
                //NotContenIndexed               
                if (attrib.Contains("NotContenIndexed"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.NotContentIndexed);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.NotContentIndexed);
                        returns = false;
                    }
                }
                //Offline
                if (attrib.Contains("Offline"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Offline);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Offline);
                        returns = false;
                    }
                }
                //ReparsePoint              
                if (attrib.Contains("ReparsePoint"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.ReparsePoint);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReparsePoint);
                        returns = false;
                    }
                }
                //System
                if (attrib.Contains("System"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.System);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.System);
                        returns = false;
                    }
                }
                //Temporary             
                if (attrib.Contains("Temporary"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Temporary);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Temporary);
                        returns = false;
                    }
                }
            }

            if (files.GetShow(path))
            {
                bool isHidden = (File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isArchive = (File.GetAttributes(path) & FileAttributes.Archive) == FileAttributes.Archive;
                bool isReadOnly = (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                bool isCompressed = (File.GetAttributes(path) & FileAttributes.Compressed) == FileAttributes.Compressed;
                bool isDevice = (File.GetAttributes(path) & FileAttributes.Device) == FileAttributes.Device;
                bool isEncrypted = (File.GetAttributes(path) & FileAttributes.Encrypted) == FileAttributes.Encrypted;
                bool isIntegrityStream = (File.GetAttributes(path) & FileAttributes.IntegrityStream) == FileAttributes.IntegrityStream;
                bool isNormal = (File.GetAttributes(path) & FileAttributes.Normal) == FileAttributes.Normal;
                bool isNoScrubDat = (File.GetAttributes(path) & FileAttributes.NoScrubData) == FileAttributes.NoScrubData;
                bool isNotContenIndexed = (File.GetAttributes(path) & FileAttributes.NotContentIndexed) == FileAttributes.NotContentIndexed;
                bool isOffline = (File.GetAttributes(path) & FileAttributes.Offline) == FileAttributes.Offline;
                bool isReparsePoint = (File.GetAttributes(path) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
                bool isSystem = (File.GetAttributes(path) & FileAttributes.System) == FileAttributes.System;
                bool isTemporary = (File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary;
                //Archive
                if (attrib.Contains("Archive"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Archive);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Archive);
                        returns = false;
                    }
                }
                //Hidden
                if (attrib.Contains("Hidden"))
                {

                    //if true = Hidden
                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Hidden);
                        returns = true;
                    }
                    //Hiden false
                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Hidden);
                        returns = false;
                    }
                }
                //ReadyOnly
                if (attrib.Contains("ReadOnly"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.ReadOnly);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
                        returns = false;
                    }
                }
                //Compressed             
                if (attrib.Contains("Compressed"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Compressed);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Compressed);
                        returns = false;
                    }
                }
                //Devince
                if (attrib.Contains("Device"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Device);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Device);
                        returns = false;
                    }
                }
                //Encrypted
                if (attrib.Contains("Encrypted"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Encrypted);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Encrypted);
                        returns = false;
                    }
                }
                //Integritestream
                if (attrib.Contains("IntegrityStream"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.IntegrityStream);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.IntegrityStream);
                        returns = false;
                    }
                }
                //Normal
                if (attrib.Contains("Normal"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Normal);
                        returns = false;
                    }
                }
                //NoScrubData
                if (attrib.Contains("NoScrubData"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.NoScrubData);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.NoScrubData);
                        returns = false;
                    }
                }
                //NotContenIndexed               
                if (attrib.Contains("NotContenIndexed"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.NotContentIndexed);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.NotContentIndexed);
                        returns = false;
                    }
                }
                //Offline
                if (attrib.Contains("Offline"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Offline);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Offline);
                        returns = false;
                    }
                }
                //ReparsePoint              
                if (attrib.Contains("ReparsePoint"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.ReparsePoint);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReparsePoint);
                        returns = false;
                    }
                }
                //System
                if (attrib.Contains("System"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.System);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.System);
                        returns = false;
                    }
                }
                //Temporary             
                if (attrib.Contains("Temporary"))
                {

                    if (tf)
                    {
                        File.SetAttributes(path, FileAttributes.Temporary);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Temporary);
                        returns = false;
                    }
                }
            }
            //returns how the arguments remained.
            return returns;
        }
        public bool AttribuiltesSet(string path, string attrib, bool ReadOHidden, bool nul)
        {
            bool returns = false;
            //check if is an folder
            if (folders.GetShow(path))
            {
                bool isHidden = (File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isArchive = (File.GetAttributes(path) & FileAttributes.Archive) == FileAttributes.Archive;
                bool isReadOnly = (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                bool isCompressed = (File.GetAttributes(path) & FileAttributes.Compressed) == FileAttributes.Compressed;
                bool isDevice = (File.GetAttributes(path) & FileAttributes.Device) == FileAttributes.Device;
                bool isEncrypted = (File.GetAttributes(path) & FileAttributes.Encrypted) == FileAttributes.Encrypted;
                bool isIntegrityStream = (File.GetAttributes(path) & FileAttributes.IntegrityStream) == FileAttributes.IntegrityStream;
                bool isNormal = (File.GetAttributes(path) & FileAttributes.Normal) == FileAttributes.Normal;
                bool isNoScrubDat = (File.GetAttributes(path) & FileAttributes.NoScrubData) == FileAttributes.NoScrubData;
                bool isNotContenIndexed = (File.GetAttributes(path) & FileAttributes.NotContentIndexed) == FileAttributes.NotContentIndexed;
                bool isOffline = (File.GetAttributes(path) & FileAttributes.Offline) == FileAttributes.Offline;
                bool isReparsePoint = (File.GetAttributes(path) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
                bool isSystem = (File.GetAttributes(path) & FileAttributes.System) == FileAttributes.System;
                bool isTemporary = (File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary;
                //Hidden //ReadyOnly
                if (attrib.Contains("readohidden"))
                {

                    if (ReadOHidden)
                    {
                        File.SetAttributes(path, FileAttributes.ReadOnly | FileAttributes.Hidden);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~(FileAttributes.Hidden | FileAttributes.ReadOnly));
                        returns = false;
                    }
                }
            }

            if (files.GetShow(path))
            {
                bool isHidden = (File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isArchive = (File.GetAttributes(path) & FileAttributes.Archive) == FileAttributes.Archive;
                bool isReadOnly = (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                bool isCompressed = (File.GetAttributes(path) & FileAttributes.Compressed) == FileAttributes.Compressed;
                bool isDevice = (File.GetAttributes(path) & FileAttributes.Device) == FileAttributes.Device;
                bool isEncrypted = (File.GetAttributes(path) & FileAttributes.Encrypted) == FileAttributes.Encrypted;
                bool isIntegrityStream = (File.GetAttributes(path) & FileAttributes.IntegrityStream) == FileAttributes.IntegrityStream;
                bool isNormal = (File.GetAttributes(path) & FileAttributes.Normal) == FileAttributes.Normal;
                bool isNoScrubDat = (File.GetAttributes(path) & FileAttributes.NoScrubData) == FileAttributes.NoScrubData;
                bool isNotContenIndexed = (File.GetAttributes(path) & FileAttributes.NotContentIndexed) == FileAttributes.NotContentIndexed;
                bool isOffline = (File.GetAttributes(path) & FileAttributes.Offline) == FileAttributes.Offline;
                bool isReparsePoint = (File.GetAttributes(path) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
                bool isSystem = (File.GetAttributes(path) & FileAttributes.System) == FileAttributes.System;
                bool isTemporary = (File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary;

                //Hidden //ReadyOnly
                if (attrib.Contains("readohidden"))
                {

                    if (ReadOHidden)
                    {
                        File.SetAttributes(path, FileAttributes.ReadOnly | FileAttributes.Hidden);
                        returns = true;
                    }

                    else
                    {
                        File.SetAttributes(path, File.GetAttributes(path) & ~(FileAttributes.Hidden | FileAttributes.ReadOnly));
                        returns = false;
                    }
                }
            }









            //returns how the arguments remained.
            return returns;
        }
        #endregion
        //
        #region  Modifiers Structure and Permissions
        //Add account in acl list
        public void Add(string path, string account, FileSystemRights accessLevel)
        {
            if (folders.GetShow(path))
            {
                //Disable inherited and remove
                DirectorySecurity fSecurity = Directory.GetAccessControl(path);
              //  fSecurity.SetAccessRuleProtection(true, false);
                //remove, all rights not inherited.
                fSecurity.AddAccessRule(new FileSystemAccessRule(account, accessLevel, AccessControlType.Allow));
                Directory.SetAccessControl(path, fSecurity);
            }
            if (files.GetShow(path))
            {
                //Disable inherited and remove
                FileSecurity fSecurity = File.GetAccessControl(path);
              //  fSecurity.SetAccessRuleProtection(true, false);
                //remove, all rights not inherited.
                fSecurity.AddAccessRule(new FileSystemAccessRule(account, accessLevel, AccessControlType.Allow));
                File.SetAccessControl(path, fSecurity);
            }

        }
        //rename specific file or folder.
        public void Rename(string path, string newName, bool overWrite)
        {
            //-- bool returns = false;
            //check if is an folder
            if (folders.GetShow(path))
            {
                try
                {

                    if (overWrite)
                    {

                        DirectoryInfo parent = Directory.GetParent(path);
                        DirectoryInfo rename = new DirectoryInfo(path);
                        Directory.Move(rename.FullName, Path.Combine(parent.FullName, newName));
                    }

                }
                catch
                {
                    MessageBox.Show("Falha ao renomear Pasta!", "AVISO:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

            if (files.GetShow(path))
            {
                try
                {

                    if (overWrite)
                    {

                        DirectoryInfo parent = Directory.GetParent(path);
                        FileInfo rename = new FileInfo(path);
                        File.Move(rename.FullName, Path.Combine(parent.FullName, newName));

                    }

                }
                catch
                {
                    MessageBox.Show("Falha ao renomear Arquivo!", "AVISO:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
        }
        //If already exist a folder using the new name, than rename 
        //to a new name using dateTime.NameToFile and following, rename to new original name
        public void Rename(string path, string newName, bool overWrite, int nul)
        {

            //check if is an folder
            if (folders.GetShow(path))
            {
                DirectoryInfo parent = Directory.GetParent(path);
                DirectoryInfo rename = new DirectoryInfo(path);
                try
                {

                    if (folders.GetShow(Path.Combine(parent.FullName, newName)))
                    {
                        Directory.Move(Path.Combine(parent.FullName, newName), Path.Combine(parent.FullName, rename.Name + "--" + DateTime.Now.ToFileTime().ToString()));
                        // MessageBox.Show(Path.Combine(parent.FullName, newName),Path.Combine(parent.FullName, rename.Name, DateTime.Now.ToFileTime().ToString()));
                    }
                    Directory.Move(rename.FullName, Path.Combine(parent.FullName, newName));
                }
                catch
                {
                    MessageBox.Show("Falha ao renomear arquivo!", "AVISO:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

            if (files.GetShow(path))
            {
                DirectoryInfo parent = Directory.GetParent(path);
                DirectoryInfo rename = new DirectoryInfo(path);
                try
                {

                    if (files.GetShow(Path.Combine(parent.FullName, newName)))
                    {
                        File.Move(Path.Combine(parent.FullName, newName), Path.Combine(parent.FullName, rename.Name + "--" + DateTime.Now.ToFileTime().ToString()));
                        // MessageBox.Show(Path.Combine(parent.FullName, newName),Path.Combine(parent.FullName, rename.Name, DateTime.Now.ToFileTime().ToString()));
                    }

                    File.Move(rename.FullName, Path.Combine(parent.FullName, newName));
                }
                catch
                {
                    MessageBox.Show("Falha ao renomear arquivo!", "AVISO:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

        }
        //remove, all rights not inherited or specific account user.
        public void Remove(string path, string account)
        {
            
           if (folders.GetShow(path))
            {
                //Code below is iqual for a file.
                FileSecurity fSecurity = File.GetAccessControl(path);
                fSecurity.RemoveAccessRule(new FileSystemAccessRule(account, FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl(path, fSecurity);

            }
      

            if (files.GetShow(path))
            {
                FileSecurity fSecurity = File.GetAccessControl(path);
                fSecurity.RemoveAccessRule(new FileSystemAccessRule(account, FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl(path, fSecurity);
            }





        }
        //remove, all rights inherited or not, of all users accounts.
        public void Remove(string path, string account , bool nul)
        {


            if (folders.GetShow(path))
            {
                //Disable inherited and remove
                DirectorySecurity fSecurity = Directory.GetAccessControl(path);
                fSecurity.SetAccessRuleProtection(true, false);
                //remove, all rights not inherited.
                fSecurity.RemoveAccessRule(new FileSystemAccessRule(account, FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(path, fSecurity);
            }
            if (files.GetShow(path))
            {
                //Disable inherited and remove
                FileSecurity fSecurity = File.GetAccessControl(path);
                fSecurity.SetAccessRuleProtection(true, false);
                //remove, all rights not inherited.
                fSecurity.RemoveAccessRule(new FileSystemAccessRule(account, FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl(path, fSecurity);
            }




           
        }
        //Disable or enable inherited and keep or not the accounts.
        public void EnableDisable(string path, bool disable, bool keepAcounts)
        {

            if (folders.GetShow(path))
            {

                //Disable inherited and remove
                DirectorySecurity fSecurity = Directory.GetAccessControl(path);
                fSecurity.SetAccessRuleProtection(disable, keepAcounts);
                Directory.SetAccessControl(path, fSecurity);
            }
            if (files.GetShow(path))
            {

                //Disable inherited and remove
                FileSecurity fSecurity = File.GetAccessControl(path);
                fSecurity.SetAccessRuleProtection(disable, keepAcounts);
                File.SetAccessControl(path, fSecurity);
            }



        }
        #endregion
    }
}
