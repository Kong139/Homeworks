using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHocSinh
{
    // Định nghĩa lớp Student
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Tạo danh sách học sinh
            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "An", Age = 18 },
                new Student { Id = 2, Name = "Binh", Age = 17 },
                new Student { Id = 3, Name = "Kha", Age = 18 },
                new Student { Id = 4, Name = "Linh", Age = 16 },
                new Student { Id = 5, Name = "Tung", Age = 19 }
            };

            // a. In toàn bộ danh sách học sinh
            Console.WriteLine("Danh sách toàn bộ học sinh:");
            students.Select(s => s)
                    .ToList()
                    .ForEach(s => Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}"));

            // b. Tìm và in danh sách các học sinh có tuổi từ 15 đến 18
            Console.WriteLine("\nHọc sinh có tuổi từ 15 đến 18:");
            students.Where(s => s.Age >= 15 && s.Age <= 18)
                    .ToList()
                    .ForEach(s => Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}"));

            // c. Tìm và in học sinh có tên bắt đầu bằng chữ "A"
            Console.WriteLine("\nHọc sinh có tên bắt đầu bằng chữ 'A':");
            students.Where(s => s.Name.StartsWith("A"))
                    .ToList()
                    .ForEach(s => Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}"));

            // d. Tính tổng tuổi của tất cả học sinh trong danh sách
            int totalAge = students.Sum(s => s.Age);
            Console.WriteLine($"\nTổng tuổi của tất cả học sinh: {totalAge}");

            // e. Tìm và in học sinh có tuổi lớn nhất
            Console.WriteLine("\nHọc sinh có tuổi lớn nhất:");
            students.Where(s => s.Age == students.Max(m => m.Age))
                    .ToList()
                    .ForEach(s => Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}"));

            // f. Sắp xếp danh sách học sinh theo tuổi tăng dần và in ra
            Console.WriteLine("\nDanh sách sau khi sắp xếp theo tuổi tăng dần:");
            students.OrderBy(s => s.Age)
                    .ToList()
                    .ForEach(s => Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}"));
        }
    }
}