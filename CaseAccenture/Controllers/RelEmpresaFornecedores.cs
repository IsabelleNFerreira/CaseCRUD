using CaseAccenture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CaseAccenture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelEmpresaFornecedoresController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RelEmpresaFornecedoresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /*Função para buscar Empresas e Fornecedores vinculados*/
        [HttpGet]
        [Route("GetRelEmpresaFornecedores")]
        public JsonResult GetRelEmpresaFornecedoress()
        {
            string query = @"select a.RelEmpresaFornecedorID, b.NomeFantasia, c.Nome 
                            from RelEmpresaFornecedor a, Empresa b, Fornecedor c
                            where b.EmpresaID = a.EmpresaID
                            and c.FornecedorID = a.FornecedorID";

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

        /*Função para vincular Empresas e Fornecedores ja cadastrados*/
        [HttpPost]
        [Route("PostRelEmpresasFornecedores")]
        public JsonResult PostRelEmpresasFornecedores(int EmpresaID, int FornecedorID)
        {
            string query = @"insert into RelEmpresaFornecedor values (@EmpresaID, @FornecedorID)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmpresaID", EmpresaID);
                    myCommand.Parameters.AddWithValue("@FornecedorID", FornecedorID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Inserção realizada com sucesso!");
        }

        /*Função para deletar Empresas e Fornecedores vinculados*/
        [HttpDelete]
        [Route("DeleteRelEmpresasFornecedores")]
        public JsonResult DeleteRelEmpresasFornecedores(int id)
        {
            string query = @"delete from RelEmpresaFornecedor where RelEmpresaFornecedorID = @RelEmpresaFornecedorID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@RelEmpresaFornecedorID", id);
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
