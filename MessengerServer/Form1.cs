using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using MessangerData;

namespace MessengerServer
{
    public partial class Form1 : Form
    {
        TcpListener listener = null;
        BinaryFormatter formatter = null;
        static List<MUser> users = new List<MUser>();
        Semaphore semaphore = new Semaphore(3, 3);

        public Form1()
        {
            InitializeComponent();

            formatter = new BinaryFormatter();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            //TODO get current user address
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), Convert.ToInt32(this.tbPort.Text.Trim()));
            listener.Start();

            Thread thread = new Thread(WaitForClient);
            thread.IsBackground = true;
            thread.Start();
        }

        private void WaitForClient(object? obj)
        {
            while (true)
            {
                semaphore.WaitOne();

                Thread thread = new Thread(Listen);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void Listen()
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream ns = client.GetStream();
                StreamReader reader = null;

                while (true)
                {
                    if (!ns.DataAvailable) continue;

                    reader = new StreamReader(ns, Encoding.UTF8);
                    MessageData data = (MessageData)formatter.Deserialize(reader.BaseStream);

                    switch (data.Action)
                    {
                        case "Login":
                            if (users.Any(u => u.Name.Equals(data.From)))
                            {
                                NetworkStream tempNs = client.GetStream();

                                using (MemoryStream tempMs = new MemoryStream())
                                {
                                    formatter.Serialize(tempMs, users.Select(u => u.Name).ToList());

                                    MessageData response = new MessageData()
                                    {
                                        Action = "Error.Name",
                                        Text = "User with that name already exists. Please change the name!",
                                        To = data.From,
                                        FileData = tempMs.ToArray(),
                                        CreatedAt = DateTime.Now,
                                        From = "System message",
                                    };

                                    using (MemoryStream tempMsForSend = new MemoryStream())
                                    {
                                        formatter.Serialize(tempMsForSend, response);
                                        tempNs.Write(tempMsForSend.ToArray(), 0, (int)tempMsForSend.Length);
                                        tempNs.Flush();
                                    }
                                }
                            }
                            else
                            {
                                users.Add(new MUser() { Name = data.From, ClientSocket = client });
                                this.Invoke((MethodInvoker)delegate () {
                                    lbUsers.Items.Add(data.From);
                                });

                                //Inform other chatters about new user

                                MemoryStream ms = new MemoryStream();
                                formatter.Serialize(ms, users.Select(u => u.Name).ToList());
                                MessageData response = new MessageData() 
                                {
                                    Action = "Login",
                                    Text = data.From + " has joined!",
                                    FileData = ms.ToArray(),
                                    To = "All",
                                    CreatedAt = DateTime.Now,
                                    From = data.From,
                                };

                                ms.Close();
                                SendGroupMessage(response);
                            }
                            break;
                        case "Logout":
                            users.Remove(users.Where(u => u.Name.Equals(data.From)).FirstOrDefault());

                            this.Invoke((MethodInvoker)delegate () {
                                lbUsers.Items.Remove(data.From);
                            });

                            MemoryStream ms2 = new MemoryStream();
                            formatter.Serialize(ms2, users.Select(u => u.Name).ToList());
                            MessageData response2 = new MessageData()
                            {
                                Action = "Logout",
                                Text = data.From + " has disconnected!",
                                FileData = ms2.ToArray(),
                                To = "All",
                                CreatedAt = DateTime.Now,
                                From = data.From,
                            };

                            ms2.Close();
                            SendGroupMessage(response2);
                            break;
                        case "Message":
                            this.Invoke((MethodInvoker)delegate () {
                                tbResponces.Text += data.From + ">" + data.Text + "\r\n";

                                if (data.To.Equals("All"))
                                {
                                    SendGroupMessage(data);
                                }
                                else
                                {
                                    SendPrivateMessage(data);
                                }
                            });
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void SendGeneralMessage(MessageData toSend, TcpClient socket)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, toSend);
                    NetworkStream ns = socket.GetStream();
                    ns.Write(ms.ToArray(), 0, (int)ms.Length);
                    ns.Flush();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("SendGeneralMessage:" + ex.Message);
            }
        }

        private void SendGroupMessage(MessageData toSend)
        {
            foreach (MUser user in users)
            {
                SendGeneralMessage(toSend, user.ClientSocket);
            }
        }

        private void SendPrivateMessage(MessageData toSend)
        {
            TcpClient clientSocket = users.Where(u => u.Name.Equals(toSend.To)).FirstOrDefault().ClientSocket;
            SendGeneralMessage(toSend, clientSocket);
        }
    }
}