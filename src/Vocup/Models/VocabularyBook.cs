using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Models
{
    public class VocabularyBook
    {
        private ListView _listViewControl;

        public VocabularyBook()
        {
            var words = new ObservableCollection<VocabularyWord>();
            words.CollectionChanged += Words_CollectionChanged;
            Words = words;
        }

        IList<VocabularyWord> Words { get; }
        ListView ListViewControl => _listViewControl ?? RegisterListView(new ListView());
        // TODO: Push changes to ListView
        public string FileVersion { get; set; }
        public string VhrCode { get; set; }
        public string MotherTongue { get; set; }
        public string ForeignLang { get; set; }

        public ListView RegisterListView(ListView control)
        {
            _listViewControl = control;
            foreach (VocabularyWord word in Words) // Load current items
                _listViewControl.Items.Add(word.ListViewItem);
            return control;
        }

        public void ReadFile(string path)
        {

        }

        public void WriteFile(string path)
        {

        }

        private void Words_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (VocabularyWord word in e.NewItems)
                    {
                        word.Owner = this;
                        _listViewControl?.Items.Add(word.ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (VocabularyWord word in e.OldItems)
                    {
                        word.Owner = null;
                        _listViewControl?.Items.Remove(word.ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (VocabularyWord word in e.OldItems)
                    {
                        word.Owner = null;
                        _listViewControl?.Items.Remove(word.ListViewItem);
                    }
                    foreach (VocabularyWord word in e.NewItems)
                    {
                        word.Owner = this;
                        _listViewControl?.Items.Add(word.ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break; // sort will be done on UI layer

                case NotifyCollectionChangedAction.Reset:
                    foreach (VocabularyWord word in e.OldItems)
                        word.Owner = null;
                    _listViewControl?.Items.Clear();
                    break;
            }
        }
    }
}
