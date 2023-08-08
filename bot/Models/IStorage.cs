using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using bot.Models;

namespace bot.Models
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
