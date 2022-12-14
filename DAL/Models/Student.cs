using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace DataAccessLayer.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(maximumLength: 50, ErrorMessage = "The Name must contain a max of 50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(maximumLength: 50, ErrorMessage = "The LastName must contain a max of 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "NID is required.")]
        [StringLength(14, ErrorMessage = "The NID must contain 14 characters", MinimumLength = 14)]
        public string NID { get; set; }
        
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Email input does not match expression")]
        public string Email { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(maximumLength: 50, ErrorMessage = "The Guardian must contain a max of 50 characters")]
        public string GuardianName { get; set; }
        public int UserId { get; set; }
        public string[] Subjects { get; set; }
        public char[] Grades { get; set; }
        public string Status { get; set; }
        
        public Student()
        {

        }
        public Student(int studentId, string nID, string firstName, string lastName,  string phoneNumber, DateTime dateOfBirth, string email, string guardianName, int userId, string status)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            NID = nID;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Email = email;
            GuardianName = guardianName;
            UserId = userId;
            Status = status;
        }
    }
}