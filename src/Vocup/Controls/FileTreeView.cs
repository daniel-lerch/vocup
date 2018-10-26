using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls
{
    [DefaultEvent("FileSelected")]
    public partial class FileTreeView : UserControl
    {
        private const int ImgRoot = 0;
        private const int ImgRootSelected = 0;
        private const int ImgDir = 0;
        private const int ImgDirSelected = 0;
        private const int ImgFile = 2;
        private const int ImgFileSelected = 2;

        private Size _imageScalingBaseSize = new Size(16, 16);
        private string _rootPath = "";
        private SizeF scalingFactor = new SizeF(1F, 1F);
        private bool interceptEvents;

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
        public string FileFilter { get; set; } = "*.*";

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
                TreeNode node = new TreeNode
                {
                    Text = info.Name,
                    Tag = info,
                    ImageIndex = ImgRoot,
                    SelectedImageIndex = ImgRootSelected
                };
                MainTreeView.Nodes.Add(node);
                LoadNodes(node);
                MainTreeView.EndUpdate();
                _rootPath = value;
                MainWatcher.Path = value;
                MainWatcher.EnableRaisingEvents = true;
            }
        }

        [DefaultValue("")]
        public string SelectedPath
        {
            get => (MainTreeView.SelectedNode?.Tag as FileInfo)?.FullName;
            set
            {
                if (value == null) value = "";
                if (value != SelectedPath)
                {
                    interceptEvents = true;
                    MainTreeView.SelectedNode = GetNode(value);
                    interceptEvents = false;
                }
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public event EventHandler<FileSelectedEventArgs> FileSelected;

        protected virtual void OnFileSelected(FileSelectedEventArgs e)
        {
            if (!interceptEvents)
            {
                FileSelected?.Invoke(this, e);
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

        private void LoadNodes(TreeNode root)
        {
            root.Nodes.Clear();
            DirectoryInfo rootInfo = (DirectoryInfo)root.Tag;
            DirectoryInfo[] directories = rootInfo.GetDirectories();
            FileInfo[] files = rootInfo.GetFiles(FileFilter);
            foreach (DirectoryInfo directory in directories)
            {
                root.Nodes.Add(new TreeNode()
                {
                    Tag = directory,
                    Text = directory.Name,
                    ImageIndex = ImgDir,
                    SelectedImageIndex = ImgDirSelected
                });
            }
            foreach (FileInfo file in files)
            {
                root.Nodes.Add(new TreeNode()
                {
                    Tag = file,
                    Text = Path.GetFileNameWithoutExtension(file.FullName),
                    ImageIndex = ImgFile,
                    SelectedImageIndex = ImgFileSelected
                });
            }
        }

        private TreeNode LoadNode(TreeNode parent, string path)
        {
            TreeNode result = null;

            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                DirectoryInfo info = new DirectoryInfo(path);
                parent.Nodes.Add(result = new TreeNode()
                {
                    Tag = info,
                    Text = info.Name,
                    ImageIndex = 1,
                    SelectedImageIndex = 1
                });
                LoadNodes(result);
            }
            else if (PatternMatcher.StrictMatchPattern(FileFilter, path))
            {
                FileInfo info = new FileInfo(path);
                parent.Nodes.Add(result = new TreeNode()
                {
                    Tag = info,
                    Text = Path.GetFileNameWithoutExtension(info.FullName),
                    ImageIndex = 2,
                    SelectedImageIndex = 2
                });
            }

            return result;
        }

        private TreeNode GetNode(string path)
        {
            if (!path.ToLower().Contains(_rootPath.ToLower()))
                return null;
            string relativePath = path.Substring(_rootPath.Length);

            string[] names = relativePath.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            if (MainTreeView.Nodes.Count == 0)
                return null;

            TreeNode currentNode = MainTreeView.Nodes[0];

            foreach (string name in names)
            {
                currentNode = currentNode.Nodes.Cast<TreeNode>().Where(x => GetName(x) == name).FirstOrDefault();
                if (currentNode == null)
                    return currentNode;
            }

            return currentNode;
        }

        private string GetName(TreeNode node)
        {
            if (node.Tag is DirectoryInfo directory)
                return directory.Name;
            if (node.Tag is FileInfo file)
                return file.Name;
            return null;
        }

        private void SafeRemoveNode(TreeNode node)
        {
            if (node != null)
            {
                if (SelectedPath == (node.Tag as FileInfo)?.FullName)
                    SelectedPath = null; // prevent from automatically loading another file
                node.Remove();
            }
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
            if (e.Node.Tag is FileInfo)
            {
                OnFileSelected(new FileSelectedEventArgs(((FileInfo)e.Node.Tag).FullName));
            }
        }

        private void MainWatcher_Created(object sender, FileSystemEventArgs e)
        {
            TreeNode root = GetNode(Path.GetDirectoryName(e.FullPath));
            LoadNode(root, e.FullPath);
        }

        private void MainWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            TreeNode node = GetNode(e.FullPath);
            SafeRemoveNode(node); // null check because no FileFilter is applied to the watcher
        }

        private void MainWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            TreeNode old = GetNode(e.OldFullPath);
            SafeRemoveNode(old); // null check because user can change file ending to match FileFilter
            TreeNode root = GetNode(Path.GetDirectoryName(e.FullPath));
            LoadNode(root, e.FullPath);
        }
    }
}
