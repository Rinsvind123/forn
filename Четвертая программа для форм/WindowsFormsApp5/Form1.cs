using System;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
            // Данный метод позволяет при нажатии на кнопку очистить текстовые поля 
        {                                                       
            if (inputTextBox.Text.Length > 0)
            inputTextBox.Text = inputTextBox.Text.Remove(0); //Если пустое поле, то не сработает 
            if (outputTextBox.Text.Length > 0)
                outputTextBox.Text = outputTextBox.Text.Remove(0);
            if (FilterTextBox.Text.Length > 0)
                FilterTextBox.Text = FilterTextBox.Text.Remove(0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void generationButton_Click(object sender, EventArgs e)
            // Этот метод позволяет сгенерировать 10 записей типа NOTE
        {
            string temp = null; // Заводим пустую строковую переменную
            NOTE[] note = new NOTE[10]; //Создаем массив из 10 записей типа NOTE
            Random rand = new Random();
            for(int i = 0; i < 10; i++)
            {
                note[i] = new NOTE();
                string puth_name = @"C:\Users\днс\Desktop\Inputtext.txt"; 
                string[] name = File.ReadAllLines(puth_name); // Из содержимого входного файла формируем массив
                beginning:
                note[i].name_surname = name[rand.Next(0, 100)]; // Случайным образом выбираем одну из 100 элементов внутри файла
                if (i>=1 && note[i].name_surname == note[i - 1].name_surname) // Проверка совпадений записей
                {
                    goto beginning;
                }
                respawn:
                note[i].number_phone = rand.Next(890000000, 899999999).ToString() + rand.Next(10, 99).ToString();
                note[i].day = rand.Next(1, 31);
                note[i].month = rand.Next(1, 12);
                note[i].year = rand.Next(1998, 2002);
                if ((note[i].month == 4 || note[i].month == 6 || note[i].month == 9 || note[i].month == 11) 
                    && note[i].day > 30) //С 51 по 60 строки идет проверка корректности сформированной даты рождения
                {
                    goto respawn;
                }
                if((note[i].month == 2 && note[i].day > 28 && note[i].year % 4 == 1) ||
                    note[i].month == 2 && note[i].day > 29 && note[i].year % 4 == 0)
                {
                    goto respawn;
                }
                temp += note[i].getInformation(); //Записываем в переменную сведения об одном человеке
            }
            inputTextBox.Text = temp; // Содержимое переменной записываем во входное текстовое поле
        }
        private void sortButton_Click(object sender, EventArgs e)
        {
            // Этот метод сортирует записи по датам рождения
            string[] notes = inputTextBox.Text.Split('\n'); // Разбиваем наш текст построчно
            string temp = null;
           
                for (int i = 0; i != notes.Length - 1; i++)
                {
                    
                    for (int j = i + 1; j != notes.Length -1 ; j++)
                   {
                    string[] arr = notes[i].Split(' '); // Разбиваем строку на элементы, создав массив
                    string[] res = notes[j].Split(' ');
                    if (Convert.ToInt32(arr[arr.Length - 1]) > Convert.ToInt32(res[res.Length - 1])) /* С 78-110 строки идет сортировка записей
                       * по датам рождения по возрастанию  */
                    {
                        string vr = notes[i];
                        notes[i] = notes[j];
                        notes[j] = vr;
                    }
                    
                   }
                for (int j = i + 1; j != notes.Length - 1; j++)
                {
                    string[] arr = notes[i].Split(' ');
                    string[] res = notes[j].Split(' ');
                    if (Convert.ToInt32(arr[arr.Length - 1]) == Convert.ToInt32(res[res.Length - 1]) &&
                        Convert.ToInt32(arr[arr.Length - 3]) > Convert.ToInt32(res[res.Length - 3]))
                    {
                        string vr = notes[i];
                        notes[i] = notes[j];
                        notes[j] = vr;
                    }
                }
                for (int j = i + 1; j != notes.Length - 1; j++)
                {
                    string[] arr = notes[i].Split(' ');
                    string[] res = notes[j].Split(' ');
                    if (Convert.ToInt32(arr[arr.Length - 1]) == Convert.ToInt32(res[res.Length - 1]) &&
                        Convert.ToInt32(arr[arr.Length - 3]) == Convert.ToInt32(res[res.Length - 3]) &&
                        Convert.ToInt32(arr[arr.Length - 5]) > Convert.ToInt32(res[res.Length - 5]))
                    {
                        string vr = notes[i];
                        notes[i] = notes[j];
                        notes[j] = vr;
                    }
                }
                temp += notes[i] + "\n"; // После сортировки добавляем запись с переходом на новую строку

            }
            inputTextBox.Text = temp; // Заполняем текстовое поле отсортированным списком записей типа NOTE
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Метод позволяет сохранять информацию из текстового поля в сторонний текстовый файл
            string save = @"C:\Users\днс\Desktop\Savefile.txt"; // Создаем текстовый файл
            File.WriteAllText(save, inputTextBox.Text); // Сохраняем содержимое текстового поля в файл
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            // Метод позволяет загружать информацию из текстового файла во второе текстовое поле
            string load = @"C:\Users\днс\Desktop\Savefile.txt"; 
            outputTextBox.Text = File.ReadAllText(load); //Заполняем второе текстовое поле содержимым из файла
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            // Метод позволяет находить записи по номеру телефона
            if (inputTextBox.Text.Length > 0 && FilterTextBox.Text.Length > 0)
            {
                string[] notes = inputTextBox.Text.Split('\n');
                bool flag = false; /* Создаем логическую переменную для одноразовой проверки условия, поскольку нас интересует, есть ли
                                    * вообще люди с таким номером */
                long number = Convert.ToInt64(FilterTextBox.Text);
                for (int i = 0; i < notes.Length - 1; i++)
                {
                    string[] array = notes[i].Split(' ');
                    if (Convert.ToInt64(array[array.Length - 7]) == number) // Проверяем совпадения
                    {
                        flag = true;
                        outputTextBox.Text = notes[i]; // Выводим во второе текстовое поле информацию о человеке в случае совпадения
                    }
                }
                if (flag == false)
                {
                    if(outputTextBox.Text.Length > 0) // если нет совпадений, и второе текстовое поле не пустое, то очищаем это поле
                    {
                        outputTextBox.Text = outputTextBox.Text.Remove(0);
                    }
                    MessageBox.Show("Абонентов с таким номером не найдено"); //Выводим на экран сообщение о том, что совпадений не обнаружено
                }
            }
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}