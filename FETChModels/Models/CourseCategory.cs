using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Models
{
	public class CourseCategory
	{
		public int Id { get; set; }                        // ��������� ������������� �������
		public string Name { get; set; }                   // ����� �������
		public string Slug { get; set; }                   // URL-������ ������������� (��� �����)
	}
}