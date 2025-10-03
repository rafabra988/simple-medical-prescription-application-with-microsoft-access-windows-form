using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

public class ReceitaDados
{
    public int Codigo { get; set; }
    public string nome_paciente { get; set; }
    public string nome_remedio { get; set; }
    public bool remedio_controlado { get; set; }
    public string descricao { get; set; }
    public string nome_doutor { get; set; }
    public DateTime data_consulta { get; set; }
    public double quantidade { get; set; }
    public int vezes_ao_dia { get; set; }
}

public static class receitaDAO
{
    public static List<ReceitaDados> LerTodos()
    {
        List<ReceitaDados> receitas = new List<ReceitaDados>();
        string sql = "SELECT * FROM receita";
        using (OleDbCommand cmd = new OleDbCommand(sql, Conexao.ObterConexao()))
        {
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    receitas.Add(new ReceitaDados
                    {
                        Codigo = Convert.ToInt32(reader["codigo"]),
                        nome_paciente = reader["nome_paciente"].ToString(),
                        nome_remedio = reader["nome_remedio"].ToString(),
                        remedio_controlado = Convert.ToBoolean(reader["remedio_controlado"]),
                        descricao = reader["descricao"].ToString(),
                        nome_doutor = reader["nome_doutor"].ToString(),
                        data_consulta = Convert.ToDateTime(reader["data_consulta"]),
                        quantidade = Convert.ToDouble(reader["quantidade"]),
                        vezes_ao_dia = Convert.ToInt32(reader["vezes_ao_dia"])
                    });
                }
            }
        }
        Conexao.FecharConexao();
        return receitas;
    }

    public static void Inserir(ReceitaDados receita)
    {
        string sql = "INSERT INTO receita (nome_paciente, nome_remedio, remedio_controlado, descricao, nome_doutor, data_consulta, quantidade, vezes_ao_dia) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
        using (OleDbCommand cmd = new OleDbCommand(sql, Conexao.ObterConexao()))
        {
            cmd.Parameters.AddWithValue("?", receita.nome_paciente);
            cmd.Parameters.AddWithValue("?", receita.nome_remedio);
            cmd.Parameters.AddWithValue("?", receita.remedio_controlado);
            cmd.Parameters.AddWithValue("?", receita.descricao);
            cmd.Parameters.AddWithValue("?", receita.nome_doutor);
            cmd.Parameters.AddWithValue("?", receita.data_consulta);
            cmd.Parameters.AddWithValue("?", receita.quantidade);
            cmd.Parameters.AddWithValue("?", receita.vezes_ao_dia);
            cmd.ExecuteNonQuery();
        }
        Conexao.FecharConexao();
    }

    public static void Atualizar(ReceitaDados receita)
    {
        string sql = "UPDATE receita SET nome_paciente = ?, nome_remedio = ?, descricao = ?, nome_doutor = ?, data_consulta = ?, quantidade = ?, vezes_ao_dia = ?, remedio_controlado = ? WHERE codigo = ?";
        using (OleDbCommand cmd = new OleDbCommand(sql, Conexao.ObterConexao()))
        {
            cmd.Parameters.AddWithValue("?", receita.nome_paciente);
            cmd.Parameters.AddWithValue("?", receita.nome_remedio);
            cmd.Parameters.AddWithValue("?", receita.descricao);
            cmd.Parameters.AddWithValue("?", receita.nome_doutor);
            cmd.Parameters.AddWithValue("?", receita.data_consulta);
            cmd.Parameters.AddWithValue("?", receita.quantidade);
            cmd.Parameters.AddWithValue("?", receita.vezes_ao_dia);
            cmd.Parameters.AddWithValue("?", receita.remedio_controlado);
            cmd.Parameters.AddWithValue("?", receita.Codigo);
            cmd.ExecuteNonQuery();
        }
        Conexao.FecharConexao();
    }

    public static void Remover(int codigo)
    {
        string sql = "DELETE FROM receita WHERE codigo = ?";
        using (OleDbCommand cmd = new OleDbCommand(sql, Conexao.ObterConexao()))
        {
            cmd.Parameters.AddWithValue("?", codigo);
            cmd.ExecuteNonQuery();
        }
        Conexao.FecharConexao();
    }
}
