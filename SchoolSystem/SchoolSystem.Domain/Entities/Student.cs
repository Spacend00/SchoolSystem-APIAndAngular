
using SchoolSystem.Domain.Enums;
using SchoolSystem.Domain.Interfaces;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SchoolSystem.Domain.Entities
{
    public class Student : IUserEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public int Age { get; private set; }
        public string SchoolNumber { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public int TotalCredit { get; private set; }
        public Role Role { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        private readonly List<StudentCourse> _studentCourses = new();
        public IReadOnlyCollection<StudentCourse> StudentCourses => _studentCourses.AsReadOnly();

        private Student() { }
        public Student(string name, string surname, int age, string schoolNumber, string email, string passwordHash, int totalCredit = 32, Role role = Role.Student)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetSurname(surname);
            SetAge(age);
            SetSchoolNumber(schoolNumber);
            SetEmail(email);
            SetPasswordHash(passwordHash);
            TotalCredit = totalCredit;
            Role = role;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        public void EnrollInCourse(Guid courseId)
        {
            var existingCourse = _studentCourses.FirstOrDefault(x => x.CourseId == courseId);
            if(existingCourse != null)
            {
                if (existingCourse.IsActive) throw new InvalidOperationException("Öğrenci bu kursa zaten katılı.");

                existingCourse.ReEnroll();
                TouchUpdate();
                return;
            }

            _studentCourses.Add(new StudentCourse(this.Id, courseId));
            TouchUpdate();
        }
        public void UnEnrollInCourse(Guid courseId)
        {
            var activeCourse = _studentCourses.FirstOrDefault(x => x.CourseId == courseId && x.IsActive);
            if (activeCourse == null) throw new InvalidOperationException("Öğrencinin bu kursa aktif bir kaydı bulunmamaktadır.");

            activeCourse.UnEnroll();
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
        public void UpdateProfile(string? name, string? surname, int? age)
        {
            bool isUpdated = false;
            if (name is not null)
            {
                SetName(name);
                isUpdated |= true;
            }
            if (surname is not null)
            {
                SetSurname(surname);
                isUpdated |= true;
            }
            if (age.HasValue)
            {
                SetAge(age.Value);
                isUpdated |= true;
            }
            if (isUpdated) TouchUpdate();
        }
        public void ChangeEmail(string email)
        {
            SetEmail(email);
            TouchUpdate();
        }
        public void ChangePasswordHash(string passwordHash)
        {
            SetPasswordHash(passwordHash);
            TouchUpdate();
        }
        public void ChangeSchoolNumber(string schoolNumber)
        {
            SetSchoolNumber(schoolNumber);
            TouchUpdate();
        }
        public void ChangeRole(Role role)
        {
            SetRole(role);
            TouchUpdate();
        }
        public void ChangeTotalCredit(int totalCredit)
        {
            SetTotalCredit(totalCredit);
            TouchUpdate();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Ad boş olamaz.", nameof(name));
            if (!Regex.IsMatch(name, @"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")) throw new ArgumentException("Ad sadece harflerden oluşmalı, sayı veya boşluk içeremez.", nameof(name));
            Name = name;
        }
        private void SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Soyad boş olamaz.", nameof(surname));
            if (!Regex.IsMatch(surname, @"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")) throw new ArgumentException("Soyad sadece harflerden oluşmalı, sayı veya boşluk içeremez.", nameof(surname));
            Surname = surname;
        }
        private void SetAge(int age)
        {
            if (age <= 15 || age >= 130) throw new ArgumentException("Yaş 15 - 130 arasında olmalı.", nameof(age));
            Age = age;
        }
        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("E-posta boş olamaz", nameof(email));
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) throw new ArgumentException("Geçersiz e-posta formatı.", nameof(email));
            Email = email;
        }
        private void SetSchoolNumber(string schoolNumber) 
        {
            if (string.IsNullOrWhiteSpace(schoolNumber)) throw new ArgumentException("Geçerli bir okul numarası girin.", nameof(schoolNumber));
            if (schoolNumber.Any(char.IsWhiteSpace)) throw new ArgumentException("Okul numarası boşluk içeremez.", nameof(schoolNumber));
            SchoolNumber = schoolNumber;
        }
        private void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("Şifre boş olamaz.", nameof(passwordHash));
            PasswordHash = passwordHash;
        }
        private void SetRole(Role role)
        {
            if (!Enum.IsDefined(typeof(Role), role)) throw new ArgumentException("Geçerli bir rol girin.", nameof(role));
            Role = role;
        }
        private void SetTotalCredit(int totalCredit)
        {
            if (totalCredit < 10 || totalCredit > 50) throw new ArgumentException("Toplam kredi 10 - 50 arasında olmalıdır.", nameof(totalCredit));
            TotalCredit = totalCredit;
        }
        private void TouchUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
