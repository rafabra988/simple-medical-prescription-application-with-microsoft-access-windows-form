using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BDados
{
    public partial class Form1 : Form
    {
        private List<ReceitaDados> receitas;
        private int RegAtual = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicializando a lista de receitas
            receitas = receitaDAO.LerTodos();
            // Verifica se há receitas para exibir
            if (receitas.Count == 0)
            {
                MessageBox.Show("Nenhum registro encontrado.");
                return;
            }
            else
            {
                atualizar();
            }
        }

        private void PreencherDados()
        {

            string controlado;
            // Limpando a ListBox antes de preencher
            listBox1.Items.Clear();
            // Percorrendo a lista de receitas e adicionando os dados na ListBox
            foreach (var receita in receitas)
            {
                //substituindo o valor booleano por "Sim" ou "Não"
                if (receita.remedio_controlado == true)
                {
                    controlado = "Sim";
                }
                else
                {
                    controlado = "Não";
                }

                // Adicionando os dados formatados na ListBox
                listBox1.Items.Add("Código:"+receita.Codigo + " | Nome do Paciente: " + receita.nome_paciente + " | Nome do Remedio: " + receita.nome_remedio + " | Remedio Controlado: " + controlado + " | Descrição: " + receita.descricao + " | Nome do Doutor: " + receita.nome_doutor + " | Data da consulta: " +receita.data_consulta.ToString("dd/MM/yyyy") + " | Quantidade do Remedio: " +receita.quantidade + " | Quantidade de vezes ao dia: " +receita.vezes_ao_dia); // Adicionando o nome do aluno como exemplo  
            }

        }

        public void atualizar()
        {
            // Atualizando a lista de receitas
            receitas = receitaDAO.LerTodos();
            // Verifica se há receitas para exibir
            PreencherDados();
        }

        private void button3_Click(object sender, EventArgs e) // Sair
        {
            // Fechando a aplicação
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e) // Form2
        {
            // Chamando a Form2 para inserir uma nova receita
            Form2 tela = new Form2(1, 0);
            tela.AtualizacaoFeita += Form2_AtualizacaoFeita;
            tela.ShowDialog();
        }

        private void Form2_AtualizacaoFeita(object sender, EventArgs e)
        {
            // Evento disparado quando a atualização é feita na Form2
            atualizar();
        }

        public static int? PegarCodigoNumerico(string texto)
        {
            // Verifica se o texto é nulo ou vazio
            if (string.IsNullOrEmpty(texto))
            {
                return null;
            }

            // Procura a posição da palavra "Código:" no texto, ignorando maiúsculas e minúsculas
            int indiceCodigo = texto.IndexOf("Código:", StringComparison.OrdinalIgnoreCase);
            // Se não encontrar, retorna null
            if (indiceCodigo == -1)
            {
                return null;
            }
            // Pega a substring que vem depois de "Código:"
            string subStringDepoisCodigo = texto.Substring(indiceCodigo + "Código:".Length).Trim();
            // Procura a posição do caractere "|", que indica o fim do número do código
            int indiceBarra = subStringDepoisCodigo.IndexOf("|");
            
            string numeroString;
            if (indiceBarra != -1)
            {
                // Se encontrar o caractere "|", pega a substring até esse ponto
                numeroString = subStringDepoisCodigo.Substring(0, indiceBarra).Trim();
            }
            else
            {
                numeroString = subStringDepoisCodigo.Trim();
            }
            // Tenta converter a string para um número inteiro
            int codigo;
            if (int.TryParse(numeroString, out codigo))
            {
                return codigo;
            }
            else
            {
                return null;
            }
        }

        private void button5_Click(object sender, EventArgs e) // Remover
        {
            // Declaração de variáveis
            string textoSelecionado = null;
            // Verifica se algum item está selecionado na ListBox
            if (listBox1.SelectedItem != null)
            {
                // Pega o texto do item selecionado
                textoSelecionado = listBox1.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            // Pega o código numérico do texto selecionado e converte para int
            int? codigoNullable = PegarCodigoNumerico(textoSelecionado);
            // Verifica se o código é válido
            if (codigoNullable.HasValue) 
            {
                // Se o código for válido, chama o método de remoção passando o código
                int codigo = codigoNullable.Value; 
                receitaDAO.Remover(codigo);
                MessageBox.Show("Receita removida com sucesso!");
                receitas = receitaDAO.LerTodos();
                // Atualiza a ListBox com os dados mais recentes
                PreencherDados();
            }
            else
            {
                MessageBox.Show("Código inválido. Não foi possível remover a receita.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e) 
        {
            // Declaração de variáveis
            string textoSelecionado = null;
            // Verifica se algum item está selecionado na ListBox
            if (listBox1.SelectedItem != null)
            {
                // Pega o texto do item selecionado
                textoSelecionado = listBox1.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            // Pega o código numérico do texto selecionado e converte para int
            int? codigoNullable = PegarCodigoNumerico(textoSelecionado);

            if (codigoNullable.HasValue) 
            {
                //se o código for válido, chama a tela de edição passando o código
                int codigo = codigoNullable.Value;
                Form2 tela = new Form2(2, codigo);
                // Assinando o evento AtualizacaoFeita para atualizar a lista após a edição
                tela.AtualizacaoFeita += Form2_AtualizacaoFeita;
                tela.ShowDialog();
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Fechando a aplicação
            Application.Exit();
        }

        private void creditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chamando a tela de créditos
            Form2 tela = new Form2(3, 0);
            tela.AtualizacaoFeita += Form2_AtualizacaoFeita;
            tela.ShowDialog();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //declaração de variáveis
            string textoSelecionado = null;
            int codigo = 0;


            // Verifica se algum item está selecionado na ListBox
            if (listBox1.SelectedItem != null)
            {
                textoSelecionado = listBox1.SelectedItem.ToString();
                
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Pega o código numérico do texto selecionado e converte para int
            int? codigoNullable = PegarCodigoNumerico(textoSelecionado);
            //Verifica se o código é válido
            if (codigoNullable.HasValue)
            {
                // Se o código for válido, passar valor para a variavel codigo
                codigo = codigoNullable.Value;
                
            }
            else
            {
                MessageBox.Show("Código inválido. Não foi possível localizar a receita.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            //procurando o registro que foi selecionado na listbox
            foreach (var receita in receitas)
            {
                if (receita.Codigo == codigo)
                {
                    // Se o código for encontrado, abrir a tela de receita
                    Form2 tela = new Form2(4, codigo);
                    tela.AtualizacaoFeita += Form2_AtualizacaoFeita;
                    tela.ShowDialog();
                }
            }
        }
    }
}
