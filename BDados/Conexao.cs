using System;
using System.Data.OleDb;
using System.IO;

public static class Conexao
{
    private static OleDbConnection conexao;

    public static OleDbConnection ObterConexao()
    {
        if (conexao == null)
        {
            string diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
            string nomeArquivo = "p2.accdb";
            var diretorio = new DirectoryInfo(diretorioBase);

            while (diretorio != null && diretorio.GetFiles(nomeArquivo).Length == 0)
                diretorio = diretorio.Parent;

            string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={diretorio.FullName}\\{nomeArquivo}";
            conexao = new OleDbConnection(connStr);
        }

        if (conexao.State != System.Data.ConnectionState.Open)
            conexao.Open();

        return conexao;
    }

    public static void FecharConexao()
    {
        if (conexao != null && conexao.State == System.Data.ConnectionState.Open)
            conexao.Close();
    }
}
