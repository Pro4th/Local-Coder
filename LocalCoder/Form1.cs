using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace LocalCoder
{
    public partial class LCForm : Form
    {
        public Engine Engine;

        public LCForm()
        {
            InitializeComponent();

            Engine = new Engine(this);
            Engine.Start();
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
                        int cursorPosition = localCodeBox.SelectionStart;
                        int lineIndex = localCodeBox.GetLineFromCharIndex(cursorPosition);
                        int tabs = 0;
                        int tabsToAdd = tabs;
                        string[] lines = localCodeBox.Lines;
                        string currentTab = lines[lineIndex];
                        while (currentTab.StartsWith("\t"))
                        {
                            currentTab = currentTab.Remove(0, 1);
                            //MessageBox.Show(currentTab);
                            tabs++;
                            tabsToAdd++;
                        }

                        string newLineTab = "";
                        while (tabsToAdd > 0)
                        {
                            newLineTab = newLineTab + '\t';
                            tabsToAdd--;
                        }

                        List<string> _lines = lines.ToList();
                        _lines.Insert(lineIndex+1, "");
                        lines = _lines.ToArray();
                        lines[lineIndex+1] = newLineTab;
                        localCodeBox.Lines = lines;

                        localCodeBox.Select(localCodeBox.GetFirstCharIndexFromLine(lineIndex + 1) + tabs, 0);

                        e.SuppressKeyPress = true;
                        break;
                    }
            }
        }

        private void localCodeBox_KeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}
