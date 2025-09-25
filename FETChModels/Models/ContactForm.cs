using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Models
{
    public class ContactForm
    {
        public int Id { get; set; }                        // ��������� ������������� ������
        public string Name { get; set; }                   // ��'� �����������, ���� ������� ������
        public string Email { get; set; }                  // Email �����������
        public string Subject { get; set; }                // ���� �����������
        public string Message { get; set; }                // ����� �����������
        public DateTime SubmittedDate { get; set; }        // ���� �� ��� �������� ������
        public bool IsProcessed { get; set; }             // ��������, ��������� ������ �� �
    }
}