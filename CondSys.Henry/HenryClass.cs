using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CondSys.Henry
{
    public class HenryClass
    {
        Socket client = null;
        private static ManualResetEvent connectDone = null;
        private static ManualResetEvent sendDone = null;
        private static ManualResetEvent disconnectDone = null;
        private byte[] bufferBytes = new byte[1024];
        private byte[] sizeBytes = new byte[1024];
        private static Int32 quantBytesRec = 0;
        string matriculaPedido = String.Empty;
        private string txt_DadosRecebidos;
        public string txt_MatriculaON { get; set; }

        public HenryClass()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connectDone = new ManualResetEvent(false);
            sendDone = new ManualResetEvent(false);
            disconnectDone = new ManualResetEvent(false);
        }

        public async Task Connect(string nroIp, int nroPorta)
        {
            IPAddress ipAddress = IPAddress.Parse(nroIp);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, nroPorta);
            string strCMD = String.Empty;
            string confirmCmd = "01+REON+00+5]00000000]]]0]0";
            string strResponse = String.Empty;
            bool conectado = client.Connected;
            try
            {
                if (conectado == false)
                {
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();
                }
                strCMD = CreatCMD(confirmCmd);
                Send(client, strCMD);

                sendDone.WaitOne();
                quantBytesRec = client.Receive(bufferBytes);
                if (Convert.ToInt32(CheckResponse()) == 0)
                {
                    client.BeginReceive(bufferBytes, 0, bufferBytes.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OnReceive(IAsyncResult ar)
        {
            string responseON = String.Empty;
            string strRec = String.Empty;
            string askCode = String.Empty;
            int idxByte = 0;
            int i = 0;

            try
            {
                quantBytesRec = client.EndReceive(ar);
                askCode = Convert.ToString((char)bufferBytes[15]);

                if (askCode.Equals("0"))
                {
                    while (i < quantBytesRec)
                    {
                        responseON += (char)bufferBytes[i];
                        txt_DadosRecebidos += responseON.ElementAt(i);
                        i = i + 1;
                    }

                    txt_DadosRecebidos += Environment.NewLine;

                    for (idxByte = 17; idxByte < 37; idxByte++)
                    {
                        strRec = strRec + responseON.ElementAt(idxByte);
                    }
                    string respostaOnline = string.Empty;
                    if (strRec.Equals("00000000000000000200") || strRec.Equals("00000000000000000965"))
                    {
                        txt_MatriculaON = strRec;
                        respostaOnline = "01+REON+00+1]3]Bem vindo a Henry!}Va Trabalhar!]1";
                    }
                    else
                    {
                        respostaOnline = "01+REON+00+30]3]Bem vindo a Henry!}Acesso negado!]1";
                    }
                    string strResposta = CreatCMD(respostaOnline);
                    Send(client, strResposta);
                    matriculaPedido = strRec;
                    txt_MatriculaON = matriculaPedido;
                }
                else
                {
                    responseON = String.Empty;
                    matriculaPedido = String.Empty;
                }

                //ControlAccess();
                client.BeginReceive(bufferBytes, 0, bufferBytes.Length, SocketFlags.None,
                                          new AsyncCallback(OnReceive), null);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreatCMD(String initCMD)
        {
            //Neste método é criado o comando com o byte inicial, o comando base, que é passado por parâmetro,
            //que será enviado para o equipamento, então é feito o calculo do CheckSum e então adcionado o byte final.
            string command = String.Empty; // Este é o comando final
            string preCommand = String.Empty; //Este é o comando parcial
            byte chkSum = 0;

            command = command + (char)(2);// start byte
            //txt_strCmd.Text = command + (char)(2);
            preCommand = preCommand + (char)(initCMD.Length);// tamanho do comando
            //txt_strCmd.Text += preCommand + (char)(initCMD.Length);
            preCommand = preCommand + (char)(0);
            //txt_strCmd.Text += (char)(0);
            preCommand = preCommand + initCMD;
            //txt_strCmd.Text += initCMD;

            // Execução do calculo CheckSum
            chkSum = CalcCheckSumString(preCommand);
            //txt_strCmd.Text += Convert.ToChar(chkSum);
            // Concatenação do comando final, que já tinha o byte inicial 02 com o comando parcial, constituido pelo tamanho do comando
            // + comando base.
            command = command + preCommand;
            //Concatenação do resultado do calculo CheckSum na string do comando final.
            command = command + Convert.ToChar(chkSum);
            // checksum
            // end byte
            command = command + (char)(3);
            //txt_strCmd.Text += (char)(3);

            return command;
        }

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.Default.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                //MessageBox.Show("Sent "+ bytesSent.ToString() +" bytes to server.");

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string CheckResponse()
        {
            string response = String.Empty;
            string strRec = String.Empty;
            int idxByte = 0;
            int i = 0;
            int x = 0, z = 0;

            while (i < quantBytesRec)
            {
                response = response + (char)bufferBytes[i];
                i = i + 1;
            }
            while (idxByte < quantBytesRec)
            {
                if (response.ElementAt(idxByte).Equals('+'))
                {
                    idxByte++;
                    x++;
                    while (x == 2 && z < 3)
                    {
                        strRec = strRec + response.ElementAt(idxByte);
                        idxByte++;
                        z++;
                    }
                }
                idxByte++;
            }
            return strRec;
        }

        private byte CalcCheckSumString(string data)
        {
            byte cks = (byte)(data.ElementAt(0));
            int i = 1;

            while (i < data.Length)
            {
                cks = (byte)(cks ^ (byte)(data.ElementAt(i)));
                i = i + 1;
            }
            return cks;
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Desconectar()
        {
            try
            {
                client.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), client);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DisconnectCallback(IAsyncResult ar)
        {
            try
            {
                // Complete the disconnect request.
                Socket client = (Socket)ar.AsyncState;
                client.EndDisconnect(ar);

                //MessageBox.Show("Socket disconnected to " + client.RemoteEndPoint.ToString(),
                //    client.RemoteEndPoint.ToString());

                // Signal that the disconnect is complete.
                disconnectDone.Set();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ColetaDigital(string nroIp, int nroPorta, int idMorador)
        {
            try
            {
                string confirmCmd = "01+REON+00+5]00000000]]]0]0";
                string confirmColetaDigital = $"01+CB+00+{idMorador}";
                if (!client.Connected)
                {
                    IPAddress ipAddress = IPAddress.Parse(nroIp);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, nroPorta);
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();
                    string sendCommand = CreatCMD(confirmCmd);
                    Send(client, sendCommand);
                    //01 + CB + 00 + 12345678
                }
                string strCMD = CreatCMD(confirmColetaDigital);
                Send(client, strCMD);

                sendDone.WaitOne();
                quantBytesRec = client.Receive(bufferBytes);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
