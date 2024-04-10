using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WJLThoughts.Common.Core.SocketUtils
{
   public class SocketServer
    {
        private TcpListener _tcpServer;
        private bool isRunning = false;
        private Task _monitorTask;
        public bool Connect(int port)
        {
            try
            {
                _tcpServer = new TcpListener(IPAddress.Any, port);
                if (!isRunning)
                {
                    if (_tcpServer != null)
                    {
                        isRunning = true;
                        if (_monitorTask == null || _monitorTask.IsCompleted)
                        {
                            _monitorTask = Task.Factory.StartNew(
                                () =>
                                {
                                    while (isRunning)
                                    {
                                        checkClientsStatus();
                                        System.Threading.Thread.Sleep(2000);
                                    }
                                }, TaskCreationOptions.LongRunning);
                        }
                        _tcpServer.Start();
                        _tcpServer.BeginAcceptSocket(new AsyncCallback(HasNewClient), _tcpServer);
                    }
                    else
                    {
                        isRunning = false;
                    }
                }
                return isRunning;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  bool DisConnect()
        {
            if (_tcpServer != null && isRunning)
            {
                try
                {
                    _tcpServer.Stop();
                    foreach (var item in tcpClients)
                    {
                        item.Close();
                        item.Dispose();
                    }
                    tcpClients.Clear();
                    isRunning = false;
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }
        public event EventHandler<object> PushResultEvent;
        public event EventHandler<TcpClient> ClientOfflineEvent;
        private readonly List<TcpClient> tcpClients = new List<TcpClient>();
        private Queue<TcpClient> removeQueue = new Queue<TcpClient>();
        private void HasNewClient(IAsyncResult asyncResult)
        {
            try
            {
                TcpListener listener = (TcpListener)asyncResult.AsyncState;
                TcpClient tcpClient = listener.EndAcceptTcpClient(asyncResult);
                tcpClient.Client.IOControl(IOControlCode.KeepAliveValues, GetKeepAliveData(), null);
                tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                tcpClients.Add(tcpClient);
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] buffRece = new byte[tcpClient.ReceiveBufferSize];
                networkStream.BeginRead(buffRece, 0, buffRece.Length, HasNewMsg, new KeyValuePair<byte[], TcpClient>(buffRece, tcpClient));
                _tcpServer.BeginAcceptSocket(new AsyncCallback(HasNewClient), _tcpServer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Dictionary<TcpClient, string> receives = new Dictionary<TcpClient, string>();
        private object lockObj = new object();
        private void HasNewMsg(IAsyncResult asyncResult)
        {
            if (isRunning)
            {
                try
                {
                    KeyValuePair<byte[], TcpClient> keyValue = (KeyValuePair<byte[], TcpClient>)asyncResult.AsyncState;
                    byte[] buffRece = keyValue.Key;
                    TcpClient tcpClient = keyValue.Value;
                    int recv = 0;
                    try
                    {
                        recv = tcpClient.GetStream().EndRead(asyncResult);
                    }
                    catch
                    {
                    }
                    if (recv > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffRece, 0, recv);
                        PushResultEvent?.BeginInvoke(this, message, null, null);
                        tcpClient.GetStream().BeginRead(buffRece, 0, buffRece.Length, HasNewMsg, keyValue);
                    }
                    else
                    {
                        ClientOfflineEvent?.Invoke(this,tcpClient);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                System.Threading.Thread.Sleep(10);
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendData(string msg)
        {
            try
            {
                if (tcpClients.Count < 1) return false;
                foreach (var item in tcpClients)
                {
                    NetworkStream network = item.GetStream();
                    byte[] buffer = Encoding.UTF8.GetBytes(msg);
                    network.Write(buffer, 0, buffer.Length);
                };
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void checkClientsStatus()
        {
            try
            {
                lock (tcpClients)
                {
                    foreach (var item in tcpClients)
                    {
                        if (!isClientConnected(item))
                        {
                            removeQueue.Enqueue(item);
                            ClientOfflineEvent?.Invoke(this, item);
                            continue;
                        }
                    }
                    while (removeQueue.Count > 0)
                    {
                        TcpClient item = removeQueue.Dequeue();
                        tcpClients.Remove(item);
                        try
                        {
                            item.Close();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool isClientConnected(TcpClient ClientSocket)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

            TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation c in tcpConnections)
            {
                TcpState stateOfConnection = c.State;

                if (c.LocalEndPoint.Equals(ClientSocket.Client.LocalEndPoint) && c.RemoteEndPoint.Equals(ClientSocket.Client.RemoteEndPoint))
                {
                    if (stateOfConnection == TcpState.Established)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }

            return false;
        }

        private byte[] GetKeepAliveData()
        {
            uint dummy = 0;
            byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)3000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            BitConverter.GetBytes((uint)500).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
            return inOptionValues;
        }

    }
}
