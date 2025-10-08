using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }                        // ��������� ������������� �������
        public string? Email { get; set; }                  // Email ���������
        public DateTime SubscribedDate { get; set; }       // ���� �������
        public bool IsConfirmed { get; set; }              // �� ����������� �������
    }
}