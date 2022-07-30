using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPStudentAJAX.Models;
using System.Data;
using System.Data.SqlClient;

namespace SPStudentAJAX.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public void InsertRecord(StudentClass ss)
        {

            //string msg = "";
            
            SqlConnection con = new SqlConnection(@"Data Source=ZUNAYED-DESKTOP\SQLEXPRESS02;Initial Catalog=studentmanagementajaxDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("InsertData", con);   //first e stored procedure er nam. then connection er nam
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", ss.Name);
            cmd.Parameters.AddWithValue("@BirthDate", ss.BirthDate);
            cmd.Parameters.AddWithValue("@Roll", ss.Roll);
            cmd.Parameters.AddWithValue("@Class_", ss.Class_);
            cmd.Parameters.AddWithValue("@ClassName", ss.ClassName);
            cmd.Parameters.AddWithValue("ClassStartDate", ss.ClassStartDate);
            if (ConnectionState.Closed == con.State)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();    
            
        }
        public List<StudentClass> GetAllData()
        {
            List<StudentClass> lst = new List<StudentClass>();
            SqlConnection con = new SqlConnection(@"Data Source=ZUNAYED-DESKTOP\SQLEXPRESS02;Initial Catalog=studentmanagementajaxDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SelectData", con);   //first e stored procedure er nam. then connection er nam
            cmd.CommandType = CommandType.StoredProcedure;
            if (ConnectionState.Closed == con.State)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                lst.Add(new StudentClass
                {
                    StudentID = Convert.ToInt32(dr["StudentID"]),
                    Name = dr["Name"].ToString(),
                    BirthDate = dr["BirthDate"].ToString(),
                    Roll = dr["Roll"].ToString(),
                    Class_ = dr["Class_"].ToString(),
                    ClassName = dr["ClassName"].ToString(),
                    ClassStartDate = dr["ClassStartDate"].ToString()
                });
            }

            con.Close();
            return lst;
        }

        public List<StudentClass> GetSingleData(Int32 StudentID)
        {
            List<StudentClass> lst = new List<StudentClass>();
            SqlConnection con = new SqlConnection(@"Data Source=ZUNAYED-DESKTOP\SQLEXPRESS02;Initial Catalog=studentmanagementajaxDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SelectSingleData", con);   //first e stored procedure er nam. then connection er nam
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentID", StudentID);
            if (ConnectionState.Closed == con.State)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lst.Add(new StudentClass
                {
                    StudentID = Convert.ToInt32(dr["StudentID"]),
                    Name = dr["Name"].ToString(),
                    BirthDate = dr["BirthDate"].ToString(),
                    Roll = dr["Roll"].ToString(),
                    Class_ = dr["Class_"].ToString(),
                    ClassName = dr["ClassName"].ToString(),
                    ClassStartDate = dr["ClassStartDate"].ToString()
                });
            }

            con.Close();
            return lst;
        }

        public ActionResult Display()
        {
            return View();
        }
        public JsonResult getList()
        {
            return Json(GetAllData(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleRow(Int32 StudentID)
        {
            return Json(GetSingleData(StudentID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRecord(Int32 StudentID)
        {

            //string msg = "";
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=ZUNAYED-DESKTOP\SQLEXPRESS02;Initial Catalog=studentmanagementajaxDB;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("DeleteData", con);   //first e stored procedure er nam. then connection er nam
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentID", StudentID);
                if (ConnectionState.Closed == con.State)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                return Json("Record Deleted", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json("Record not deleted", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult UpdateRecord(string sid, string n,string r,string dob,string c,string cn,string csd)
        {

            //string msg = "";
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=ZUNAYED-DESKTOP\SQLEXPRESS02;Initial Catalog=studentmanagementajaxDB;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("UpdateData", con);   //first e stored procedure er nam. then connection er nam
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentID", Convert.ToInt32(sid));
                cmd.Parameters.AddWithValue("@Name", n);
                cmd.Parameters.AddWithValue("@BirthDate", dob);
                cmd.Parameters.AddWithValue("@Roll", r);
                cmd.Parameters.AddWithValue("@Class_", c);
                cmd.Parameters.AddWithValue("@ClassName", cn);
                cmd.Parameters.AddWithValue("ClassStartDate", csd);
                if (ConnectionState.Closed == con.State)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                return Json("Record Updated", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Record not updated", JsonRequestBehavior.AllowGet);
            }

        }

    }
}
