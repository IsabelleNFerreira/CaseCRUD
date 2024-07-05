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


        [HttpPost]
        [Route("PostEmpresas")]
        public JsonResult PostEmpresas(Empresa emp)
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
                    myCommand.Parameters.AddWithValue("@CNPJ", emp.CNPJ);
                    myCommand.Parameters.AddWithValue("@NomeFantasia", emp.NomeFantasia);
                    myCommand.Parameters.AddWithValue("@CEP", emp.CEP);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPut]
        [Route("PutEmpresas")]
        public JsonResult PutEmpresas(Empresa emp)
        {
            string query = @"update Empresa
                            CNPJ = @CNPJ,
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
            return new JsonResult(table);
        }

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
            return new JsonResult(table);
        }
    }
}
