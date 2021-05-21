using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionDemo
{
    [Description("唯一标识")]
    public class PersonModel
    {
        [Description("唯一标识")]
        
        public string ID { get; set; }

        [Description("名称")]
        public string Name { get; set; }

        [Description("值")]
        public double Value { get; set; }

        [Description("年齡")]
        public double Age { get; set; }

        [Description("收入")]
        public double InCome { get; set; }

        [Description("支出")]
        public double Pay { get; set; }

        /// <summary>
        /// 通过Linq表达式获取成员属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Tuple<string, string> GetPropertyValue<T>(T instance, Expression<Func<T, string>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            string propertyName = memberExpression.Member.Name;

            string attributeName = (memberExpression.Member.GetCustomAttributes(false)[0] as DescriptionAttribute).Description;

            var property = typeof(T).GetProperties().Where(l => l.Name == propertyName).First();

            return new Tuple<string, string>(attributeName, property.GetValue(instance).ToString());

        }

        /// <summary>
        /// 获取汇总求和数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="groupby"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static DataTable GetSum<T>(IQueryable<T> collection, Expression<Func<T, String>> groupby, params Expression<Func<T, double>>[] expressions)
        {
            DataTable table = new DataTable();

            //  Message：利用表达式设置列名称
            MemberExpression memberExpression = groupby.Body as MemberExpression;

            var displayName = (memberExpression.Member.GetCustomAttributes(false)[0] as DescriptionAttribute).Description;

            table.Columns.Add(new DataColumn(displayName));

            foreach (var expression in expressions)
            {
                memberExpression = expression.Body as MemberExpression;

                displayName = (memberExpression.Member.GetCustomAttributes(false)[0] as DescriptionAttribute).Description;

                table.Columns.Add(new DataColumn(displayName));

            }

            //  Message：通过表达式设置数据体 
            var groups = collection.GroupBy(groupby);

            foreach (var group in groups)
            {
                //  Message：设置分组列头
                DataRow dataRow = table.NewRow();
                dataRow[0] = group.Key;

                //  Message：设置分组汇总数据
                for (int i = 0; i < expressions.Length; i++)
                {
                    var expression = expressions[i];

                    Func<T, double> fun = expression.Compile();

                    dataRow[i + 1] = group.Sum(fun);
                }

                table.Rows.Add(dataRow);
            }

            return table;

        }
              /// <summary>
        /// 获取汇总求和数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="groupby"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public DataTable GetSum<TModel,TAttr>(IQueryable<TModel> collection,Func<TAttr,string> toHeader, 
                  Expression<Func<TModel, String>> groupby, params Expression<Func<TModel, double>>[] expressions)
        {
            DataTable table = new DataTable();
 
            //  Message：利用表达式设置列名称
            MemberExpression memberExpression = groupby.Body as MemberExpression;
 
            var displayName = toHeader((TAttr)memberExpression.Member.GetCustomAttributes(typeof(TAttr),false).First());
 
            table.Columns.Add(new DataColumn(displayName));
 
            foreach (var expression in expressions)
            {
                memberExpression = expression.Body as MemberExpression;
 
               displayName = toHeader((TAttr)memberExpression.Member.GetCustomAttributes(typeof(TAttr), false).First());
 
                table.Columns.Add(new DataColumn(displayName));
 
            }
 
            //  Message：通过表达式设置数据体 
            var groups = collection.GroupBy(groupby);
 
            foreach (var group in groups)
            {
                //  Message：设置分组列头
                DataRow dataRow = table.NewRow();
                dataRow[0] = group.Key;
 
                //  Message：设置分组汇总数据
                for (int i = 0; i < expressions.Length; i++)
                {
                    var expression = expressions[i];
 
                    Func<TModel, double> fun = expression.Compile();
 
                    dataRow[i + 1] = group.Sum(fun);
                }
 
                table.Rows.Add(dataRow);
            }
 
            return table;
 
        }
    }


}
