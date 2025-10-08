using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Media //???
    {
        public int Id { get; set; }                        // ��������� ������������� ���������
        public string FileName { get; set; }               // ��'� �����
        public string FileUrl { get; set; }                // ��������� �� ���� (URL ��� ���� �� ������)
        public string MediaType { get; set; }              // ��� ���� (��������� "image", "video")
        public int? CourseId { get; set; }                 // �������: ����, �� ����� ����'������ ����
        public Course Course { get; set; }                 // ���������� ���������� �� ����
    }
}