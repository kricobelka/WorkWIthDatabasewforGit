using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;

namespace WorkingWithDatabase
{
    public class AdoNetExample
    {
        public async Task MakeAdoNet()
        //28.10: параметризированный запрос: //public async Task MakeAdoNet(string firstNameFromUi)
        {
            //connection to the db:
            var connectionString = "Server=localhost;Database=AdoNetExample;Trusted_Connection=True;Encrypt=False";
            //если бы был пароль, писали бы вместо трастид "User=;PSW="
            using var sqlConnection = new SqlConnection(connectionString);
            {
                //using - директива, которая позволяет освобождать память без исп-я метода Dispose()
                //после того как вызвали метод, using автоматически освобождает память 
                await sqlConnection.OpenAsync();

                var getUsersSql = "select * from Users";
                //var getUsersSql = "select * from Users where FirstName = @firstName";

                var sqlCommand = new SqlCommand(getUsersSql, sqlConnection);
                //sqlCommand.ExecuteReaderAsync();
                //// когда из табл возвращаются к-л данные, возвращает данные коотрые мы можем считать с БД
                //sqlCommand.ExecuteScalarAsync();
                //// возвращает объект, можем возвратить одно скалярное значение из БД 
                //sqlCommand.ExecuteNonQueryAsync();
                //когда не ожидаем возвращ-я к-л данных из БД,когда возвращаем хранимые функции, например insert/delete

                //28.10: у sqlcommand есть property Parameteres: с помощью него определяем параметры
                //которые хотим передать в БД и затем отсылаем запрос к БД:
                //sqlCommand.Parameters.Add(new SqlParameter("firstName", firstNameFromUi));

                var dataReader = await sqlCommand.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    //получаем нулевой индекс:
                    //var userId1 = (int)dataReader.GetValue(0);
                    //var userId2 = dataReader.GetInt32(0);
                    var userId = dataReader.GetFieldValue<int>("UserId");
                    var firstName = dataReader.GetFieldValue<string>("FirstName");
                    var lastName = dataReader.GetFieldValue<string>("LastName");
                    var birthDate = dataReader.IsDBNull("BirthDate")
                        // если значение null, то возвращает ноль,
                        ? null
                        // если нет значения то existing value:
                        :
                        dataReader.GetFieldValue<DateTime?>("BirthDate");

                    Console.WriteLine($"{userId}: {firstName} {lastName} {birthDate}");
                }
            }
        }
    }
}
