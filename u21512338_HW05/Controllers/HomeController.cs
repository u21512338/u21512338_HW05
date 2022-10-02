using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21512338_HW05.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace u21512338_HW05.Controllers
{
    public class HomeController : Controller
    {
        private DataService dataService = new DataService();
        public static List<Book> Books = new List<Book>();
        public static List<Student> Students = new List<Student>();
        private String ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ActionResult Index()
        {
            List<Book> books = dataService.getAllBooks();
            return View(books);

        }

        public ActionResult FuzzySearch(string searchText)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConnectionString);
                myConnection.Open();

                //Read
                SqlCommand myFuzzySearch = new SqlCommand("Select books.bookId as bID, books.name as bName, books.pagecount as pagecount, books.point as point, authors.surname as authorSName, types.name as typeName FROM books Inner join authors on books.authorId = authors.authorId Inner join types on books.typeId = types.typeId WHERE books.name LIKE '%" + searchText + "%'", myConnection);

                SqlDataReader myReader = myFuzzySearch.ExecuteReader();
                Books.Clear();
                while (myReader.Read())
                {
                    Book book = new Book();
                    book.bookID = (int)myReader["bID"];
                    book.aName = myReader["bName"].ToString();
                    book.pageCount = (int)myReader["pagecount"];
                    book.point = (int)myReader["point"];
                    book.authorN = myReader["authorSName"].ToString();
                    book.typeN = myReader["typeName"].ToString();

                    ViewBag.Status = 1;

                    Books.Add(book);
                }
                ViewBag.SearchStatus = 2;
                ViewBag.SearchText = "Showing results for: '" + searchText + "'";
                myConnection.Close();
            }
            catch (Exception err)
            {
                //ViewBag.Status = 0;
            }
            
            return View("Index", Books);
        }



        public ActionResult BookDetails(int bID)
        {
            List<Borrow> borrowHistory = dataService.getAllBorrows(bID);
            return View(borrowHistory);
        }

        public ActionResult StudentDetails()
        {
            List<Student> dbStudents = dataService.getAllStudents();
            return View(dbStudents);
        }

        public ActionResult StudentSearch(string searchText2)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConnectionString);
                myConnection.Open();

                //Read
                SqlCommand myStudentSearch = new SqlCommand("Select students.studentId as sID, students.name as sName, students.surname as sSurname, students.class as sClass, students.point as sPoint FROM students WHERE students.name LIKE '%" + searchText2 + "%'", myConnection);

                //SqlCommand myFuzzySearch = new SqlCommand("Select * From books WHERE name LIKE '%" + searchText + "%'", myConnection);

                SqlDataReader myReader = myStudentSearch.ExecuteReader();
                Students.Clear();
                while (myReader.Read())
                {
                    Student student = new Student();
                    student.studentID = (int)myReader["sID"];
                    student.sName = myReader["sName"].ToString();
                    student.sSurname = myReader["sSurname"].ToString();
                    student.Class = myReader["sClass"].ToString();
                    student.point = (int)myReader["sPoint"];

                    ViewBag.Status = 1;

                    Students.Add(student);
                }
                ViewBag.SearchStatus = 2;
                ViewBag.SearchText = "Showing results for: '" + searchText2 + "'";
                myConnection.Close();
            }
            catch (Exception err)
            {
                //ViewBag.Status = 0;
            }

            return View("StudentDetails", Students);
        }

        //public JsonResult CheckAvailability(DateTime bookData)
        //{
        //    System.Threading.Thread.Sleep(5000);
        //    var SearchData = Books.Where(x => x.bDate == bookData).SingleOrDefault();
        //    if (SearchData != null)
        //    {
        //        return Json(1);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }
        //}



    }
}