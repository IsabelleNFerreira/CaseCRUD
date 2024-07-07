using CaseAccenture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CaseAccenture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmpresaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /*Função para BUSCAR Empresa*/
        [HttpGet]
        [Route("GetEmpresas")]
        public JsonResult GetEmpresas()
        {
            string query = "select * from Empresa";

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


        /*Função para verificar CNPJ, caso existente*/
        /*Esta deve ser implementada em uma das regras de validação durante a inserção e edição*/
        [HttpGet]
        [Route("VerificaCNPJ")]
        public JsonResult VerificaCNPJ(string cnpj)
        {
            string query = @"select * from Empresa where CNPJ = @CNPJ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CNPJ", cnpj);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        /*Função para CRIAR nova Empresa*/
        [HttpPost]
        [Route("PostEmpresas")]
        public JsonResult PostEmpresas(string CNPJ, string NomeFantasia, string CEP)
        {
            string query = @"insert into Empresa values (@CNPJ, @NomeFantasia, @CEP)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CNPJ", CNPJ);
                    myCommand.Parameters.AddWithValue("@NomeFantasia", NomeFantasia);
                    myCommand.Parameters.AddWithValue("@CEP", CEP);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Inserção realizada com sucesso!");
        }

        /*Função para EDITAR Empresa*/
        [HttpPut]
        [Route("PutEmpresas")]
        public JsonResult PutEmpresas(Empresa emp)
        {
            string query = @"update Empresa
                            set CNPJ = @CNPJ,
                            NomeFantasia = @NomeFantasia,
                            CEP = @CEP
                            where EmpresaID = @EmpresaID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmpresaID", emp.EmpresaID);
                    myCommand.Parameters.AddWithValue("@CNPJ", emp.CNPJ);
                    myCommand.Parameters.AddWithValue("@NomeFantasia", emp.NomeFantasia);
                    myCommand.Parameters.AddWithValue("@CEP", emp.CEP);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Edição realizada com sucesso!");
        }

        /*Função para DELETAR Empresa*/
        [HttpDelete]
        [Route("DeleteEmpresas")]
        public JsonResult DeleteEmpresas(int id)
        {
            string query = @"delete from Empresa where EmpresaID = @EmpresaID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AccentureCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmpresaID", id);
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
