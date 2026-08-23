
using SchoolSystem.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace SchoolSystem.Domain.Entities
{
    public class Course : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Credit { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt{ get; private set; }
        public Guid TeacherId { get; private set; }
        public Teacher Teacher { get; private set; } = null!;

        private Course() { }
        public Course(string name, int credit) 
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetCredit(credit);
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        public void UpdateCourse(string? name, int? credit)
        {
            bool isUpdated = false;
            if (name is not null)
            {
                SetName(name);
                isUpdated |= true;
            }
            if (credit.HasValue)
            {
                SetCredit(credit.Value);
                isUpdated |= true;
            }
            if (isUpdated) TouchUpdate();
        }
        public void AssignTeacher(Guid teacherId)
        {
            if (teacherId == Guid.Empty) throw new InvalidOperationException("Geçerli bir öğretmen id'si girin.");

            TeacherId = teacherId;
            TouchUpdate();
        }
        public void Deactivate()
        {
            IsActive = false;
            TouchUpdate();
        }
        public void Activate()
        {
            IsActive = true;
            TouchUpdate();
        }
        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Ad boş olamaz.", nameof(name));
            if (!Regex.IsMatch(name, @"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")) throw new ArgumentException("Ad sadece harflerden oluşmalı, sayı veya boşluk içeremez.", nameof(name));
            Name = name;
        }
        private void SetCredit(int credit)
        {
            if (credit <= 0 || credit > 10) throw new ArgumentException("Kurs kredisi 1 - 10 arasında olmalıdır.", nameof(credit));
            Credit = credit;
        }
        private void TouchUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
