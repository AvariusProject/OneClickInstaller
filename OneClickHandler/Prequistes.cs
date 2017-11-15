using System;
using System.IO;

namespace Prequisite
{

    public class Prequisite
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        public void PreWorker()
        {
            string[] drives = System.Environment.GetLogicalDrives();
            foreach (string dr in drives)
            {
                System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

                // Here we skip the drive if it is not ready to be read. This
                // is not necessarily the appropriate action in all scenarios.
                if (!di.IsReady)
                {
                    Console.WriteLine("The drive {0} could not be read", di.Name);
                    continue;
                }
                System.IO.DirectoryInfo rootDir = di.RootDirectory;

                WalkDirectoryTree(rootDir, "camke.exe");
                WalkDirectoryTree(rootDir, "git.exe");

            }
        }

        public void WalkDirectoryTree(System.IO.DirectoryInfo root, string keyword)
        {


            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    try
                    {


                        if (fi.Name == keyword)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Datei gefunden.");
                            Console.ForegroundColor = ConsoleColor.White;
                            try
                            {
                                //PathVariable.PathVariable.addPathVariable(fi.FullName);
                                string oldentry = Environment.GetEnvironmentVariable("Path");
                                Console.WriteLine(oldentry.ToUpper());


                                if (oldentry.ToUpper().Contains("7Z.EXE"))
                                {
                                    Console.WriteLine("Found");

                                }
                                /*
                                string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine) + fi.FullName;
                                Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine); */

                            }

                            catch
                            {
                                Console.WriteLine("Cannot add new Entry");
                                
                            }

                            Console.WriteLine();
                            Console.WriteLine("Pathvariable is set successfull");
                            Console.WriteLine("Path " + fi.FullName);
                            Console.WriteLine("Restart your CMD to activate the Changes");
                            Console.WriteLine("Please press a Key to continue");
                            Console.ReadLine();
                           
                        }

                        else
                        {
                            //Console.WriteLine(fi.FullName);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    }

                    catch (PathTooLongException)
                    {

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Filename too long!");
                        Console.WriteLine(fi.Name);
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                }

               
                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, keyword);
                }


            }
        }

    }

}