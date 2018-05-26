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

        public ListView RegisterListView(ListView control)
        {
            _listViewControl = control;
            // TODO: Load current items
            return control;
        }

        private void Words_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            throw new NotImplementedException();
        }
    }
}
