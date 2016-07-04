using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSoftware.Core
{
    public interface INotifier
    {
        void Notify(String message);
    }
}
