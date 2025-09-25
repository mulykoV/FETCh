using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class UserCourse
    {
        public string UserId { get; set; }                 // ������������� ����������� (FK)
        public ApplicationUser User { get; set; }                     // ���������� ���������� �� �����������

        public int CourseId { get; set; }                  // ������������� ����� (FK)
        public Course Course { get; set; }                 // ���������� ���������� �� ����

        public DateTime EnrolledDate { get; set; }         // ���� ������ �� ����
        public bool IsCompleted { get; set; }              // �� �������� ���� ����������
        public DateTime? CompletedDate { get; set; }       // ���� ���������� (���� ��������)
    }
}