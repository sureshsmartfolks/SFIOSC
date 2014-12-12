using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ResourceManager.Models
{
    public interface ITranslatorRepository
    {
        DataTable GetAll();
        DataTable Get(int id);
        Translator Add(Translator item);
        void Remove(int id);
        bool Update(int id);

    }
}
