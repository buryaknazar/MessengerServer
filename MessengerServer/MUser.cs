using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServer
{
    public class MUser
    {
        public string Name { get; set; }
        public TcpClient ClientSocket { get; set; }
    }
}
