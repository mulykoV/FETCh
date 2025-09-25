using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class User
    {
        public int Id { get; set; }                        // ��������� ������������� �����������
        public string Email { get; set; }                  // Email �����������
        public string FullName { get; set; }               // ����� ��� �����������
        public string PasswordHash { get; set; }           // ��� ������ ��� �������
        public string AvatarUrl { get; set; }              // ������ �����������
        public DateTime RegisteredDate { get; set; }       // ���� ���������
        public ICollection<UserCourse> Enrollments { get; set; } // ������� �����
        public ICollection<UserLectureProgress> LectureProgresses { get; set; } // ������� �� �������
    }
}