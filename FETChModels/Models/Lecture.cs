using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Lecture
    {
        public int Id { get; set; }                        // ��������� ������������� ������
        public int ModuleId { get; set; }                  // ��������� �� ������ (FK)
        public Module? Module { get; set; }                 // ���������� ���������� �� ������
        public string? Title { get; set; }                  // ����� ������
        public string? Content { get; set; }                // �������� ����� ��� HTML ������� ������
        public string? VideoUrl { get; set; }               // ��������� �� ���� (���� �)
        public TimeSpan? Duration { get; set; }            // ��������� ������
        public int Order { get; set; }                     // ������� ����������� ������ � �����

        public ICollection<UserLectureProgress> UserLectureProgresses { get; set; } = new List<UserLectureProgress>();
    }
}