using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG_ICE_3
{
    public partial class Form1 : Form
    {
        //-------------------------------------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------
        private void sendBtn_Click(object sender, EventArgs e)
        {
            MessageQueue mq;
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                mq = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                mq = MessageQueue.Create(@".\Private$\MyQueue");
            }

            // Create a message with an emoji
            string messageWithEmoji = txtInput.Text + " 😄"; // Add an emoji to the message
            mq.Send(messageWithEmoji);


        }

        //-------------------------------------------------------------------------------------------------------------
        private void receiveBtn_Click(object sender, EventArgs e)
        {
            MessageQueue mq = new MessageQueue(@".\Private$\MyQueue");
            mq.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });

            try
            {
                System.Messaging.Message msg = mq.Receive(new TimeSpan(0, 0, 3));
                string messageWithEmoji = msg.Body.ToString();
                txtOutput.Text = messageWithEmoji;
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                {
                    txtOutput.Text = "No new messages.";
                }
                else
                {
                    // Handle other message queue exceptions
                    txtOutput.Text = "Error: " + ex.Message;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------

    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------