using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace SerialPortTest
{
    public partial class SerialPort : Form
    {
        public SerialPort( )
        {
            InitializeComponent( );
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            serialPort_Send.PortName = "COM2";
            serialPort_Send.BaudRate = 9600;

            serialPort_Receive.PortName = "COM3";
            serialPort_Receive.BaudRate = 9600;//设置串口名和波特率

            serialPort_Send.Open( );
            serialPort_Receive.Open( );//打开串口

            serialPort_Receive.DataReceived += new SerialDataReceivedEventHandler( DataReceviedHandler );//串口接收数据事件

        }

        private void DataReceviedHandler( object sender, SerialDataReceivedEventArgs e )
        {
            try
            {
                byte[ ] buffer = new byte[1024];
                serialPort_Receive.Read( buffer, 0, buffer.Length );

                string strReceive = System.Text.Encoding.UTF8.GetString( buffer );

                this.Invoke( new Action( ( ) =>
                {
                    this.textBox_Receive.Text = strReceive;
                } ) );

            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.ToString( ) );
            }
        }

        private void button1_Click( object sender, EventArgs e )
        {
            string strSend = textBox_Send.Text + '\r';

            if ( this.serialPort_Send.IsOpen )
            {
                serialPort_Send.Write( strSend );//发送数据
            }
            else
            {
                MessageBox.Show( "串口未打开！" );
            }
        }
        

    }
}
