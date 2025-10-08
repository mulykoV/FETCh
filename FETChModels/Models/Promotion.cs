using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Promotion
    {
        public int Id { get; set; }                        // ��������� ������������� �����
        public required string Title { get; set; }                  // ����� �����
        public string? Code { get; set; }                   // �������� ��� ������������ ������
        [Precision(18, 2)]
        public decimal DiscountPercentage { get; set; }    // ³������ ������ (��������� 10%)
        [Precision(18, 2)]
        public decimal? DiscountAmount { get; set; }       // Գ������� ���� ������ (���� �������������)
        public DateTime StartDate { get; set; }            // ���� ������� 䳿 �����
        public DateTime EndDate { get; set; }              // ���� ���������� �����
        public bool IsActive { get; set; }                 // ������� ����� �� �
        public ICollection<Course>? Courses { get; set; }   // �����, �� ���� ������������� �����
    }
}