using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using u21512338_HW05.Models;

namespace u21512338_HW05.Models
{
    public class DataService
    {
        private String ConnectionString;

        public DataService()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Book> getAllBooks()
        {
            List<Book> Books = new List<Book>();

            try
            {
                SqlConnection myConnection = new SqlConnection(ConnectionString);
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("Select books.bookId as bID, books.name as bName, books.pagecount as pagecount, books.point as point, authors.surname as authorSName, types.name as typeName FROM books Inner join authors on books.authorId = authors.authorId Inner join types on books.typeId = types.typeId", myConnection);

                using (SqlDataReader myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        Book book = new Book();
                        book.bookID = (int)myReader["bID"];
                        book.aName = myReader["bName"].ToString();
                        book.pageCount = (int)myReader["pagecount"];
                        book.point = (int)myReader["point"];
                        book.authorN = myReader["authorSName"].ToString();
                        book.typeN = myReader["typeName"].ToString();

                        Books.Add(book);
                    }
                }

                myConnection.Close();
            }
            catch (Exception err)
            {
            }

            return Books;
        }

        public List<Borrow> getAllBorrows(int bID)
        {
            List<Borrow> Borrows = new List<Borrow>();

            try
            {
                SqlConnection myConnection = new SqlConnection(ConnectionString);
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("Select borrows.borrowId as borrowID, borrows.takenDate as tDate, borrows.broughtDate as bDate, students.name + ' ' + students.surname as StudentFullName, books.name as bkName FROM borrows INNER JOIN students ON borrows.studentId = students.studentId INNER JOIN books ON borrows.bookId = books.bookId WHERE books.bookId =" + bID, myConnection);

                using (SqlDataReader myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        Borrow borrow = new Borrow();
                        borrow.borrowID = (int)myReader["borrowID"];
                        borrow.takenDate = (DateTime)myReader["tDate"];
                        borrow.broughtDate = (DateTime)myReader["bDate"];
                        borrow.studentName = myReader["StudentFullName"].ToString();
                        borrow.bookName = myReader["bkName"].ToString();

                        Borrows.Add(borrow);
                    }
                }

                myConnection.Close();
            }
            catch (Exception err)
            {
            }

            return Borrows;
        }

        public List<Student> getAllStudents()
        {
            List<Student> Students = new List<Student>();

            try
            {
                SqlConnection myConnection = new SqlConnection(ConnectionString);
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("Select students.studentId as sID, students.name as sName, students.surname as sSurname, students.class as sClass, students.point as sPoint FROM students", myConnection);

                using (SqlDataReader myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        Student student = new Student();
                        student.studentID = (int)myReader["sID"];
                        student.sName = myReader["sName"].ToString();
                        student.sSurname = myReader["sSurname"].ToString();
                        student.Class = myReader["sClass"].ToString();
                        student.point = (int)myReader["sPoint"];

                        Students.Add(student);
                    }
                }

                myConnection.Close();
            }
            catch (Exception err)
            {
            }

            return Students;
        }


        public static void BorrowBook(int sID, int bID)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection("Data Source=LAPTOP-NIKITA\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True");
                myConnection.Open();
                SqlCommand myInsertCommand = new SqlCommand("INSERT [dbo].[borrows] ( [studentId], [bookId], [takenDate]) VALUES ('" + sID + "','" + bID + "','" + DateTime.Now + "' AS DateTime))", myConnection);
                int rowsAffected = myInsertCommand.ExecuteNonQuery();

                myConnection.Close();
            }
            catch (Exception err)
            {
            }
        }

        public static void ReturnBook()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection("Data Source=LAPTOP-NIKITA\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True");
                myConnection.Open();
                SqlCommand myInsertCommand = new SqlCommand("INSERT [dbo].[borrows] ( [broughtDate]) VALUES ('" + DateTime.Now + "' AS DateTime))", myConnection);
                int rowsAffected = myInsertCommand.ExecuteNonQuery();

                myConnection.Close();
            }
            catch (Exception err)
            {
            }


        }

    }

}
