using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    
        public class Module
        {
            public int Id { get; set; }                        // ��������� ������������� ������
            public int CourseId { get; set; }                  // ��������� �� ���� (FK)
            public Course? Course { get; set; }                 // ���������� ���������� �� ����
            public string? Title { get; set; }                  // ����� ������
            public string? Description { get; set; }           // ���� ������
            public int Order { get; set; }                     // ������� ����������� ������ � ����
            public ICollection<Lecture>? Lectures { get; set; } // ������ ������
        }
}