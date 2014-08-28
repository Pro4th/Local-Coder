using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LocalCoder;
using LocalCoder.CORE;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime;
using System.Runtime.Serialization;

namespace LocalCoder.CORE
{
    public class TREE_ITEM : TreeNode
    {
        public string ma;
        public string Text { get; set; }
        public string ImageKey { get; set; }
        public int ImageIndex { get; set; }
        public TREE_ITEM()
        {
        }
        public TREE_ITEM(string text)
        {
            Text = text;
        }
        public TREE_ITEM(string text, TREE_ITEM[] children)
        {
            Text = text;
        }
        public TREE_ITEM(string text, int imageIndex, int selectedImageIndex)
        {
            ma = text;
            Text = text;
        }
        public TREE_ITEM(string text, int imageIndex, int selectedImageIndex, TREE_ITEM[] children)
        {
            Text = text;
        }
        public int SelectedImageIndex { get; set; }
        public string SelectedImageKey { get; set; }
        public int StateImageIndex { get; set; }
        public string StateImageKey { get; set; }
    }
}
