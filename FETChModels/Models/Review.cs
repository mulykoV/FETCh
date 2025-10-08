using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Review
    {
        public int Id { get; set; }                        // ��������� ������������� ������
        public int CourseId { get; set; }                  // ������������� ����� (FK)
        public Course? Course { get; set; }                 // ���������� ���������� �� ����

        public string? UserId { get; set; }                 // ������������� ����������� (FK, string ��� Identity)
        public ApplicationUser? User { get; set; }                     // ���������� ���������� �� �����������

        public string? Text { get; set; }                   // ����� ������
        public int Rating { get; set; }                    // ������ (1�5)
        public DateTime PostedDate { get; set; }
    }
}