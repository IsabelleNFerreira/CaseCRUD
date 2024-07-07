using CaseAccenture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;

namespace CaseAccenture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FornecedorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /*Função para BUSCAR Fornecedores*/
        [HttpGet]
        [Route("GetFornecedores")]
        public JsonResult GetFornecedores()
        {
            string query = "select * from Fornecedor";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        /*Função para CRIAR novos Fornecedores*/
        [HttpPost]
        [Route("PostFornecedores")]
        public JsonResult PostFornecedores(Fornecedor fornc)
        {
            string query = @"insert into Fornecedor values (@CNPJ, @CPF, @RG, @DataNasc, @Nome, @Email, @CEP)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CNPJ", fornc.CNPJ);
                    myCommand.Parameters.AddWithValue("@CPF", fornc.CPF);
                    myCommand.Parameters.AddWithValue("@RG", fornc.RG);
                    myCommand.Parameters.AddWithValue("@DataNasc", fornc.DataNasc);
                    myCommand.Parameters.AddWithValue("@Nome", fornc.Nome);
                    myCommand.Parameters.AddWithValue("@Email", fornc.Email);
                    myCommand.Parameters.AddWithValue("@CEP", fornc.CEP);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Inserção realizada com sucesso!");
        }

        /*Função para EDITAR Fornecedores*/
        [HttpPut]
        [Route("PutFornecedores")]
        public JsonResult PutFornecedores(Fornecedor fornc)
        {
            string query = @"update Fornecedor
                            set CNPJ = @CNPJ,
                            CPF = @CPF,
                            RG = @RG,
                            DataNasc = @DataNasc,
                            Nome = @Nome,
                            Email = @Email,
                            CEP = @CEP
                            where FornecedorID = @FornecedorID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CNPJ", fornc.CNPJ);
                    myCommand.Parameters.AddWithValue("@CPF", fornc.CPF);
                    myCommand.Parameters.AddWithValue("@RG", fornc.RG);
                    myCommand.Parameters.AddWithValue("@DataNasc", fornc.DataNasc);
                    myCommand.Parameters.AddWithValue("@Nome", fornc.Nome);
                    myCommand.Parameters.AddWithValue("@Email", fornc.Email);
                    myCommand.Parameters.AddWithValue("@CEP", fornc.CEP);
                    myCommand.Parameters.AddWithValue("FornecedorID", fornc.FornecedorID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Edição realizada com sucesso!");
        }

        /*Função para DELETAR Fornecedores*/
        [HttpDelete]
        [Route("DeleteFornecedores")]
        public JsonResult DeleteFornecedores(int id)
        {
            string query = @"delete from Fornecedor where FornecedorID = @FornecedorID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FornecedorID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleção realizada com sucesso!");
        }
    }
}
