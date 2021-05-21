using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonModel model1 = new PersonModel();
            model1.ID = "1";
            model1.Name = "王杰";
            model1.Value = 90;
            model1.InCome = 100;
            model1.Pay = 200;
            model1.Age = 33;
            var result = model1.GetPropertyValue(model1, l => l.Name);
            Console.WriteLine($"显示名称：{result.Item1}-值:{result.Item2}");



            Expression<Func<int, double>> dd = a => (double)a;


            var ddd = dd.Compile();






            //  Message：根据表达式获取对应属性的值  
            List<PersonModel> models = new List<PersonModel>();

            Random r = new Random();

            string[] names = { "张学友", "王杰", "刘德华", "张曼玉", "李连杰", "孙悟空" };

            //  Message：构造测试数据
            for (int i = 0; i < 80; i++)
            {
                PersonModel model = new PersonModel();
                model.ID = i.ToString();
                model.Name = names[r.Next(6)];
                model.Value = r.Next(20, 100);
                model.InCome = r.Next(20, 100);
                model.Pay = r.Next(20, 100);
                model.Age = r.Next(20, 100);
                models.Add(model);
            }

            //  Message：生成自定义报表
            DataTable dt = PersonModel.GetSum(models.AsQueryable(), l => l.Name, l => l.Value, l => l.Age);

            WriteTable(dt);
        }

        public static void WriteTable(DataTable dt)
        {
            string colums = string.Empty;
            ;
            foreach (DataColumn item in dt.Columns)
            {
                colums += item.ColumnName.PadRight(5, ' ') + " ";
            }

            Console.WriteLine(colums);

            foreach (DataRow item in dt.Rows)
            {
                string rows = string.Empty;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    rows += item[i].ToString().PadRight(5, ' ') + " ";
                }

                Console.WriteLine(rows);
            }
        }
    }

}
