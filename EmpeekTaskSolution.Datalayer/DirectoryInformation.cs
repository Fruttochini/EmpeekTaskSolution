using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EmpeekTaskSolution.Datalayer
{
    /// <summary>
    /// Represents Information about current directory with access to subdirectories and files,
    /// provides information about filesizes
    /// </summary>
    public class DirectoryInformation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curDir">"Main" by default = access to logical drives</param>
        public DirectoryInformation(string curDir = "main")
        {
            CurrentDirectory = curDir;
        }

        private string _directory;

        public string CurrentDirectory
        {
            get { return _directory; }
            set { _directory = value; }
        }

        /// <summary>
        /// Array of strings with subdirectories names
        /// </summary>
        public string[] SubDirectories
        {
            get
            {
                string[] output = new string[2];
                try
                {
                    if (CurrentDirectory == "main")
                    {
                        output = Directory.GetLogicalDrives();
                    }
                    else
                    {
                        var _dirs = Directory.GetDirectories(CurrentDirectory);
                        output = new string[_dirs.Length + 1];
                        output[0] = "..";
                        for (int i = 0; i < _dirs.Length; i++)
                        {
                            DirectoryInfo di = new DirectoryInfo(_dirs[i]);
                            output[i + 1] = di.Name;
                        }
                    }
                }
                catch (Exception)
                {
                    output[1] = "Cannot access the directory";
                }
                return output;
            }

        }

        /// <summary>
        /// Array of strings with filenames
        /// </summary>
        public string[] SubFiles
        {
            get
            {
                string[] _files = new string[1];
                try
                {
                    _files = Directory.GetFiles(CurrentDirectory);
                    for (int i = 0; i < _files.Length; i++)
                    {
                        FileInfo fi = new FileInfo(_files[i]);
                        _files[i] = fi.Name;
                    }

                }
                catch (Exception)
                {
                    _files[0] = "Cannot access the directory to read files";
                }
                return _files;
            }
        }

        /// <summary>
        /// Gets array with counts of ranged filesizes
        /// </summary>
        public int[] CurrentDirectoryFS
        {
            get { return GetFileSizes(CurrentDirectory); }
        }

        /// <summary>
        /// Returns array with counts of ranged filesizes in subdirectories of current directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private int[] GetFileSizes(string directory)
        {
            int[] fileSizes = { 0, 0, 0 };

            if ((!String.IsNullOrWhiteSpace(directory)) && Directory.Exists(directory))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    FileInfo _fi = new FileInfo(file);

                    var size = _fi.Length;
                    if (size <= 10 * 1024 * 1024)
                    {
                        fileSizes[0]++;
                    }
                    else if ((10 * 1024 * 1024 < size) && (size <= 50 * 1024 * 1024))
                    {
                        fileSizes[1]++;
                    }
                    else fileSizes[2]++;

                }
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    try
                    {
                        var di = new DirectoryInfo(dir);

                        var tmpvalues = GetFileSizes(dir);
                        fileSizes[0] += tmpvalues[0];
                        fileSizes[1] += tmpvalues[1];
                        fileSizes[2] += tmpvalues[2];
                    }
                    catch { continue; }
                }
            }
            return fileSizes;
        }


        /// <summary>
        /// Move to parent directory (up)
        /// </summary>
        /// <param name="curPath"></param>
        public void MoveUp(string curPath)
        {
            DirectoryInfo di = new DirectoryInfo(curPath);
            var fn = di.FullName;
            var nm = di.Name;
            if (!fn.Equals(nm))
                CurrentDirectory = fn.Substring(0, fn.TrimEnd('\\').Length - nm.Length);
            else
                CurrentDirectory = "main";
        }

    }
}
