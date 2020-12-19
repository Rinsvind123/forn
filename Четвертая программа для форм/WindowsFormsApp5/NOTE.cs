using System;
namespace WindowsFormsApp5
{
   public class NOTE
    {
        public string name_surname; // все необходимые поля для наших записей
        public string number_phone;
        public int day;
        public int month;
        public int year;
        ~NOTE()
        {

        }
        public string getInformation()
            // Метод, позволяющий непосредственно создавать запись типа NOTE
        {
            string info = name_surname + " Номер: " + number_phone + " Дата: "  + day.ToString() + " / " 
            + month.ToString() + " / " + year.ToString() + Environment.NewLine; 
            return info;
        }
    }
}