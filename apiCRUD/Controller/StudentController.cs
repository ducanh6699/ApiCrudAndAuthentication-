using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace apiCRUD.Controller
{
    public class Student
    {
        public string MSV { get; set; }
        public string HoTen { get; set; }
        public string Lop { get; set; }
    }

    public class StudentController : ApiController
    {

        public Student GetStudent(string fMSV)
        {
            var con = ConfigurationManager.ConnectionStrings["Demo"].ToString();

            Student matchingStudent = new Student();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from Sinh_Vien where MSV=@fMSV";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@fMSV", fMSV);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingStudent.MSV = oReader["MSV"].ToString();
                        matchingStudent.HoTen = oReader["HoTen"].ToString();
                        matchingStudent.Lop = oReader["Lop"].ToString();
                    }

                    myConnection.Close();
                }
            }
            return matchingStudent;
        }

        //[HttpGet]
        //public string student()
        //{
        //    return "abc";
        //}

        [HttpGet]
        public IEnumerable<string> studentID(int id, string name, string Class)
        {
            return new string[] {id.ToString(), name, Class};
        }

        [HttpGet]
        public Student studentID(string id)
        {
            return GetStudent(id);
        }

        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            IList<Sinh_Vien> students = null;

            using (var ctx = new DemoEntities())
            {
                students = ctx.Sinh_Vien.ToList<Sinh_Vien>();
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpPost]
        //Get action methods of the previous section
        public IHttpActionResult InsertNewStudent([FromBody]Sinh_Vien student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new DemoEntities())
            {
                try
                {
                    ctx.Sinh_Vien.Add(student);

                    ctx.SaveChanges();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
            }

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult EditStudent(Sinh_Vien student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new DemoEntities())
            {
                var existingStudent = ctx.Sinh_Vien.Where(s => s.MSV == student.MSV)
                                                        .FirstOrDefault<Sinh_Vien>();

                if (existingStudent != null)
                {
                    existingStudent.Ho = student.Ho;
                    existingStudent.Ten = student.Ten;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent(string id)
        {
            if (id.Length <= 1)
                return BadRequest("Not a valid student id");

            using (var ctx = new DemoEntities())
            {
                var student = ctx.Sinh_Vien
                    .Where(s => s.MSV == id)
                    .FirstOrDefault();

                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
