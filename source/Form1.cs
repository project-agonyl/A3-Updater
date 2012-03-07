using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;
using System.Security.Cryptography;
using System.Diagnostics;

namespace A3Updator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void a3LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forum.ragezone.com/f98/");
        }

        private void doDecompress(string sName)
        {
            ProcessStartInfo k = new ProcessStartInfo();
            k.FileName = "7z.exe";
            k.Arguments = "e " + sName + " -y";
            k.WindowStyle = ProcessWindowStyle.Hidden;
            Process z = Process.Start(k);
            z.WaitForExit();
        }

        private void decompress()
        {
            try
            {
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
                Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + @"\Update");
                string dFileName;
                string[] dFilePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.7z");
                for (int i = 0; i < dFilePaths.Length; i++)
                {
                    a3LaunchButton.Enabled = false;
                    a3UpdateButton.Enabled = false;
                    a3UpdateButton.Text = "Decompressing files...";
                    Application.DoEvents();
                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                        Directory.SetCurrentDirectory(mainPath);
                    Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + @"\Update");
                    dFileName = Path.GetFileName(dFilePaths[i]);
                    doDecompress(dFileName);
                    File.Delete(dFileName);
                }
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
            }
            catch (Exception dex)
            {
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
                MessageBox.Show(dex.Message, "Decompression Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void launchButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchGame = new System.Diagnostics.Process();
                launchGame.StartInfo.FileName = "A3Client.exe";
                launchGame.StartInfo.Arguments = "3216473353";
                launchGame.Start();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("A3Client.exe was not found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyFiles(string sourcePath, string destinationPath, string destinationFolder)
        {
            string[] fileArray = Directory.GetFiles(sourcePath);
            string fileName = null;
            for (int i = 0; i < fileArray.Length; i++)
            {
                fileName = Path.GetFileName(fileArray[i]);
                if (File.Exists(fileName))
                    File.Delete(fileName);
                File.Copy(fileArray[i], destinationPath + "\\" + destinationFolder + "\\" + fileName, true);
            }
        }

        private void deleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                deleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);

        }

        private byte[] downloadedData;
        String mainPath = Directory.GetCurrentDirectory();

        private void download(String basePath, String fileName)
        {
            if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                Directory.SetCurrentDirectory(mainPath);
            if (String.Equals("md5.csv", fileName))
                Application.DoEvents();
            else if (String.Equals("7z.exe", fileName))
                Application.DoEvents();
            else
                fileName = fileName + ".7z";
            a3ProgressBar.Value = 0;
            a3FileLabel.Text = fileName;
            a3LaunchButton.Enabled = false;
            a3UpdateButton.Enabled = false;
            a3UpdateButton.Text = "Downloading files...";
            Application.DoEvents();
            String url = basePath + fileName;
            downloadedData = new byte[0];
            string updateDirectoryString = Directory.GetCurrentDirectory() + @"\Update";
            if (!Directory.Exists(updateDirectoryString))
                Directory.CreateDirectory(updateDirectoryString);
            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();
                byte[] buffer = new byte[1024];
                int dataLength = (int)response.ContentLength;
                a3ProgressBar.Maximum = dataLength;
                a3SizeLabel.Text = "0/" + dataLength.ToString();
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        
                        a3ProgressBar.Value = a3ProgressBar.Maximum;
                        a3SizeLabel.Text = dataLength.ToString() + "/" + dataLength.ToString();
                        Application.DoEvents();
                        break;
                    }
                    else
                    {
                        
                        memStream.Write(buffer, 0, bytesRead);

                        
                        if (a3ProgressBar.Value + bytesRead <= a3ProgressBar.Maximum)
                        {
                            a3ProgressBar.Value += bytesRead;
                            a3SizeLabel.Text = a3ProgressBar.Value.ToString() + "/" + dataLength.ToString();

                            a3ProgressBar.Refresh();
                            Application.DoEvents();
                        }
                    }
                }
                downloadedData = memStream.ToArray();
                stream.Close();
                memStream.Close();
                if (downloadedData != null && downloadedData.Length != 0)
                {
                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                        Directory.SetCurrentDirectory(mainPath);
                    Directory.SetCurrentDirectory(Directory.GetCurrentDirectory()+@"\Update");
                    FileStream newFile = new FileStream(fileName, FileMode.Create);
                    newFile.Write(downloadedData, 0, downloadedData.Length);
                    newFile.Close();
                }
                else
                    MessageBox.Show("File could not be downloaded!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                a3LaunchButton.Enabled = true;
                a3UpdateButton.Text = "Update Game";
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                MessageBox.Show("ERROR: " + exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
            }
        }

        private void a3UpdateButton_Click(object sender, EventArgs e)
        {
            StreamReader pathFile;
            FileStream currentFile;
            try
            {
                pathFile = new StreamReader("path.ini");
                String basePath = pathFile.ReadLine();
                download(basePath, "md5.csv");
                download(basePath, "7z.exe");
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
                Directory.SetCurrentDirectory(Directory.GetCurrentDirectory()+@"\Update");
                StreamReader hashFile = new StreamReader("md5.csv");
                string line;
                int counter = 0;
                Hashtable hashData = new Hashtable();
                string[] fileNames = new string[10000];
                while ((line = hashFile.ReadLine()) != null)
                {
                    string[] temp = line.Split(',');
                    fileNames[counter] = temp[0];
                    hashData.Add(temp[0], temp[1]);                    
                    counter++;
                }
                hashFile.Dispose();
                hashFile.Close();
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
                string dataDirectoryString = Directory.GetCurrentDirectory() + @"\Data";
                if (Directory.Exists(dataDirectoryString))
                {
                    for (int i = 0; i < counter; i++)
                    {
                        try
                        {
                            if (String.Equals("A3client.exe", fileNames[i]))
                            {
                                try
                                {
                                    FileStream kcurrentFile;
                                    kcurrentFile = new FileStream(fileNames[i], FileMode.Open);
                                    MD5 kmd5 = new MD5CryptoServiceProvider();
                                    byte[] kretVal = kmd5.ComputeHash(kcurrentFile);
                                    kcurrentFile.Close();
                                    String khash = BitConverter.ToString(kretVal).Replace("-", string.Empty);
                                    khash = khash.ToLower();
                                    if (!String.Equals(khash, hashData[fileNames[i]]))
                                    {
                                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                                            Directory.SetCurrentDirectory(mainPath);
                                        download(basePath, fileNames[i]);
                                    }
                                }
                                catch (Exception)
                                {
                                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                                        Directory.SetCurrentDirectory(mainPath);
                                    download(basePath, fileNames[i]);
                                }
                            }
                            else if (String.Equals("setup.ini", fileNames[i]))
                            {
                                try
                                {
                                    FileStream k1currentFile;
                                    k1currentFile = new FileStream(fileNames[i], FileMode.Open);
                                    MD5 k1md5 = new MD5CryptoServiceProvider();
                                    byte[] k1retVal = k1md5.ComputeHash(k1currentFile);
                                    k1currentFile.Close();
                                    String k1hash = BitConverter.ToString(k1retVal).Replace("-", string.Empty);
                                    k1hash = k1hash.ToLower();
                                    if (!String.Equals(k1hash, hashData[fileNames[i]]))
                                    {
                                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                                            Directory.SetCurrentDirectory(mainPath);
                                        download(basePath, fileNames[i]);
                                    }
                                }
                                catch (Exception)
                                {
                                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                                        Directory.SetCurrentDirectory(mainPath);
                                    download(basePath, fileNames[i]);
                                }
                            }
                            else
                            {
                                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                                    Directory.SetCurrentDirectory(mainPath);
                                Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + @"\Data");
                                currentFile = new FileStream(fileNames[i], FileMode.Open);
                                MD5 md5 = new MD5CryptoServiceProvider();
                                byte[] retVal = md5.ComputeHash(currentFile);
                                currentFile.Close();
                                String hash = BitConverter.ToString(retVal).Replace("-", string.Empty);
                                hash = hash.ToLower();
                                if (!String.Equals(hash, hashData[fileNames[i]]))
                                {
                                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                                        Directory.SetCurrentDirectory(mainPath);
                                    download(basePath, fileNames[i]);
                                }
                                if(!String.Equals(mainPath,Directory.GetCurrentDirectory()))
                                    Directory.SetCurrentDirectory(mainPath);
                            }
                        }
                        catch (Exception)
                        {
                            download(basePath, fileNames[i]);
                        }
                    }
                }
                else
                {
                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                        Directory.SetCurrentDirectory(mainPath);
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Data");
                    for (int i = 0; i < counter; i++)
                    {
                        download(basePath, fileNames[i]);
                    }
                }
                Application.DoEvents();
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
                if(Directory.Exists(Directory.GetCurrentDirectory() + @"\Update"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\Update\md5.csv");
                    decompress();
                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                        Directory.SetCurrentDirectory(mainPath);
                    copyFiles(Directory.GetCurrentDirectory() + @"\Update", Directory.GetCurrentDirectory(), "Data");
                    deleteDirectory(Directory.GetCurrentDirectory() + @"\Update");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\A3Client.exe"))
                    {
                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                            Directory.SetCurrentDirectory(mainPath);
                        if (File.Exists("A3Client.exe"))
                            File.Delete("A3Client.exe");
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\A3Client.exe", "A3Client.exe");
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\D3dHook.dll"))
                    {
                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                            Directory.SetCurrentDirectory(mainPath);
                        if (File.Exists("D3dhook.dll"))
                            File.Delete("D3dhook.dll");
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\D3dHook.dll", "D3dHook.dll");
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\DSETUP.dll"))
                    {
                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                            Directory.SetCurrentDirectory(mainPath);
                        if (File.Exists("DSETUP.dll"))
                            File.Delete("DSETUP.dll");
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\DSETUP.dll", "DSETUP.dll");
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\en.ini"))
                    {
                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                            Directory.SetCurrentDirectory(mainPath);
                        if (File.Exists("en.ini"))
                            File.Delete("en.ini");
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\en.ini", "en.ini");
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\setup.ini"))
                    {
                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                            Directory.SetCurrentDirectory(mainPath);
                        if (File.Exists("setup.ini"))
                            File.Delete("setup.ini");
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\setup.ini", "setup.ini");
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\msvcp60.dll"))
                    {
                        if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                            Directory.SetCurrentDirectory(mainPath);
                        if (File.Exists("msvcp60.dll"))
                            File.Delete("msvcp60.dll");
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\msvcp60.dll", "msvcp60.dll");
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\7z.exe"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + @"\Data\7z.exe");
                    }
                }
                MessageBox.Show("Update Complete!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a3LaunchButton.Enabled = true;
                a3UpdateButton.Enabled = false;
                a3FileLabel.Text = "";
                a3SizeLabel.Text = "";
                a3UpdateButton.Text = "Client Up to date!";
                Application.DoEvents();
                pathFile.Close();
            }
            catch (Exception exc)
            {
                Application.DoEvents();
                if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                    Directory.SetCurrentDirectory(mainPath);
                if (Directory.Exists(Directory.GetCurrentDirectory() + @"\Update"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\Update\md5.csv");
                    decompress();
                    if (!String.Equals(mainPath, Directory.GetCurrentDirectory()))
                        Directory.SetCurrentDirectory(mainPath);
                    copyFiles(Directory.GetCurrentDirectory() + @"\Update", Directory.GetCurrentDirectory(), "Data");
                    deleteDirectory(Directory.GetCurrentDirectory() + @"\Update");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\A3client.exe"))
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\A3client.exe", "A3client.exe");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\D3dHook.dll"))
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\D3dHook.dll", "D3dHook.dll");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\DSETUP.dll"))
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\DSETUP.dll", "DSETUP.dll");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\en.ini"))
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\en.ini", "en.ini");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\setup.ini"))
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\setup.ini", "setup.ini");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\msvcp60.dll"))
                        File.Move(Directory.GetCurrentDirectory() + @"\Data\msvcp60.dll", "msvcp60.dll");
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\Data\7z.exe"))
                        File.Delete(Directory.GetCurrentDirectory() + @"\Data\7z.exe");
                }
                MessageBox.Show("ERROR: " + exc.Message, "Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                a3LaunchButton.Enabled = true;
                a3UpdateButton.Enabled = false;
                a3FileLabel.Text = "";
                a3SizeLabel.Text = "";
                a3UpdateButton.Text = "Client update interrupted!";
                Application.DoEvents();
            } 
         }
    }
}