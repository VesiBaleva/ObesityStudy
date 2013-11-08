using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObesityStudyWin8.Services
{
    public interface IDataService
    {
        Task<IEnumerable> GetUpdates();

        IEnumerable GetUpdatesCategories();
    }
}
