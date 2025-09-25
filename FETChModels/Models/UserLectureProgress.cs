using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class UserLectureProgress
    {
        public int UserId { get; set; }                 // ������������� ����������� (FK)
        public User User { get; set; }                     // ���������� ���������� �� �����������

        public int LectureId { get; set; }                 // ������������� ������ (FK)
        public Lecture Lecture { get; set; }               // ���������� ���������� �� ������

        public bool Watched { get; set; }                  // �� ����������� ������
        public DateTime? WatchedDate { get; set; }         // ���� ���������
        public double ProgressPercentage { get; set; }     // ³������ �������� ������
    }
}