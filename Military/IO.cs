using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractMilitary
{
    interface IMilitaryWriter<TO, TG> where TG : IMilitaryGroup<TO>
    {
        void WriteToFile(string path, TG group);
    }

    interface IMilitaryReader<TO, TG> where TG : IMilitaryGroup<TO>
    {
        TG ReadFromFile(string path);
    }

    interface IMilitaryGroup<TO>
    {
        IEnumerable<TO> Organizations { get; }
    }
}
