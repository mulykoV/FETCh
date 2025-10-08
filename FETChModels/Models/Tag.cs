using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Tag //???
    {
        public int Id { get; set; }                        // ��������� ������������� ����
        public string Name { get; set; }                   // ����� ����
        public string Slug { get; set; }                   // URL-����� ��� ����
        public ICollection<CourseTag> CourseTags { get; set; } // �����, �� ����� ��� ���
    }
}