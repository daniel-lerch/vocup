using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Controls
{
    public partial class FileTreeView : UserControl
    {
        private Size _imageScalingBaseSize = new Size(16, 16);
        private string _fileFilter = "*.*";
        private string _rootPath = "";
        private SizeF scalingFactor = new SizeF(1F, 1F);

        public FileTreeView()
        {
            InitializeComponent();
            MainTreeView.PathSeparator = Path.DirectorySeparatorChar.ToString();
        }

        [DefaultValue(typeof(Size), "16,16")]
        public Size ImageScalingBaseSize
        {
            get => _imageScalingBaseSize;
            set
            {
                _imageScalingBaseSize = value;
                ScaleImageList();
            }
        }

        [DefaultValue("*.*")]
        public string FileFilter
        {
            get => _fileFilter;
            set
            {
                _fileFilter = value;
                MainWatcher.Filter = value;
            }
        }

        [DefaultValue("")]
        public string RootPath
        {
            get => _rootPath;
            set
            {
                MainWatcher.EnableRaisingEvents = false;
                MainTreeView.BeginUpdate();
                MainTreeView.Nodes.Clear();
                DirectoryInfo info = new DirectoryInfo(value);
                TreeNode node = MainTreeView.Nodes.Add(info.Name);
                node.Tag = info;
                LoadNodes(node);
                MainTreeView.EndUpdate();
                _rootPath = value;
                MainWatcher.Path = value;
                MainWatcher.EnableRaisingEvents = true;
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            scalingFactor = scalingFactor.Multiply(factor);
            ScaleImageList();
            base.ScaleControl(factor, specified);
        }

        private void ScaleImageList()
        {
            ImageList old = MainTreeView.ImageList;
            MainTreeView.ImageList = IconImageList.Scale(_imageScalingBaseSize.Multiply(scalingFactor).Rectify().Round());
            old?.Dispose();
        }

        /// <summary>
        /// Refreshes a associated node and loads eventually new files.
        /// </summary>
        public void RefreshPath(string path)
        {

        }

        private void LoadNodes(TreeNode root)
        {
            root.Nodes.Clear();
            DirectoryInfo rootInfo = (DirectoryInfo)root.Tag;
            DirectoryInfo[] directories = rootInfo.GetDirectories();
            FileInfo[] files = rootInfo.GetFiles(_fileFilter);
            foreach (DirectoryInfo directory in directories)
            {
                root.Nodes.Add(new TreeNode()
                {
                    Tag = directory,
                    Text = directory.Name,
                    ImageIndex = 1,
                    SelectedImageIndex = 1
                });
            }
            foreach (FileInfo file in files)
            {
                root.Nodes.Add(new TreeNode()
                {
                    Tag = file,
                    Text = Path.GetFileNameWithoutExtension(file.FullName),
                    ImageIndex = 2,
                    SelectedImageIndex = 2,
                });
            }
        }

        private TreeNode GetNode(string path)
        {
            // TODO: Get relative path
            string[] names = path.Split(Path.DirectorySeparatorChar);

            if (MainTreeView.Nodes.Count == 0)
                return null;

            TreeNode currentNode = MainTreeView.Nodes[0];

            foreach (string name in names)
            {
                currentNode = currentNode.Nodes[name];
                if (currentNode == null)
                    return currentNode;
            }

            return currentNode;
        }

        private void MainTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                if (node.Tag is DirectoryInfo)
                    LoadNodes(node);
            }
        }

        private void MainTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Nodes.Clear();
            }
        }

        private void MainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void MainWatcher_Created(object sender, FileSystemEventArgs e)
        {
            
        }

        private void MainWatcher_Deleted(object sender, FileSystemEventArgs e)
        {

        }

        private void MainWatcher_Renamed(object sender, RenamedEventArgs e)
        {

        }

        // FileSystemWatcher watcher is being disposed in FileTreeView.Designer.cs
    }
}
