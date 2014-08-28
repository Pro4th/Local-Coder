using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using LCData;
using LocalCoder;
using LocalCoder.CORE;

namespace LocalCoder
{
    public partial class LCForm : Form
    {
        public Engine Engine;

        public LCForm()
        {
            InitializeComponent();

            PrepareToSaveNewProject();
            PrepareToOpenProject();
            PrepareProjectView();

            Engine = new Engine(this);
            //Engine.Start();

            if (Environment.GetCommandLineArgs().Length > 1)
            {
                string openProjectLocation = Environment.GetCommandLineArgs()[1];
            }
        }

        private void webResultBox_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void LCForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Engine.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Engine.Update();
        }

        private void localCodeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void localCodeBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Return:
                    {
                        //int cursorPosition = localCodeBox.SelectionStart;
                        //int lineIndex = localCodeBox.GetLineFromCharIndex(cursorPosition);
                        //int tabs = 0;
                        //int tabsToAdd = tabs;
                        //string[] lines = localCodeBox.Lines;
                        //string currentTab = lines[lineIndex];
                        //while (currentTab.StartsWith("\t"))
                        //{
                        //    currentTab = currentTab.Remove(0, 1);
                        //    //MessageBox.Show(currentTab);
                        //    tabs++;
                        //    tabsToAdd++;
                        //}

                        //string newLineTab = "";
                        //while (tabsToAdd > 0)
                        //{
                        //    newLineTab = newLineTab + '\t';
                        //    tabsToAdd--;
                        //}

                        //List<string> _lines = lines.ToList();
                        //_lines.Insert(lineIndex+1, "");
                        //lines = _lines.ToArray();
                        //lines[lineIndex+1] = newLineTab;
                        //localCodeBox.Lines = lines;

                        //localCodeBox.Select(localCodeBox.GetFirstCharIndexFromLine(lineIndex + 1) + tabs, 0);

                        //e.SuppressKeyPress = true;
                        break;
                    }
            }
        }

        private void localCodeBox_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveNewProject.ShowDialog();
        }

        private void PrepareProjectView()
        {
            string imgs = Application.StartupPath + "\\res\\img\\";
            ImageList il = new ImageList();

            il.Images.Add(Image.FromFile(imgs + "01.png"));
            il.Images.Add(Image.FromFile(imgs + "02.png"));

            projectTree.ImageList = il;

            ///

            
        }

        private void PrepareToSaveNewProject()
        {
            saveNewProject.AddExtension = true;
            string doc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.DoNotVerify);
            if (!Directory.Exists(doc + @"\LocalCoder\"))
                Directory.CreateDirectory(doc + @"\LocalCoder\");
            saveNewProject.InitialDirectory = doc + @"\LocalCoder\";
            saveNewProject.FileOk += new CancelEventHandler(saveNewProject_FileOk);
        }

        private void PrepareToOpenProject()
        {
            openProject.AddExtension = true;
            string doc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.DoNotVerify);
            if (!Directory.Exists(doc + @"\LocalCoder\"))
                Directory.CreateDirectory(doc + @"\LocalCoder\");
            openProject.InitialDirectory = doc + @"\LocalCoder\";
            openProject.FileOk += new CancelEventHandler(openProject_FileOk);
        }

        void openProject_FileOk(object sender, CancelEventArgs e)
        {
            byte[] data = File.ReadAllBytes(openProject.FileName);
            BlowFish bf = new BlowFish("C3Y14AD5955928NC");
            data = bf.Decrypt_ECB(data);

            MessageBox.Show(ASCIIEncoding.ASCII.GetString(data));
        }

        void saveNewProject_FileOk(object sender, CancelEventArgs e)
        {
            string newProjectFile = 
                "[LocalCoderProject]" + Environment.NewLine + 
                "Created=" + DateTime.Now.ToShortDateString() + Environment.NewLine + 
                "ProjectName=" + saveNewProject.FileName + Environment.NewLine +
                "Files=1" + Environment.NewLine +
                "File0=index.php" + Environment.NewLine +
                "FileType0=PHP_FILE"
            ;

            byte[] data = BlowFish.StrToByteArray(newProjectFile);
            BlowFish bf = new BlowFish("C3Y14AD5955928NC");
            data = bf.Encrypt_ECB(data);

            File.WriteAllBytes(saveNewProject.FileName, data);


            FileInfo f = new FileInfo(saveNewProject.FileName);
            while (!Directory.Exists(f.Directory.FullName + "\\bin"))
            {
                Directory.CreateDirectory(f.Directory.FullName + "\\bin");
            }
            File.WriteAllBytes(f.Directory.FullName + "\\bin\\index.php", new byte[]{});

            TreeNode ti = new TREE_ITEM() as TreeNode;
            ti.Text = "Project Name";
            ti.ImageIndex = 0;
            ti.SelectedImageIndex = 0;

            TreeNode ti2 = new TREE_ITEM() as TreeNode;
            ti2.Text = "index";
            ti2.ImageIndex = 1;
            ti2.SelectedImageIndex = 1;

            projectTree.Nodes.Add(ti);
            projectTree.Nodes[0].Nodes.Add(ti2);
            projectTree.Nodes[0].Expand();
        }

        private void projectSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.ColumnStyles[0].Width > 0F)
                tableLayoutPanel1.ColumnStyles[0].Width = 0F;
            else
                tableLayoutPanel1.ColumnStyles[0].Width = 14F;
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProject.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(Environment.ExitCode);
        }
    }
}
