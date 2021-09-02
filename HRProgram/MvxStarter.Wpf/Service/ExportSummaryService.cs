using HRProgram.BLL.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvxStarter.Wpf.Service
{
    public static class ExportSummaryService
    {
        public static void ExportSummary(SummaryDTO summary, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("ФИО:");
                sw.Write($"{summary.Fullname}\n\n");
                sw.WriteLine("Должность:");
                sw.Write($"{summary.Position}\n\n");
                sw.WriteLine("Контактная информация:");
                sw.Write($"{summary.ContactInfo}\n\n");
                sw.WriteLine("Образование:");
                sw.Write($"{summary.Education}\n\n");
                sw.WriteLine("Опыт:");
                sw.Write($"{summary.Experience}\n\n");
                sw.WriteLine("Навыки:");
                sw.Write($"{summary.Skills}\n\n");
                sw.WriteLine("Рекоммендации:");
                sw.Write($"{summary.Recommendations}\n\n");
            }
        }
    }
}
