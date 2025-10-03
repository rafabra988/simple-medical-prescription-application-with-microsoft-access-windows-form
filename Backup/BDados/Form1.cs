using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;// Include para Access
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BDados
{
    public partial class Form1 : Form
    {
        private DataTable  DT;
        private int RegAtual=0;
        private int totalReg =0;
            
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // String de conexão:
            String ConnAccess = "provider=Microsoft.Jet.OLEDB.4.0;data source=G:\\S2B\\BDados\\Aula.mdb";

            // Abrindo a conexão:
            OleDbConnection Conn = new OleDbConnection(ConnAccess);

            // montando a consulta:
            String Consulta = "Select * from Alunos";

            // Criando o Data Adapter:
            OleDbDataAdapter DA = new OleDbDataAdapter(Consulta, Conn);

            // Criando o Data Set
            DataSet DSet1 = new DataSet();
            
            DA.Fill(DSet1, "Alunos");
            DT = DSet1.Tables["Alunos"];
            RegAtual = 0;
            totalReg = DT.Rows.Count;
            label2.Text = Convert.ToString(totalReg);
            PreencheDados();
            button1.Enabled = true;
            button2.Enabled = true;
            
        }

        private void PreencheDados()
        {
            if (RegAtual < 0 )
             {
                MessageBox.Show("Primeiro Registro. Não posso Voltar.");
                RegAtual =0;
            }   
            if (RegAtual >= totalReg)
            {
                MessageBox.Show("Último Registro. Não posso Continuar.");
                RegAtual = totalReg-1; 
            }
           textBox1.Text = DT.Rows[RegAtual]["mat"].ToString();
           textBox2.Text = DT.Rows[RegAtual]["nome"].ToString();
           textBox3.Text = DT.Rows[RegAtual]["telef"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegAtual++;
            PreencheDados();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegAtual--;
            PreencheDados();
        }
    }
}