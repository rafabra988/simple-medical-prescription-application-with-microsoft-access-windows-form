using System.Reflection.Emit;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace BDados
{
    public partial class Form2 : Form
    {
        //Iniciando variáveis e objetos necessários
        public int modo;
        public int codigo;
        private List<ReceitaDados> receitas;
        public event EventHandler AtualizacaoFeita;
        Bitmap memoryImage;

        public Form2(int modo, int codigo)
        {
            //construtor da classe Form2, recebe o modo de operação e o código da receita
            this.modo = modo;
            this.codigo = codigo;
            InitializeComponent();

            //setando data padrao para o dateTimePicker com o dia atual
            dateTimePicker1.Value = DateTime.Today;

            if (modo == 1)
            {
                //desativando elementos que nao serao usado nessa tela
                button3.Visible = false;
                pictureBox1.Visible = false;
                label9.Visible = false;
                button4.Visible = false;
            }
            else if (modo == 2)
            {
                //desativando elementos que nao serao usado nessa tela
                button2.Visible = false;
                pictureBox1.Visible = false;
                label9.Visible = false;
                button4.Visible = false;
                //carregando as receitas do banco de dados
                receitas = receitaDAO.LerTodos();
                //procurando a receita com o código fornecido
                foreach (var receita in receitas)
                {
                    if (receita.Codigo == codigo)
                    {
                        //preenchendo as textboxes com os dados da receita correspondente ao codigo
                        textBox1.Text = receita.nome_paciente;
                        textBox2.Text = receita.nome_remedio;
                        checkBox1.Checked = receita.remedio_controlado;
                        textBox3.Text = receita.descricao;
                        textBox4.Text = receita.nome_doutor;
                        dateTimePicker1.Value = receita.data_consulta;
                        textBox6.Text = receita.quantidade.ToString();
                        textBox7.Text = receita.vezes_ao_dia.ToString();
                        break;
                    }

                }
            }
            else if (modo == 3)
            {
                //desativando elementos que nao serao usado nessa tela
                pictureBox1.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                checkBox1.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                dateTimePicker1.Visible = false;
                textBox6.Visible = false;
                textBox7.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                //estilizacao tela de creditos
                label1.Text = "\t          Desenvolvido por Rafael M. A. do Amaral.\n Tópicos Especiais em Tecnologia da Informação III";
                label1.Location = new System.Drawing.Point(40, 120);
            }
            else if (modo == 4) {
                //desativando elementos que nao serao usado nessa tela
                button2.Visible = false;
                button3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                checkBox1.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                dateTimePicker1.Visible = false;
                textBox6.Visible = false;
                textBox7.Visible = false;

                //carregando as receitas do banco de dados
                receitas = receitaDAO.LerTodos();
                //procurando a receita com o código fornecido
                foreach (var receita in receitas)
                {
                    if (receita.Codigo == codigo)
                    {
                        //preenchendo os labels com os dados da receita correspondente ao codigo
                        label1.Text = "Nome do Paciente: "+ receita.nome_paciente;
                        label6.Text = "Consulta feita pelo Doutor(a):\n" + receita.nome_doutor;
                        label8.Text = "Data: "+receita.data_consulta.ToString("dd/MM/yyyy");
                        label5.Text = receita.nome_remedio + " - O remédio deve ser tomado " + receita.vezes_ao_dia + " vezes ao dia, com a quantidade de " + receita.quantidade + "\n comprimidos cada vez. \n\n\n\n\nDescrição: " + receita.descricao;
                        break;
                    }

                }

                //estilizacao receita medica
                this.Width = 700;
                this.Height = 900;
                this.BackColor = System.Drawing.Color.White;

                pictureBox1.Location = new System.Drawing.Point(310, 10);
                //cabeçalho
                label2.Text = "Governo do Estado do Rio de Janeiro \n Universidade do Estado do RIo de Janeiro \n Hospital Universitário Pedro Ernesto";
                label2.Location = new System.Drawing.Point(180, 110);
                label2.Font = new Font(label2.Font.FontFamily, 9, label2.Font.Style | FontStyle.Bold);
                label2.TextAlign = ContentAlignment.MiddleCenter;
                //receita medica
                label3.Text = "Receita Médica";
                label3.Font = new Font(label3.Font.FontFamily, 15, label3.Font.Style | FontStyle.Bold);
                label3.Location = new System.Drawing.Point(235, 230);
                //nome paciente
                label1.Font = new Font(label1.Font.FontFamily, 11, label1.Font.Style);
                label1.Location = new System.Drawing.Point(20, 290);
                //uso oral
                label4.Text = "Uso Oral";
                label4.Font = new Font(label4.Font.FontFamily, 11, label4.Font.Style | FontStyle.Bold);
                label4.Location = new System.Drawing.Point(20, 325);
                //precricao
                label5.Font = new Font(label5.Font.FontFamily, 11, label5.Font.Style);
                label5.Location = new System.Drawing.Point(20, 420);
                //roda pé
                //nome do doutor(a)
                label6.Font = new Font(label6.Font.FontFamily, 11, label6.Font.Style | FontStyle.Bold);
                label6.Location = new System.Drawing.Point(20, 695);
                //data
                label8.Font = new Font(label8.Font.FontFamily, 11, label8.Font.Style | FontStyle.Bold);
                label8.Location = new System.Drawing.Point(510, 695);
                //texto
                label7.Text = "ESSE\nDOCUMENTO\nNÃO\nTEM\nVALIDADE";
                label7.BackColor = Color.Transparent;
                label7.Font = new Font(label7.Font.FontFamily, 11, label7.Font.Style | FontStyle.Bold);
                label7.TextAlign = ContentAlignment.MiddleCenter;
                label7.Location = new System.Drawing.Point(1, 1);
                //assinatura
                label9.Text = "Assinatura do Doutor(a): __________________________________";
                label9.Location = new System.Drawing.Point(20, 750);
                //botoes
                button1.Location = new System.Drawing.Point(200, 830);
                button4.Location = new System.Drawing.Point(400, 830);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //inicializando a lista de receitas
            receitas = new List<ReceitaDados>();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //fechando o formulário sem fazer nenhuma alteração
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //verificando se as textboxes estão preenchidas antes de inserir a receita no banco de dados
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {

                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            //passando os dados preenchidos para a lista de receitas
            receitas.Add(new ReceitaDados
            {
                nome_paciente = textBox1.Text,
                nome_remedio = textBox2.Text,
                remedio_controlado = checkBox1.Checked,
                descricao = textBox3.Text,
                nome_doutor = textBox4.Text,
                data_consulta = dateTimePicker1.Value,
                quantidade = Convert.ToDouble(textBox6.Text),
                vezes_ao_dia = Convert.ToInt32(textBox7.Text)
            });
            //inserindo nova receita no banco de dados
            receitaDAO.Inserir(receitas.Last());
            //invocando o evento de atualização para notificar outras partes do programa
            AtualizacaoFeita?.Invoke(this, EventArgs.Empty);
            // fechando o formulário após a inserção
            this.Close();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //verificando se as textboxes estão preenchidas antes de atualizar a receita no banco de dados
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {

                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            //passando os dados preenchidos para a lista de receitas
            receitas.Add(new ReceitaDados
            {
                nome_paciente = textBox1.Text,
                nome_remedio = textBox2.Text,
                remedio_controlado = checkBox1.Checked,
                descricao = textBox3.Text,
                nome_doutor = textBox4.Text,
                data_consulta = dateTimePicker1.Value,
                quantidade = Convert.ToDouble(textBox6.Text),
                vezes_ao_dia = Convert.ToInt32(textBox7.Text),
                Codigo = codigo
            });

            //atualizando a receita no banco de dados
            receitaDAO.Atualizar(receitas.Last());
            //invocando o evento de atualização para notificar outras partes do programa
            AtualizacaoFeita?.Invoke(this, EventArgs.Empty);
            // fechando o formulário após a atualização
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        //parte responsável pela impressão da receita médica
        private void button4_Click(object sender, EventArgs e)
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(680, 810, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X+10, this.Location.Y+35, 0, 0, s);

            printDocument1.Print();
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
    }
}
