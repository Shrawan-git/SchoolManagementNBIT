using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _Db;

        public StudentController(StudentContext Db)
        {
            _Db = Db;
        }
        public IActionResult StudentList()
        {
            try
            {
                //var stdList = _Db.tbl_Students.ToList();

                var stdList = from a in _Db.tbl_Student
                              join b in _Db.tbl_Departments
                              on a.DepID equals b.ID into Dep
                              from b in Dep.DefaultIfEmpty()

                              select new Student
                              {
                                  ID = a.ID,
                                  FirstName = a.FirstName,
                                  LastName = a.LastName,
                                  Address = a.Address,
                                  Email = a.Email,
                                  Phone = a.Phone,
                                  DepID = a.DepID,

                                  Department = b == null ? "" : b.Department
                              };

                return View(stdList);

            }
            catch (Exception ex)
            {
                return View();

            }
        }

        [HttpPost]
        public IActionResult Deleted(FormCollection formCollection)
        {
            string[] IDs = formCollection["studentId"];
            foreach(string ID in IDs)
            {
                var stud = _Db.tbl_Student.Find(int.Parse(ID));
                _Db.tbl_Student.Remove(stud);
                _Db.SaveChanges();
                
            }
            return RedirectToAction("StudentList");
        } 
    
    
        public IActionResult Create(Student obj)
        {
            loadDDL ();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student obj)
        {
            try
            {

                    if (obj.ID==0)
                    {
                        _Db.tbl_Student.Add(obj);
                        await _Db.SaveChangesAsync();
                    }
                    else
                    {
                        _Db.Entry(obj).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();

                    }
                     return RedirectToAction("StudentList");

                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("StudentList");
            }
        }

        public async Task<IActionResult> DeleteStudent(int ID)
        {
            try
            {
                if (checked(true)){
                        var std = await _Db.tbl_Student.FindAsync(ID);
                        if (std != null)
                        {
                            _Db.tbl_Student.Remove(std);
                            await _Db.SaveChangesAsync();
                        }


                        return RedirectToAction("StudentList");
                    }
                    else
                    {
                    return RedirectToAction("StudentList");
                }
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("StudentList");
            }
        }

       

        private void loadDDL()
        {
            try
            {
                List<Departments> depList = new List<Departments>();
                depList = _Db.tbl_Departments.ToList();
                depList.Insert(0, new Departments { ID = 0, Department = "Please Select" });

                ViewBag.DepList = depList;
            }
            catch(Exception ex)
            {

            }
        }
    }
}
