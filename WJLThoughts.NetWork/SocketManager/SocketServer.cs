using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace WJLThoughts.NetWork.SocketManager
{
    class SocketServer
    {
        private static Socket _socket;
        static bool Connect()
        {
            _socket = _socket ?? new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            return false;
        }

    }
}
