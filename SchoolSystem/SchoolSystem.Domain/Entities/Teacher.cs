
using SchoolSystem.Domain.Enums;
using SchoolSystem.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace SchoolSystem.Domain.Entities
{
    public class Teacher : IUserEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public int Age { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public Role Role { get; private set; }
        public Branch Branch { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Course? Course { get; private set; }

        private Teacher() { }
        public Teacher(string name, string surname, int age, string email, string passwordHash, Branch branch, Role role = Role.Teacher)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetSurname(surname);
            SetAge(age);
            SetEmail(email);
            SetPasswordHash(passwordHash);
            SetBranch(branch);
            IsActive = true;
            Role = role;
            CreatedAt = DateTime.UtcNow;
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
        public void ChangeRole(Role role)
        {
            SetRole(role);
            TouchUpdate();
        }
        public void ChangeBranch(Branch branch)
        {
            SetBranch(branch);
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
            if (!Regex.IsMatch(surname, @"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")) throw new ArgumentException("Ad sadece harflerden oluşmalı, sayı veya boşluk içeremez.", nameof(surname));
            Surname = surname;
        }
        private void SetAge(int age)
        {
            if (age <= 20 || age >= 130) throw new ArgumentException("20 - 130 arasında geçerli bir yaş girin.", nameof(age));
            Age = age;
        }
        private void SetEmail(string email)
        {   
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("E-posta boş olamaz.", nameof(email));
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) throw new ArgumentException("Geçersiz e-posta formatı.", nameof(email));
            Email = email;
        }
        private void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("Şifre boş olamaz.", nameof(passwordHash));
            PasswordHash = passwordHash;
        }
        private void SetBranch(Branch branch)
        {
            if (!Enum.IsDefined(typeof(Branch), branch)) throw new ArgumentException("Geçersiz branş.", nameof(branch));
            Branch = branch;
        }
        private void SetRole(Role role)
        {
            if (!Enum.IsDefined(typeof(Role), role)) throw new ArgumentException("Geçerli bir rol girin.", nameof(role));
            Role = role;
        }
        private void TouchUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
