using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Models
{
    public class VocabularyBook
    {
        public VocabularyBook()
        {
            var words = new ObservableCollection<VocabularyWord>();
            words.CollectionChanged += Words_CollectionChanged;
            Words = words;
        }

        IList<VocabularyWord> Words { get; }
        ListView ListViewControl { get; set; }

        private void Words_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
