using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class Chat : Form
    {
        public static HubConnection SignalRConnection { get; private set; }
        public string name;
        public string group;
        
        public Chat(string name, string group)
        {            
            InitializeComponent();
            this.name = name;
            this.group = group;

            InitializeSignalR();         
        }

        private async void InitializeSignalR()
        {
            var uri = "http://localhost:5229/chat";
            // Inicializa a conexão SignalR
            SignalRConnection = new HubConnectionBuilder().WithUrl(uri).Build();


            // Define o callback
            SignalRConnection.On<string, string>("ReceiveMessage", (userName, message) =>
            {
                Invoke(new Action(() =>
                {
                    txtChat.AppendText($"{userName}: {message}{Environment.NewLine}");
                }));
            });

            // Conecta ao servidor
            try
            {
                await SignalRConnection.StartAsync();
                await SignalRConnection.InvokeAsync("JoinGroup", group);
                txtChat.AppendText("Conexão estabelecida com o servidor SignalR.\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            await SignalRConnection.InvokeAsync("SendMessageToGroup", group, name, txtInput.Text);
            txtInput.Text = string.Empty;
        }
    }
}
