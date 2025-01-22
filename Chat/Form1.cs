namespace Chat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var chat = new Chat(nameTextBox.Text, groupTextBox.Text);
            chat.Show(); 
            this.Hide();
            nameTextBox.Text = string.Empty;
            groupTextBox.Text = string.Empty;
            
        }
    }
}
