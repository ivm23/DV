using System;
using Registration.DataInterface;
namespace Registration.Model
{
    public class Worker
    {
        public Worker() {
        }

        public Worker(IWorker worker)
        {
            Login = worker.Login;
            Name = worker.Name;
            Password = worker.Password;
            Id = worker.Id;
        }

        public string Login
        {
            get; set;
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return GetFIO();
        }

        public string GetFIO()
        {
            return string.Format($"{Name} ({Login})");
        }

        public bool isEmpty()
        {
            return string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name) || Id == Guid.Empty;
        }

    }
}
