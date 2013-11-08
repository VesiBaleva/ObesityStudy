using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObesityStudyWin8.Model
{
    public class GroupedData<T> :List<T>
    {
        public object Key { get; set; }

        public new IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)base.GetEnumerator();
        }
    }
}
