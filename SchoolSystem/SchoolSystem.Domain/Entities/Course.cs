
using SchoolSystem.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace SchoolSystem.Domain.Entities
{
    public class Course : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string ImageData { get; private set; } = string.Empty;
        public string Goal { get; private set; } = string.Empty;
        public string Summary { get; private set; } = string.Empty;
        public string TargetGroup { get; private set; } = string.Empty;
        public string Gains { get; private set; } = string.Empty;
        public string Requirements { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt{ get; private set; }
        public Guid TeacherId { get; private set; }
        public Teacher Teacher { get; private set; } = null!;

        private Course() { }
        public Course(string name, string description, string goal, string summary, string targetGroup, string gains, string requirements, string imageData, Guid teacherId) 
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetDescription(description);
            SetGoal(goal);
            SetSummary(summary);
            SetTargetGroup(targetGroup);
            SetGains(gains);
            SetRequirements(requirements);
            SetImage(imageData);
            SetTeacherId(teacherId);
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        public void UpdateCourse(string? name,string? description, string? goal, string? summary, string? targetGroup, string? gains, string? requirements, string? imageData)
        {
            bool isUpdated = false;
            if (name is not null)
            {
                SetName(name);
                isUpdated = true;
            }
            if(description is not null)
            {
                SetDescription(description);
                isUpdated = true;
            }
            if(goal is not null)
            {
                SetGoal(goal);
                isUpdated = true;
            }
            if(summary is not null)
            {
                SetSummary(summary);
                isUpdated = true;
            }
            if(targetGroup is not null)
            {
                SetTargetGroup(targetGroup);
                isUpdated = true;
            }
            if(gains is not null)
            {
                SetGains(gains);
                isUpdated = true;
            }
            if(requirements is not null)
            {
                SetRequirements(requirements);
                isUpdated = true;
            }
            if(imageData is not null)
            {
                SetImage(imageData);
                isUpdated = true;
            }
            if (isUpdated) TouchUpdate();
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
            Name = name;
        }
        private void SetDescription(string description)
        {
            Description = description;
        }
        private void SetImage(string imageData)
        {
            ImageData = imageData;
        }
        private void SetGoal(string goal)
        {
            Goal = goal;
        }
        private void SetSummary(string summary)
        {
            Summary = summary;
        }
        private void SetTargetGroup(string targetGroup)
        {
            TargetGroup = targetGroup;
        }
        private void SetGains(string gains)
        {
            Gains = gains;
        }
        private void SetRequirements(string requirements)
        {
            Requirements = requirements;
        }
        private void SetTeacherId(Guid id)
        {
            TeacherId = id;
        }
        private void TouchUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
