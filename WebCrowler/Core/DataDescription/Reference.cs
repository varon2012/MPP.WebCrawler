using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataDescription
{
    public class ReferenceDescription : IData
    {
        public string Reference { get; set; }
        public ObservableCollection<IData> ReferenceList { get; set; }
    }
}
