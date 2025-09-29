using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Course
    {
        public int Id { get; set; }                        // ��������� ������������� �����
        public required string Title { get; set; }                  // ����� �����
        public required string Subtitle { get; set; }               // ϳ���������� ��� �������� ���� �����
        public string? Description { get; set; }           // ��������� ���� �����
        public required string Language { get; set; }              // ���� ����� ("ua", "en" ����)
        public bool IsFree { get; set; }                  // �� ������������ ����
        [Precision(18, 2)] // ���� �������� �������� �� ����� (���������, ����� �� 18 ���� ��� ����� 2 ����� ���� ����)
        public decimal Price { get; set; }                // ֳ�� ����� (���� �������)
        public string? PriceCurrency { get; set; }         // ������ (UAH, USD)
        public DateTime? StartDate { get; set; }          // ���� ������� ����� (���� �)
        public DateTime? EndDate { get; set; }            // ���� ���������� ����� (���� �)
        public string? ImageUrl { get; set; }              // ����������/���������� �����
        public string? BannerImageUrl { get; set; }        // ����� ����� (��� ������� �������)
        public int DurationHours { get; set; }            // �������� ��������� ����� � �������
        public required CourseCategory Category { get; set; }      // �������� ����� (������� ������)
        public int CategoryId { get; set; }               // ������������� ������� (FK)
        public ICollection<Module>? Modules { get; set; }  // ������ ������ �����
        public ICollection<Lecture>? Lectures { get; set; }// ������, ���� ���� �� � �������
        public ICollection<CourseTag>? CourseTags { get; set; } // ���� ����� (��� �������/������)
        [NotMapped]
        public ICollection<UserCourse>? Enrollments { get; set; } // �����������, ������� �� ����
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}