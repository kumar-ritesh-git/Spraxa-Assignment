using System.ComponentModel.DataAnnotations;

namespace MedicinesApi.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }                     // Primary key
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }        // when the record was created (UTC)
    }
}
