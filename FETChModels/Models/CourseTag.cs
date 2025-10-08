using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class CourseTag //???
    {
        public int CourseId { get; set; }                  // ��������� �� ���� (FK)
        public Course? Course { get; set; }                 // ���������� ���������� �� ����
        public int TagId { get; set; }                     // ��������� �� ��� (FK)
        public Tag? Tag { get; set; }                       // ���������� ���������� �� ���
    }
}